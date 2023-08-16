using DiceCombinations.Code.Data.Progress;
using DiceCombinations.Code.Services.SaveLoad;

namespace DiceCombinations.Code.Services.PersistentProgress
{
    public class PersistentPlayerProgress : IPersistentProgress
    {
        public PlayerProgress Progress { get; set; }
        
        private readonly ISaveLoad _saveLoad;

        public PersistentPlayerProgress(ISaveLoad saveLoad)
        {
            _saveLoad = saveLoad;
        }

        public void SaveVolume(float volume)
        {
            Progress.Settings.Volume = volume;
            SaveProgress();
        }

        public void SaveBalance(int value)
        {
            Progress.Balance = value;
            SaveProgress();
        }

        private void SaveProgress() => _saveLoad.SaveProgress(Progress);
    }
}