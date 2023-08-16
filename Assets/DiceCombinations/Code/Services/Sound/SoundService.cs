using System.Collections.Generic;
using System.Linq;
using DiceCombinations.Code.Data.Enums;
using DiceCombinations.Code.Data.Progress;
using DiceCombinations.Code.Data.StaticData.Sounds;
using UnityEngine;

namespace DiceCombinations.Code.Services.Sound
{
    public class SoundService : MonoBehaviour, ISoundService
    {
        public bool EffectsMuted
        {
            get => _effectsSource.mute;
            set
            {
                _effectsSource.mute = !value;
                _musicSource.mute = !value;
            }
        }

        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _effectsSource;
        
        private Dictionary<SoundId, AudioClipData> _sounds;
        
        private void Awake() => DontDestroyOnLoad(this);

        public void Construct(SoundData soundData, Settings userSettings)
        {
            _sounds = soundData.AudioEffectClips.ToDictionary(s => s.Id);
            _musicSource.clip = soundData.BackgroundMusic;
        }

        public void PlayBackgroundMusic() => _musicSource.Play();

        public void PlayEffectSound(SoundId soundId) =>
            _effectsSource.PlayOneShot(_sounds[soundId].Clip);

        public void SetVolume(float volume)
        {
            _effectsSource.volume = volume;
            _musicSource.volume = volume;
        }
    }
}