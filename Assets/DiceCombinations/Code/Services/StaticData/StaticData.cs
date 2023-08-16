using System.Linq;
using DiceCombinations.Code.Data.StaticData;
using DiceCombinations.Code.Data.StaticData.Chip;
using DiceCombinations.Code.Data.StaticData.Sounds;
using DiceCombinations.Code.Services.StaticData.StaticDataProvider;

namespace DiceCombinations.Code.Services.StaticData
{
    public class StaticData : IStaticData
    {
        public PrefabsData Prefabs { get; private set; }
        public DiceSpawnPositions DiceSpawnPositions { get; private set; }
        public GameConfig GameConfig { get; private set; }
        public ChipsConfig ChipsConfig { get; private set; }
        public SoundData SoundData { get; private set; }
        
        private readonly IStaticDataProvider _staticDataProvider;

        public StaticData(IStaticDataProvider staticDataProvider) => _staticDataProvider = staticDataProvider;

        public void LoadStaticData()
        {
            Prefabs = _staticDataProvider.LoadPrefabsData();
            DiceSpawnPositions = _staticDataProvider.LoadDiceSpawnPositions();
            GameConfig = _staticDataProvider.LoadGameConfig();
            ChipsConfig = _staticDataProvider.LoadChipsConfig();
            SoundData = _staticDataProvider.LoadSoundData();

            SortArrays();
        }

        private void SortArrays()
        {
            GameConfig.PayoutRules = GameConfig.PayoutRules.OrderBy(t => t.MinDicesSum).ToArray();
            ChipsConfig.ChipsData = ChipsConfig.ChipsData.OrderBy(t => t.Value).ToArray();
        }
    }
}