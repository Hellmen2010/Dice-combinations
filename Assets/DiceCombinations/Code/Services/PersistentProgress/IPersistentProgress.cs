using DiceCombinations.Code.Data.Progress;
using DiceCombinations.Code.Infrastructure.ServiceContainer;

namespace DiceCombinations.Code.Services.PersistentProgress
{
    public interface IPersistentProgress : IService
    {
        PlayerProgress Progress { get; set; }
        void SaveVolume(float volume);
        void SaveBalance(int value);
    }
}