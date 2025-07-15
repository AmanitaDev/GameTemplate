using GameTemplate.Scripts.Systems.Settings;
using GameTemplate.Systems.Audio;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.Core.Scopes
{
    /// <summary>
    /// Game Logic that runs when sitting at the MainMenu. This is likely to be "nothing", as no game has been started. But it is
    /// nonetheless important to have a game state, as the GameStateBehaviour system requires that all scenes have states.
    /// </summary>
    public class MenuScope : GameStateScope
    {
        public override bool Persists => false;
        public override GameState ActiveState => GameState.MainMenu;
        
        [SerializeField] private GameSettingsSO settingsSO;
        [SerializeField] private SettingsView settingsView;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
        }

        protected override void Start()
        {
            base.Start();
            
            // Resolve SoundService from container
            var soundService = Container.Resolve<AudioService>();
            
            var model = new SettingsModel(settingsSO, soundService);
            var controller = new SettingsController(model, settingsView);
            controller.Initialize();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}