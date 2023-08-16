using DiceCombinations.Code.Data.StaticData;
using DiceCombinations.Code.Data.StaticData.Chip;
using DiceCombinations.Code.Data.StaticData.Sounds;
using DiceCombinations.Code.Infrastructure.ServiceContainer;

namespace DiceCombinations.Code.Services.StaticData.StaticDataProvider
{
    public interface IStaticDataProvider : IService
    {
        PrefabsData LoadPrefabsData();
        SoundData LoadSoundData();
        DiceSpawnPositions LoadDiceSpawnPositions();
        GameConfig LoadGameConfig();
        ChipsConfig LoadChipsConfig();
    }
}