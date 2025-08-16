using GameTemplate.Core.Scopes;
using GameTemplate.Scripts.Systems.Scene;
using GameTemplate.Scripts.UI.Game;
using GameTemplate.Systems.Audio;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.Scripts.Core.Scopes
{
    public class GameScope : GameStateScope
    {
        public override bool Persists => false;
        public override GameState ActiveState => GameState.Game;

        [SerializeField] private UIGameCanvas uiGameCanvas;

        [Inject] ISceneService _sceneService;
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
    }
}