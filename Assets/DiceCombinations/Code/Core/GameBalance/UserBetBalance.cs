using DiceCombinations.Code.Core.Chip;
using DiceCombinations.Code.Core.FieldInfo;
using DiceCombinations.Code.Core.UI.Info;
using DiceCombinations.Code.Data.Progress;
using DiceCombinations.Code.Data.StaticData;
using DiceCombinations.Code.Services.EntityContainer;
using UniRx;

namespace DiceCombinations.Code.Core.GameBalance
{
    public class UserBetBalance : IFactoryEntity
    {
        public IntReactiveProperty Balance = new ();
        public IntReactiveProperty Bet = new();
        private readonly BalanceView _balanceView;
        private readonly BetView _betView;
        private readonly UIChipView[] _chipViews;
        private readonly TableChipsSumView _tableChipsSumView;
        private readonly TableChipMover _tableChipMover;
        private readonly GameConfig _config;
        private CompositeDisposable _disposable = new ();

        public UserBetBalance(BalanceView balanceView, BetView betView, UIChipView[] chipViews, PlayerProgress progress, TableChipsSumView tableChipsSumView, 
            TableChipMover tableChipMover, GameConfig config)
        {
            _balanceView = balanceView;
            _betView = betView;
            _chipViews = chipViews;
            _tableChipsSumView = tableChipsSumView;
            _tableChipMover = tableChipMover;
            _config = config;
            SubscribeView();
            Balance.Value = progress.Balance;
        }

        public void ReturnBet()
        {
            Balance.Value += Bet.Value;
            Bet.Value = 0;
            _tableChipsSumView.ResetSum();
        }

        public void SubscribeChips()
        {
            foreach (UIChipView chip in _chipViews) 
                chip.OnChipClicked += TryChangeMoney;
        }

        public void UnSubscribeChips()
        {
            foreach (UIChipView chip in _chipViews) 
                chip.OnChipClicked -= TryChangeMoney;
        }

        public void ClearBet() => Bet.Value = 0;

        public void SetBalanceToDefault() => IncreaseBalance(_config.StartScore);

        public void SetWin(int value) => IncreaseBalance(value);

        private void SubscribeView()
        {
            Balance.Subscribe(t => { _balanceView.SetText(t.ToString()); }).AddTo(_disposable);
            Bet.Subscribe(t => { _betView.SetText(t.ToString()); }).AddTo(_disposable);
        }

        private void TryChangeMoney(UIChipView chip)
        {
            if(Balance.Value < chip.Value) return;
            MoveChip(chip);
            IncreaseBalance(-chip.Value);
            IncreaseBet(chip.Value);
        }

        private void MoveChip(UIChipView chip) => 
            _tableChipMover.MoveTableChipAnimated(chip, () => _tableChipsSumView.UpdateValue(chip.Value));

        private void IncreaseBet(int value) => Bet.Value += value;

        private void IncreaseBalance(int value) => Balance.Value += value;
    }
}