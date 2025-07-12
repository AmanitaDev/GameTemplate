using DG.Tweening;
using GameTemplate.Systems.Scene;
using UnityEngine;
using VContainer;

namespace GameTemplate.UI
{
    public class UIGameCanvas : MonoBehaviour
    {
        [SerializeField]
        private GameObject TopPanel, WinPanel, LosePanel;

        ISceneService _SceneService;

        [Inject]
        public void Construct(ISceneService SceneService)
        {
            Debug.Log("Construct UIGameCanvas");
            _SceneService = SceneService;
        }

        public void Initialize(int UIlevelID)
        {
            LevelTextSetter[] levelTextSetters = GetComponentsInChildren<LevelTextSetter>();
            foreach (var levelTextSetter in levelTextSetters)
            {
                levelTextSetter.SetLevelText(UIlevelID);
            }

            if (UIlevelID == 1)
            {
                TopPanel.SetActive(false);
            }
        }
        
        public void GameFinished(bool gameWon)
        {
            if (gameWon)
            {
                OpenPanel(WinPanel.GetComponent<CanvasGroup>());
            }
            else
            {
                OpenPanel(LosePanel.GetComponent<CanvasGroup>());
            }
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