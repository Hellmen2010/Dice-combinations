using DiceCombinations.Code.Data.StaticData;
using DiceCombinations.Code.Data.StaticData.Chip;
using DiceCombinations.Code.Data.StaticData.Sounds;
using DiceCombinations.Code.Infrastructure.ServiceContainer;

namespace DiceCombinations.Code.Services.StaticData
{
    public interface IStaticData : IService
    {
        PrefabsData Prefabs { get; }
        DiceSpawnPositions DiceSpawnPositions { get; }
        GameConfig GameConfig { get; }
        ChipsConfig ChipsConfig { get; }
        SoundData SoundData { get; }
        void LoadStaticData();
    }
}