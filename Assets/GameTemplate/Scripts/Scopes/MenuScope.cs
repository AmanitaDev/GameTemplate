using GameTemplate.Scripts.Systems.Settings;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.Scripts.Scopes
{
    /// <summary>
    /// Game Logic that runs when sitting at the MainMenu. This is likely to be "nothing", as no game has been started. But it is
    /// nonetheless important to have a game state, as the GameStateBehaviour system requires that all scenes have states.
    /// </summary>
    public class MenuScope : GameStateScope
    {
        public override bool Persists => false;
        public override GameState ActiveState => GameState.MainMenu;

        [SerializeField] private SettingsDataSO settingsDataSo;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterInstance(settingsDataSo);

            builder.Register<SettingsModel>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<SettingsView>();
            builder.Register<SettingsController>(Lifetime.Singleton).AsSelf();
        }

        protected override void Awake()
        {
            base.Awake();
            var settingsController = Container.Resolve<SettingsController>();
            settingsController.Initialize();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}