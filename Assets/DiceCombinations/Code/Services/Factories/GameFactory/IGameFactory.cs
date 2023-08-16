using DiceCombinations.Code.Infrastructure.ServiceContainer;

namespace DiceCombinations.Code.Services.Factories.GameFactory
{
    public interface IGameFactory : IService
    {
        void CreateDices();
        void CreateGameplayCalculator();
        void CreateTableChips();
        void CreateFieldDicesSumInfo();
    }
}