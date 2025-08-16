using GameTemplate.Systems.Audio;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

namespace GameTemplate.Scripts.Systems.Audio
{
    public class GameMusicStarter : MonoBehaviour
    {
        /// <summary>
        /// Simple class to play game theme on scene load
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
                _audioService.StartGameThemeMusic(restart);
            }
        }
    }
}