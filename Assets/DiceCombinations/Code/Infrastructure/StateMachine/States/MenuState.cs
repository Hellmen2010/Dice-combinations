using DiceCombinations.Code.Core.UI.Buttons;
using DiceCombinations.Code.Core.UI.MainMenu;
using DiceCombinations.Code.Infrastructure.StateMachine.GameStateMachine;
using DiceCombinations.Code.Services.EntityContainer;
using DiceCombinations.Code.Services.Factories.UIFactory;
using DiceCombinations.Code.Services.SceneLoader;
using UnityEngine;

namespace DiceCombinations.Code.Infrastructure.StateMachine.States
{
    public class MenuState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IEntityContainer _entityContainer;
        private readonly IUIFactory _uiFactory;
        private MainMenu _mainMenu;
        private RulesMenu _rulesMenu;
        private BackButton _backButton;

        private const string MenuScene = "Menu";

        public MenuState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IEntityContainer entityContainer, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _entityContainer = entityContainer;
            _uiFactory = uiFactory;
        }

        public void Enter() => _sceneLoader.LoadScene(MenuScene, SceneLoaded);

        private void SceneLoaded()
        {
            CreateUI();
            CacheEntities();
            Subscribe();
            _backButton.Hide();
        }

        private void CacheEntities()
        {
            _mainMenu = _entityContainer.GetEntity<MainMenu>();
            _rulesMenu = _entityContainer.GetEntity<RulesMenu>();
            _backButton = _entityContainer.GetEntity<BackButton>();
        }

        private void Subscribe()
        {
            _mainMenu.OnRulesButtonClicked += ShowRules;
            _mainMenu.OnPlayButtonClicked += MoveToGameCreation;
            _backButton.OnButtonClicked += HideRules;
        }

        private void UnSubscribe()
        {
            _mainMenu.OnRulesButtonClicked -= ShowRules;
            _mainMenu.OnPlayButtonClicked -= MoveToGameCreation;
            _backButton.OnButtonClicked -= HideRules;
        }

        private void HideRules()
        {
            _rulesMenu.HideView();
            _backButton.Hide();
        }

        private void ShowRules()
        {
            _rulesMenu.ShowView();
            _backButton.Show();
        }

        private void CreateUI()
        {
            Transform root = _uiFactory.CreateRootCanvas();
            _uiFactory.CreateMainMenu(root);
            _uiFactory.CreateRules(root);
        }

        public void Exit()
        {
            UnSubscribe();
        }

        private void MoveToGameCreation() => _stateMachine.Enter<GameCreationState>();
    }
}