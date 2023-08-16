using DiceCombinations.Code.Core.Chip;
using DiceCombinations.Code.Core.UI.Buttons;
using DiceCombinations.Code.Core.UI.Info;
using DiceCombinations.Code.Infrastructure.StateMachine.GameStateMachine;
using DiceCombinations.Code.Services.EntityContainer;
using DiceCombinations.Code.Services.Factories.GameFactory;
using DiceCombinations.Code.Services.Factories.UIFactory;
using DiceCombinations.Code.Services.PersistentProgress;
using DiceCombinations.Code.Services.SceneLoader;
using UnityEngine;

namespace DiceCombinations.Code.Infrastructure.StateMachine.States
{
    public class GameCreationState : IState
    {
        private const string GameSceneName = "Game";
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgress _progress;
        private readonly IEntityContainer _entityContainer;

        public GameCreationState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IUIFactory uiFactory,
            IGameFactory gameFactory, IPersistentProgress progress, IEntityContainer entityContainer)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
            _progress = progress;
            _entityContainer = entityContainer;
        }

        public void Enter()
        {
            _sceneLoader.LoadScene(GameSceneName, OnSceneLoaded);
        }

        public void Exit()
        {
            
        }

        private void OnSceneLoaded()
        {
            CreateGameplayObjects();
            CreateUI();
            _entityContainer.GetEntity<BackButton>().Show();
            MoveToIdle();
        }

        private void CreateUI()
        {
            Transform root = _uiFactory.CreateRootCanvas();
            _uiFactory.CreateClearButton(root);
            _uiFactory.CreateSpinButton(root);
            BalanceView balanceView = _uiFactory.CreateBalanceView(root);
            BetView betView = _uiFactory.CreateBetView(root);
            UIChipView[] chips = _uiFactory.CreateChips(root);
            _uiFactory.CreateBalanceChanger(balanceView, betView, chips, _progress.Progress);
            _uiFactory.CreateWinPopup(root);
        }

        private void CreateGameplayObjects()
        {
            _gameFactory.CreateDices();
            _gameFactory.CreateGameplayCalculator();
            _gameFactory.CreateFieldDicesSumInfo();
            _gameFactory.CreateTableChips();
        }

        private void MoveToIdle() => _stateMachine.Enter<IdleState>();
    }
}