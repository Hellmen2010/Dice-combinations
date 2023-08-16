using DiceCombinations.Code.Data.Progress;
using DiceCombinations.Code.Infrastructure.ServiceContainer;

namespace DiceCombinations.Code.Services.SaveLoad
{
    public interface ISaveLoad : IService
    {
        void SaveProgress(PlayerProgress progress);
        PlayerProgress LoadProgress();
    }
}