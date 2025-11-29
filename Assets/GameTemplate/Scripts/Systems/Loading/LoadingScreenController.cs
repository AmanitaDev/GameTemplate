using System.Collections;
using GameTemplate.Scripts.Systems.Scene;
using UnityEngine;
using VContainer;

namespace GameTemplate.Scripts.Systems.Loading
{
    public class LoadingScreenController : MonoBehaviour
    {
        [SerializeField] CanvasGroup canvasGroup;

        [SerializeField] float delayBeforeFadeOut = 0.5f;

        [SerializeField] float fadeOutDuration = 0.1f;
        
        public GameObject cameraObject;

        bool loadingScreenActive = true;

        Coroutine fadeOutCoroutine;

        ISceneService _sceneService;

        [Inject]
        public void Construct(ISceneService sceneService)
        {
            //Debug.Log("Construct LoadingScreen");
            _sceneService = sceneService;
            _sceneService.OnBeforeSceneLoad += OpenLoadingScreen;
            _sceneService.OnSceneLoaded += CloseLoadingScreen;
        }

        private void OnDestroy()
        {
            _sceneService.OnBeforeSceneLoad -= OpenLoadingScreen;
            _sceneService.OnSceneLoaded -= CloseLoadingScreen;
        }

        public void OpenLoadingScreen()
        {
            SetCanvasVisibility(true);
            loadingScreenActive = true;
            cameraObject.SetActive(true);
            //Debug.LogError("open canvas");
            if (loadingScreenActive)
            {
                if (fadeOutCoroutine != null)
                {
                    //Debug.Log("start loading screen");
                    StopCoroutine(fadeOutCoroutine);
                }
            }
        }

        public void CloseLoadingScreen()
        {
            //Debug.LogError("close canvas");
            cameraObject.SetActive(false);
            if (loadingScreenActive)
            {
                if (fadeOutCoroutine != null)
                {
                    //Debug.Log("stop loading screen");
                    StopCoroutine(fadeOutCoroutine);
                }

                fadeOutCoroutine = StartCoroutine(FadeOutCoroutine());
            }
        }

        void SetCanvasVisibility(bool visible)
        {
            canvasGroup.alpha = visible ? 1 : 0;
            canvasGroup.blocksRaycasts = visible;
        }

        IEnumerator FadeOutCoroutine()
        {
            yield return new WaitForSeconds(delayBeforeFadeOut);
            loadingScreenActive = false;

            float currentTime = 0;
            while (currentTime < fadeOutDuration)
            {
                canvasGroup.alpha = Mathf.Lerp(1, 0, currentTime / fadeOutDuration);
                yield return null;
                currentTime += Time.deltaTime;
            }

            SetCanvasVisibility(false);
        }
    }
}