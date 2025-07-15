using UnityEngine;
using VContainer;

namespace GameTemplate.Systems.Audio
{
    /// <summary>
    /// Simple class to play game theme on scene load
    /// </summary>
    public class MenuMusicStarter : MonoBehaviour
    {
        // set whether theme should restart if already playing
        [SerializeField]
        bool m_Restart;
        
        AudioService _audioService;

        [Inject]
        public void Construct(AudioService audioService)
        {
            _audioService = audioService;
            _audioService.StartMenuThemeMusic(m_Restart);
        }
    }
}
