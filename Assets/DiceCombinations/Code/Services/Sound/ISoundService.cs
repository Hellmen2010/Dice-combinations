using DiceCombinations.Code.Data.Enums;
using DiceCombinations.Code.Data.Progress;
using DiceCombinations.Code.Data.StaticData.Sounds;
using DiceCombinations.Code.Infrastructure.ServiceContainer;

namespace DiceCombinations.Code.Services.Sound
{
    public interface ISoundService : IService
    {
        bool EffectsMuted { get; set; }
        void Construct(SoundData soundData, Settings userSettings);
        void PlayBackgroundMusic();
        void PlayEffectSound(SoundId soundId);
        void SetVolume(float volume);
    }
}