using System;
using DiceCombinations.Code.Services.EntityContainer;
using DiceCombinations.Code.Services.PersistentProgress;
using DiceCombinations.Code.Services.Sound;

namespace DiceCombinations.Code.Core.UI.SoundSettings
{
    public class SoundSettings : IDisposable, IFactoryEntity
    {
        private readonly SoundSettingsView _view;
        private readonly ISoundService _soundService;
        private readonly IPersistentProgress _progress;

        public SoundSettings(SoundSettingsView view, ISoundService soundService, IPersistentProgress progress)
        {
            _view = view;
            _soundService = soundService;
            _progress = progress;
            Subscribe();
        }

        private void Subscribe() => _view.OnSliderDragged += VolumeChanged;

        private void UnSubscribe() => _view.OnSliderDragged += VolumeChanged;

        private void VolumeChanged(float value)
        {
            _soundService.SetVolume(value);
            SaveSettings(value);
        }

        private void SaveSettings(float value) => _progress.SaveVolume(value);

        public void Dispose() => UnSubscribe();
    }
}