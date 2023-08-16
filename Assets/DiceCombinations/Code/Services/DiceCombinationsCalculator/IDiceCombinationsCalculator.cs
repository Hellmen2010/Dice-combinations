using DiceCombinations.Code.Data.StaticData.GameRules;
using DiceCombinations.Code.Infrastructure.ServiceContainer;

namespace DiceCombinations.Code.Services.DiceCombinationsCalculator
{
    public interface IDiceCombinationsCalculator : IService
    {
        void GetRoundDiceCombinations(RoundResult roundResult, out int[] playerDices, out int[] dealerDices);
    }
}