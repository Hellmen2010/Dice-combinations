using DiceCombinations.Code.Data.StaticData;
using DiceCombinations.Code.Data.StaticData.Chip;
using DiceCombinations.Code.Data.StaticData.Sounds;
using UnityEngine;

namespace DiceCombinations.Code.Services.StaticData.StaticDataProvider
{
    public class StaticDataProvider : IStaticDataProvider
    {
        private const string PrefabsDataPath = "StaticData/PrefabsData";
        private const string SoundDataPath = "StaticData/SoundData";
        private const string DiceSpawnPositionsPath = "StaticData/DiceSpawnPositions";
        private const string GameConfigPath = "StaticData/GameConfig";
        private const string ChipsConfigPath = "StaticData/ChipsConfig";

        public PrefabsData LoadPrefabsData() => Resources.Load<PrefabsData>(PrefabsDataPath);

        public SoundData LoadSoundData() => Resources.Load<SoundData>(SoundDataPath);

        public GameConfig LoadGameConfig() => Resources.Load<GameConfig>(GameConfigPath);

        public ChipsConfig LoadChipsConfig() => Resources.Load<ChipsConfig>(ChipsConfigPath);

        public DiceSpawnPositions LoadDiceSpawnPositions() => 
            Resources.Load<DiceSpawnPositions>(DiceSpawnPositionsPath);
    }
}