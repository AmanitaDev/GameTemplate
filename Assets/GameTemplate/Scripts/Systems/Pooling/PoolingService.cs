using System.Collections.Generic;
using GameTemplate.Systems.Pooling;
using UnityEngine;
using VContainer;

namespace GameTemplate.Scripts.Systems.Pooling
{
    /// <summary>
    /// Global service responsible for managing the object pool.
    /// It handles pre-spawning, retrieval, and recycling of GameObjects efficiently.
    /// This service is registered as a MonoBehaviour component in the scene and uses Method Injection.
    /// </summary>
    public class PoolingService : MonoBehaviour
    {
        // Parent Transform to organize all pooled GameObjects in the hierarchy.
        [HideInInspector] public Transform poolParent;
        
        // Dictionary for storing the actual pooled objects (Queues provide O(1) retrieval time).
        private Dictionary<PoolID, Queue<GameObject>> objectPool = new Dictionary<PoolID, Queue<GameObject>>();
        
        // Dictionary for fast O(1) lookup of the prefab data based on PoolID. 
        // This replaces the slow LINQ search previously used in GetGameObjectById.
        private Dictionary<PoolID, PoolObject> _prefabLookup = new Dictionary<PoolID, PoolObject>();
        
        // We store pool objects that parents changed in this dictionary. 
        // This way we can retrieve them back during a global ResetPool operation.
        [SerializeField] private List<PoolElement> parentsChangedPoolObjects = new List<PoolElement>();

        // Injected data source containing pool configuration.
        PoolingDataSO _poolingDataSoDataSo;

        [Inject]
        public void Construct(PoolingDataSO poolingDataSoDataSo)
        {
            Debug.Log("Construct PoolingService");
            _poolingDataSoDataSo = poolingDataSoDataSo;
            SpawnObjects();
        }

        /// <summary>
        /// Initializes the pool by creating the parent object and pre-spawning all configured GameObjects.
        /// </summary>
        void SpawnObjects()
        {
            Debug.Log("Initialize PoolingService");
            poolParent = new GameObject("_PoolParent").transform;
            DontDestroyOnLoad(poolParent.gameObject);
            
            // Populate both the object pool and the prefab lookup dictionary for efficiency.
            for (int i = 0; i < _poolingDataSoDataSo.poolObjects.Length; i++)
            {
                PoolID currentID = (PoolID)i;
                PoolObject poolObj = _poolingDataSoDataSo.poolObjects[i];

                objectPool.Add(currentID, new Queue<GameObject>());
                _prefabLookup.Add(currentID, poolObj); // O(1) lookup for prefab data

                for (int z = 0; z < poolObj.objectCount; z++)
                {
                    GameObject newObject = Instantiate(poolObj.objectPrefab, poolParent);
                    newObject.SetActive(false);
                    newObject.GetComponent<PoolElement>()
                        .Initialize(poolObj.goBackOnDisable, currentID);
                    objectPool[currentID].Enqueue(newObject);
                }
            }
        }

        /// <summary>
        /// Gets a GameObject from the pool by its PoolID. Default position and rotation.
        /// </summary>
        public GameObject GetGameObjectById(PoolID poolId)
        {
            return GetGameObjectById(poolId, Vector3.zero, Quaternion.identity, Vector3.one);
        }

        /// <summary>
        /// Gets a GameObject from the pool using a reference Transform for position and rotation.
        /// </summary>
        public GameObject GetGameObjectById(PoolID poolId, Transform objectTransform)
        {
            return GetGameObjectById(poolId, objectTransform.position, objectTransform.rotation, objectTransform.localScale);
        }

        /// <summary>
        /// Gets a GameObject from the pool, specifying position and rotation.
        /// </summary>
        public GameObject GetGameObjectById(PoolID poolId, Vector3 position, Quaternion rotation)
        {
            return GetGameObjectById(poolId, position, rotation, Vector3.one);
        }

