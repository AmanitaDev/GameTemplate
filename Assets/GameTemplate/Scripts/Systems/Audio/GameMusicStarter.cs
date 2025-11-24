using VContainer.Unity;

namespace GameTemplate.Scripts.Systems.Audio
{
    public class GameMusicStarter : IStartable
    {
        private readonly AudioService _audioService;

        // 💡 Dependencies are injected via the constructor
        public GameMusicStarter(AudioService audioService)
        {
            _audioService = audioService;
        }

        // 💡 VContainer calls this method after all bindings are resolved.
        public void Start()
        {
            // Place your logic here to start the music.
            // This happens immediately after the container is built.
            _audioService.PlayMusic(AudioID.GameMusic, true, true); 
        }
    }
}