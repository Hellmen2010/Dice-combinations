using DiceCombinations.Code.Core.FieldInfo;
using DiceCombinations.Code.Core.GameBalance;
using DiceCombinations.Code.Core.UI.Buttons;
using DiceCombinations.Code.Core.UI.Popup;
using DiceCombinations.Code.Infrastructure.StateMachine.GameStateMachine;
using DiceCombinations.Code.Services.EntityContainer;

namespace DiceCombinations.Code.Infrastructure.StateMachine.States
{
    public class IdleState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IEntityContainer _entityContainer;
        private UserBetBalance _userBetBalance;
        private ClearButton _clearButton;
        private SpinButton _spinButton;
        private WinPopup _winPopup;
        private FieldDicesSum _filedDicesSum;
        private TableChipsSumView _tableChipsSumView;
        private BackButton _backButton;

        public IdleState(IGameStateMachine stateMachine, IEntityContainer entityContainer)
        {
            _stateMachine = stateMachine;
            _entityContainer = entityContainer;
        }

        public void Enter()
        {
            CacheEntities();
            Subscribe();
            SetupView();
        }

        private void SetupView()
        {
            if (_userBetBalance.Balance.Value < 5) _userBetBalance.SetBalanceToDefault();
            _userBetBalance.ClearBet();
            _winPopup.Hide();
            _filedDicesSum.ResetValues();
            _tableChipsSumView.ResetSum();
        }

        public void Exit()
        {
            UnSubscribe();
        }

        private void CacheEntities()
        {
            _userBetBalance = _entityContainer.GetEntity<UserBetBalance>();
            _clearButton = _entityContainer.GetEntity<ClearButton>();
            _spinButton = _entityContainer.GetEntity<SpinButton>();
            _backButton = _entityContainer.GetEntity<BackButton>();
            _winPopup = _entityContainer.GetEntity<WinPopup>();
            _filedDicesSum = _entityContainer.GetEntity<FieldDicesSum>();
            _tableChipsSumView = _entityContainer.GetEntity<TableChipsSumView>();
        }

        private void Subscribe()
        {
            _userBetBalance.SubscribeChips();
            _clearButton.OnButtonClicked += _userBetBalance.ReturnBet;
            _spinButton.OnButtonClicked += TryMoveToSpin;
            _backButton.OnButtonClicked += MoveToMenu;
        }

        private void UnSubscribe()
        {
            _userBetBalance.UnSubscribeChips();
            _clearButton.OnButtonClicked -= _userBetBalance.ReturnBet;
            _spinButton.OnButtonClicked -= TryMoveToSpin;
            _backButton.OnButtonClicked -= MoveToMenu;
        }

        private void TryMoveToSpin()
        {
            if(_userBetBalance.Bet.Value <= 0) return;
            _stateMachine.Enter<SpinState, int>(_userBetBalance.Bet.Value);
        }

        private void MoveToMenu() => _stateMachine.Enter<MenuState>();
    }
}