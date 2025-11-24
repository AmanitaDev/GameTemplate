using GameTemplate.Scripts.Systems.Audio;
using GameTemplate.Scripts.Systems.Scene;
using UnityEngine;
using VContainer.Unity;

namespace GameTemplate.Scripts.Scopes
{
    public class GameInitializer : IStartable
    {
        private readonly AudioService _audioService;
        private readonly ISceneService _sceneService;

        // Dependencies are injected via the constructor
        public GameInitializer(AudioService audioService, ISceneService sceneService)
        {
            _audioService = audioService;
            _sceneService = sceneService;
        }

        // IStartable requires this method, which VContainer calls after all dependencies are resolved.
        public void Start()
        {
            InitializeApplicationAsync();
        }

        private async void InitializeApplicationAsync()
        {
            // Ensure SoundService is ready before Main Menu
            await _audioService.InitializeAsync();
            
            // Load the main menu scene
            _sceneService.LoadMenuScene();
        }
    }
}