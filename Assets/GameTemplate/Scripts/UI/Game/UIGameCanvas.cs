using DG.Tweening;
using GameTemplate.Scripts.Systems.Scene;
using UnityEngine;
using VContainer;

namespace GameTemplate.Scripts.UI.Game
{
    public class UIGameCanvas : MonoBehaviour
    {
        ISceneService _sceneService;

        [Inject]
        public void Construct(ISceneService sceneService)
        {
            Debug.Log("Construct UIGameCanvas");
            _sceneService = sceneService;
        }
    }
}