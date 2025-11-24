using VContainer.Unity;

namespace GameTemplate.Scripts.Systems.Audio
{
    /// <summary>
    /// Simple class to play menu theme on scene load
    /// </summary>
    public class MenuMusicStarter : IStartable
    {
        private readonly AudioService _audioService;

        // 💡 Dependencies are injected via the constructor
        public MenuMusicStarter(AudioService audioService)
        {
            _audioService = audioService;
        }

        // 💡 VContainer calls this method after all bindings are resolved.
        public void Start()
        {
            // Place your logic here to start the music.
            // This happens immediately after the container is built.
            _audioService.PlayMusic(AudioID.MenuMusic, true, true); 
        }
    }
}
