using DiceCombinations.Code.Infrastructure.StateMachine.GameStateMachine;
using DiceCombinations.Code.Services.Factories.UIFactory;
using DiceCombinations.Code.Services.PersistentProgress;
using DiceCombinations.Code.Services.Sound;
using DiceCombinations.Code.Services.StaticData;
using UnityEngine;

namespace DiceCombinations.Code.Infrastructure.StateMachine.States
{
    public class CreatePersistentEntitiesState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IUIFactory _uiFactory;
        private readonly ISoundService _soundService;
        private readonly IStaticData _staticData;
        private readonly IPersistentProgress _progress;

        public CreatePersistentEntitiesState(IGameStateMachine stateMachine, IUIFactory uiFactory, 
            ISoundService soundService, IStaticData staticData, IPersistentProgress progress)
        {
            _stateMachine = stateMachine;
            _uiFactory = uiFactory;
            _soundService = soundService;
            _staticData = staticData;
            _progress = progress;
        }
        
        public void Enter()
        {
            CreatePersistantUI();
            _soundService.Construct(_staticData.SoundData, _progress.Progress.Settings);
            _soundService.PlayBackgroundMusic();
            MoveToMenu();
        }

        public void Exit()
        {
        }

        private void CreatePersistantUI()
        {
            Transform persistantRoot = _uiFactory.CreateRootCanvas();
            Object.DontDestroyOnLoad(persistantRoot);
            persistantRoot.GetComponent<Canvas>().sortingOrder = 10;
            _uiFactory.CreateBackButton(persistantRoot);
            _uiFactory.CreateSoundSettings(persistantRoot);
        }

        private void MoveToMenu() => _stateMachine.Enter<MenuState>();
    }
}