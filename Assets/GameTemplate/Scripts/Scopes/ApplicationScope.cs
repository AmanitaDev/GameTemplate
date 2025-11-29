using GameTemplate.Scripts.Systems.Audio;
using GameTemplate.Scripts.Systems.Input;
using GameTemplate.Scripts.Systems.Loading;
using GameTemplate.Scripts.Systems.Pooling;
using GameTemplate.Scripts.Systems.SaveLoad;
using GameTemplate.Scripts.Systems.Scene;
using GameTemplate.Scripts.Systems.Settings;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.Scripts.Scopes
{
    /// <summary>
    /// An entry point to the application, where we bind all the common dependencies to the root DI scope.
    /// </summary>
    public class ApplicationScope : LifetimeScope
    {
        public AudioDataSO audioDataSo;
        public SceneDataSO sceneDataSo;
        public PoolingDataSO poolingDataSo;
        public SettingsDataSO settingsDataSo;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterInstance(audioDataSo);
            builder.RegisterInstance(sceneDataSo);
            builder.RegisterInstance(poolingDataSo);
            builder.RegisterInstance(settingsDataSo);

            builder.Register<AudioService>(Lifetime.Singleton);
            builder.Register<PoolingService>(Lifetime.Singleton);
            builder.Register<SaveLoadSystem>(Lifetime.Singleton);
            
            builder.Register<SettingsModel>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<SettingsView>();
            builder.Register<SettingsController>(Lifetime.Singleton) // Singleton is recommended for application-level controllers
                .AsImplementedInterfaces() // Include any interfaces like IInitializable
                .AsSelf();
            
            builder.Register<ISceneService, SceneService>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<LoadingScreenController>();
            
            // Register the generated Controls class as a singleton
            builder.Register<Controls>(Lifetime.Singleton).AsSelf();
            
            // VContainer will instantiate this class and call Start() after binding.
            builder.RegisterEntryPoint<GameInitializer>();
        }

        public void Start()
        {
            Application.targetFrameRate = 60;
        }
    }
}