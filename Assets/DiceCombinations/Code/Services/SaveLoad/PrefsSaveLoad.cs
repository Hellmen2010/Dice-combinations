using DiceCombinations.Code.Data.Progress;
using DiceCombinations.Code.Extensions;
using UnityEngine;

namespace DiceCombinations.Code.Services.SaveLoad
{
    public class PrefsSaveLoad : ISaveLoad
    {
        private const string ProgressKey = "Progress";

        public void SaveProgress(PlayerProgress progress) => 
            PlayerPrefs.SetString(ProgressKey, progress.ToJson());

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
    }
}