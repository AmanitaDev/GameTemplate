using UnityEngine;
using GameTemplate.Systems.Audio;
using GameTemplate.Systems.Pooling;
using GameTemplate.Systems.Scene;
using GameTemplate.UI;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.Core.Scopes
{
    /// <summary>
    /// An entry point to the application, where we bind all the common dependencies to the root DI scope.
    /// </summary>
    public class ApplicationScope : LifetimeScope
    {
        [FormerlySerializedAs("audioData")] public AudioDataSO audioDataSo;
        public SceneData sceneData;
        public PoolingData poolingData;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterInstance(audioDataSo);
            builder.RegisterInstance(sceneData);
            builder.RegisterInstance(poolingData);

            builder.Register<SoundService>(Lifetime.Singleton);
            builder.Register<PoolingService>(Lifetime.Singleton);
            builder.Register<ISceneService, SceneService>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<LoadingScreenController>();
        }

        public void Start()
        {
            Application.targetFrameRate = 60;
            //SceneManager.LoadScene("MainMenu");
        }
    }
}