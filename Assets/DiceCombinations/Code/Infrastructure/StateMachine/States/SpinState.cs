using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DiceCombinations.Code.Core.Dice;
using DiceCombinations.Code.Core.FieldInfo;
using DiceCombinations.Code.Core.GameBalance;
using DiceCombinations.Code.Core.UI.Popup;
using DiceCombinations.Code.Data.StaticData.GameRules;
using DiceCombinations.Code.Infrastructure.StateMachine.GameStateMachine;
using DiceCombinations.Code.Services.DiceCombinationsCalculator;
using DiceCombinations.Code.Services.EntityContainer;
using DiceCombinations.Code.Services.PersistentProgress;
using DiceCombinations.Code.Services.StaticData;

namespace DiceCombinations.Code.Infrastructure.StateMachine.States
{
    public class SpinState : IPayloadedState<int>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IEntityContainer _entityContainer;
        private readonly IPersistentProgress _progress;
        private DiceMover _diceMover;
        private UserBetBalance _userBetBalance;
        private WinPopup _winPopup;
        private IDiceCombinationsCalculator _diceCombinationsCalculator;
        private int[] _playerCombination;
        private int[] _dealerCombination;
        private RoundCalculations _roundCalculations;
        private FieldDicesSum _filedDicesSum;

        public SpinState(IGameStateMachine stateMachine, IEntityContainer entityContainer, IPersistentProgress progress, 
            IStaticData staticData, IDiceCombinationsCalculator diceCombinationsCalculator)
        {
            _diceCombinationsCalculator = diceCombinationsCalculator;
            _stateMachine = stateMachine;
            _entityContainer = entityContainer;
            _progress = progress;
            _playerCombination = new int[staticData.DiceSpawnPositions.PlayerDices.Length];
            _dealerCombination = new int[staticData.DiceSpawnPositions.DealerDices.Length];
        }

        public async void Enter(int bet)
        {
            CacheEntities();
            Subscribe();
            RoundResult roundResult = await GameLoop(bet);
            SetWin(roundResult.WinAmount);
            ShowPopup(roundResult);
            SaveRoundResults();
        }

        private async UniTask<RoundResult> GameLoop(int bet)
        {
            RoundResult roundResult = _roundCalculations.CalculateRoundResult();
            await Spin(roundResult);
            roundResult.WinAmount = CalculateWin(bet, roundResult);
            return roundResult;
        }

        public void Exit() => UnSubscribe();

        private async UniTask Spin(RoundResult roundResult)
        {
            _diceCombinationsCalculator.GetRoundDiceCombinations(roundResult, out _playerCombination, out _dealerCombination);
            await _diceMover.Spin(
                _playerCombination, 
                _dealerCombination, 
                _filedDicesSum.SetPlayerValue, 
                _filedDicesSum.SetDealerValue);
        }

        private void Subscribe() => _winPopup.OnCloseButtonClicked += MoveToIdle;

        private void UnSubscribe() => _winPopup.OnCloseButtonClicked -= MoveToIdle;

        private int CalculateWin(int bet, RoundResult roundResult) => _roundCalculations.CalculateRoundWin(bet, roundResult.WinMulti);

        private void SetWin(int winAmount) => _userBetBalance.SetWin(winAmount);

        private void ShowPopup(RoundResult result)
        {
            _winPopup.SetText(result);
            _winPopup.Show();
        }

        private void CacheEntities()
        {
            _diceMover = _entityContainer.GetEntity<DiceMover>();
            _userBetBalance = _entityContainer.GetEntity<UserBetBalance>();
            _winPopup = _entityContainer.GetEntity<WinPopup>();
            _roundCalculations = _entityContainer.GetEntity<RoundCalculations>();
            _filedDicesSum = _entityContainer.GetEntity<FieldDicesSum>();
        }

        private void SaveRoundResults() => _progress.SaveBalance(_userBetBalance.Balance.Value);

        private void MoveToIdle() => _stateMachine.Enter<IdleState>();
    }
}