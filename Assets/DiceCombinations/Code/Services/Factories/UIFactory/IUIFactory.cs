using DiceCombinations.Code.Core.Chip;
using DiceCombinations.Code.Core.UI.Info;
using DiceCombinations.Code.Data.Progress;
using DiceCombinations.Code.Infrastructure.ServiceContainer;
using UnityEngine;

namespace DiceCombinations.Code.Services.Factories.UIFactory
{
    public interface IUIFactory : IService
    {
        Transform CreateRootCanvas();
        void CreateBackButton(Transform persistantRoot);
        void CreateSoundSettings(Transform persistantRoot);
        void CreateMainMenu(Transform root);
        void CreateRules(Transform root);
        BalanceView CreateBalanceView(Transform root);
        BetView CreateBetView(Transform root);
        void CreateClearButton(Transform root);
        void CreateSpinButton(Transform root);
        void CreateWinPopup(Transform root);
        UIChipView[] CreateChips(Transform root);
        void CreateBalanceChanger(BalanceView balanceView, BetView betView, UIChipView[] chips, PlayerProgress progressProgress);
    }
}