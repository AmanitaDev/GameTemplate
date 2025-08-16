using GameTemplate.Systems.Audio;
using UnityEngine;
using VContainer;

namespace GameTemplate.Scripts.Systems.Audio
{
    /// <summary>
    /// Simple class to play menu theme on scene load
    /// </summary>
    public class MenuMusicStarter : MonoBehaviour
    {
        // set whether theme should restart if already playing
        [SerializeField] bool restart;
        
        AudioService _audioService;

        [Inject]
        public void Construct(AudioService audioService)
        {
            _audioService = audioService;
            _audioService.StartMenuThemeMusic(restart);
        }
    }
}
