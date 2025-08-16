using DG.Tweening;
using GameTemplate.Systems.Scene;
using UnityEngine;
using VContainer;

namespace GameTemplate.Scripts.UI.Game
{
    public class UIGameCanvas : MonoBehaviour
    {
        ISceneService _SceneService;

        [Inject]
        public void Construct(ISceneService SceneService)
        {
            Debug.Log("Construct UIGameCanvas");
            _SceneService = SceneService;
        }
        
        public void GameFinished(bool gameWon)
        {
            
        }

        void OpenPanel(CanvasGroup group)
        {
            group.DOFade(1, 1);
            group.interactable = true;
            group.blocksRaycasts = true;
        }

        public void EndTheDayClick()
        {
            _SceneService.LoadScene(new SceneLoadData
            {
                sceneName = "Upgrades",
                unloadCurrent = true,
                activateLoadingCanvas = true,
                setActiveScene = true
            });
        }
    }
}