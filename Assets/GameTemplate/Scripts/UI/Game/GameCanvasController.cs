using GameTemplate.Scripts.Systems.Scene;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.Scripts.UI.Game
{
    public class GameCanvasController: IStartable
    {
        ISceneService _sceneService;

        [Inject]
        public void Construct(ISceneService sceneService)
        {
            Debug.Log("Construct UIGameCanvas");
            _sceneService = sceneService;
        }

        public void Start()
        {
            
        }
    }
}