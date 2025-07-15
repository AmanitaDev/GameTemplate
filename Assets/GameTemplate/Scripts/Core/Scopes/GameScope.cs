using AssetKits.ParticleImage;
using Cysharp.Threading.Tasks;
using GameTemplate.Systems.Audio;
using GameTemplate.Systems.Scene;
using GameTemplate.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using SceneLoadData = GameTemplate.Systems.Scene.SceneLoadData;

namespace GameTemplate.Core.Scopes
{
    public class GameScope : GameStateScope
    {
        public override bool Persists => false;
        public override GameState ActiveState => GameState.Game;

        [SerializeField] private UIGameCanvas _uiGameCanvas;
        [SerializeField] private ParticleImage _winParticleImage;

        // Wait time constants for switching to post game after the game is won or lost
        private const float k_WinDelay = 2.0f;
        private const float k_LoseDelay = 2.0f;

        [Inject] ISceneService _SceneService;
        [Inject] AudioService _audioService;


        protected override void Start()
        {
            base.Start();

            //Do some things here
        }

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            
            builder.RegisterComponentInHierarchy<UIGameCanvas>();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        public void OnGameFinished(bool isWin)
        {
            // start the coroutine
            _ = CoroGameOver(isWin ? k_WinDelay : k_LoseDelay, isWin);
        }

        async UniTaskVoid CoroGameOver(float wait, bool gameWon)
        {
            if (gameWon) _winParticleImage.Play();

            //TODO change this game to game
            // wait for game animations to finish
            await UniTask.Delay((int)(wait * 1000)); // waits for wait*1 second

            //win or lose canvas should open
            _uiGameCanvas.GameFinished(gameWon);

            if (gameWon)
            {
                _audioService.PlaySFX(AudioID.Win);
                //_levelService.SetNextLevel();
            }
            else
            {
                _audioService.PlaySFX(AudioID.Lose);
            }
        }

        public void NextButtonClick()
        {
            /*if (_levelService.LevelId < 2)
            {*/
                _SceneService.LoadScene(new SceneLoadData
                {
                    sceneName = "Game",
                    unloadCurrent = true,
                    activateLoadingCanvas = true,
                    setActiveScene = false
                });
            /*}
            else
            {
                _SceneService.LoadScene(new SceneLoadData
                {
                    sceneName = "MainMenu",
                    unloadCurrent = true,
                    activateLoadingCanvas = true,
                    setActiveScene = false
                });
            }*/
        }

        public void RetryButtonClick()
        {
            _SceneService.LoadScene(new SceneLoadData
            {
                sceneName = "Game",
                unloadCurrent = true,
                activateLoadingCanvas = true,
                setActiveScene = false
            });
        }
    }
}