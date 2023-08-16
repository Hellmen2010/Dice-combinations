using System;
using DiceCombinations.Code.Data.StaticData;

namespace DiceCombinations.Code.Data.Progress
{
    [Serializable]
    public class PlayerProgress
    {
        public int Balance;
        public Settings Settings;

        public PlayerProgress(GameConfig config)
        {
            Settings = new Settings();
            Balance = config.StartScore;
        }
    }
}