        /// <summary>
        /// Gets a GameObject from the pool, specifying position, rotation, and scale.
        /// This is the master retrieval method.
        /// </summary>
        public GameObject GetGameObjectById(PoolID poolId, Vector3 position, Quaternion rotation, Vector3 targetScale)
        {
            // Ensure the PoolID exists in the main dictionary
            if (!objectPool.ContainsKey(poolId))
            {
                // This scenario should be rare if SpawnObjects is correct, but handles unexpected IDs.
                objectPool.Add(poolId, new Queue<GameObject>());
            }

            GameObject poolObject;
            bool wasPooled = false;

            // 1. Try to retrieve from the pool queue
            if (objectPool[poolId].Count != 0)
            {
                poolObject = objectPool[poolId].Dequeue();
                wasPooled = true;
            }
            // 2. Instantiate a new object if the queue is empty (dynamic expansion)
            else if (_prefabLookup.ContainsKey(poolId)) // Check lookup for prefab data
            {
                PoolObject selectedPoolObject = _prefabLookup[poolId]; // O(1) lookup
                poolObject = Object.Instantiate(selectedPoolObject.objectPrefab, position, rotation, poolParent);
                poolObject.GetComponent<PoolElement>().Initialize(selectedPoolObject.goBackOnDisable, poolId);
            }
            else
            {
                Debug.LogError($"[PoolingService] PoolID {poolId} not found in configuration!");
                return null;
            }

            // --- Configuration of the retrieved/instantiated object ---
            poolObject.transform.position = position;
            poolObject.transform.rotation = rotation;
            poolObject.transform.localScale = targetScale;
            
            // Only set active if it came from the pool (newly instantiated objects are usually active by default)
            if (wasPooled)
            {
                poolObject.SetActive(true);
            }
            
            return poolObject;
        }

        /// <summary>
        /// Recycles a GameObject after finding its PoolElement component.
        /// This method is potentially slow due to GetComponent lookup.
        /// </summary>
        public void GoBackToPool(GameObject poolObject)
        {
            PoolElement element = poolObject.GetComponent<PoolElement>();
            if (element != null)
            {
                GoBackToPool(element.PoolId, poolObject);
            }
            else
            {
                Debug.LogError($"Attempted to return object without PoolElement to pool: {poolObject.name}");
            }
        }
        
        /// <summary>
        /// The master recycling method: deactivates, resets parent, and enqueues the object.
        /// </summary>
        public void GoBackToPool(PoolID poolId, GameObject objectToAddPool)
        {
            objectToAddPool.SetActive(false);
            objectToAddPool.transform.SetParent(poolParent);
            
            if (objectPool.ContainsKey(poolId))
            {
                objectPool[poolId].Enqueue(objectToAddPool);
            }
            else
            {
                // Should not happen, but prevents loss of object if ID is missing.
                Debug.LogWarning($"PoolID {poolId} not found during recycling. Object was deactivated but not re-pooled.");
            }
        }

        /// <summary>
        /// Resets the pool by returning all parents-changed objects and deactivating all children.
        /// Typically called on scene exit or game reset.
        /// </summary>
        public void ResetPool()
        {
            // 1. Restore the parent of all dynamically parented objects
            for (int i = 0; i < parentsChangedPoolObjects.Count; i++)
            {
                if (parentsChangedPoolObjects[i] != null)
                {
                    parentsChangedPoolObjects[i].transform.SetParent(poolParent);
                }
            }
            parentsChangedPoolObjects.Clear();

            // 2. Deactivate all existing pooled objects in the hierarchy
            // This is a safety measure to catch objects that were never properly returned to the queue.
            PoolElement[] children = poolParent.GetComponentsInChildren<PoolElement>(true); // Search inactive children too
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i].gameObject.activeSelf)
                {
                    children[i].gameObject.SetActive(false);
                }
            }
        }
    }
}