using System;
using DiceCombinations.Code.Data.Enums;
using DiceCombinations.Code.Services.PersistentProgress;
using DiceCombinations.Code.Services.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DiceCombinations.Code.Core.UI.SoundSettings
{
    public class SoundSettingsView : MonoBehaviour
    {
        public event Action<float> OnSliderDragged;
        
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _sliderHideButton;
        [SerializeField] private Slider _volumeSlider;
        private ISoundService _soundService;
        private IPersistentProgress _progress;

        public void Construct(ISoundService soundService, IPersistentProgress progress)
        {
            _soundService = soundService;
            _volumeSlider.value = progress.Progress.Settings.Volume;
            _progress = progress;
        }

        private void Start()
        {
            _settingsButton.onClick.AddListener(SwitchSliderView);
            _sliderHideButton.onClick.AddListener(HideSlider);
            _volumeSlider.onValueChanged.AddListener(VolumeChanged);
            SceneManager.activeSceneChanged += HideSlider;
        }

        private void HideSlider(Scene arg0, Scene arg1) => _volumeSlider.gameObject.SetActive(false);
        public void HideSlider() => _volumeSlider.gameObject.SetActive(false);

        private void SwitchSliderView()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            _volumeSlider.gameObject.SetActive(!_volumeSlider.gameObject.activeInHierarchy);
        }

        private void VolumeChanged(float value)
        {
            _progress.SaveVolume(value);
            OnSliderDragged?.Invoke(value);
        }

        private void OnDestroy()
        {
            _settingsButton.onClick.RemoveListener(SwitchSliderView);
            _volumeSlider.onValueChanged.RemoveListener(VolumeChanged);
            _sliderHideButton.onClick.RemoveListener(HideSlider);
            SceneManager.activeSceneChanged -= HideSlider;
        }
    }
}