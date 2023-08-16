using DiceCombinations.Code.Data.Progress;
using DiceCombinations.Code.Infrastructure.StateMachine.GameStateMachine;
using DiceCombinations.Code.Services.PersistentProgress;
using DiceCombinations.Code.Services.SaveLoad;
using DiceCombinations.Code.Services.StaticData;

namespace DiceCombinations.Code.Infrastructure.StateMachine.States
{
    public class LoadProgressState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IPersistentProgress _playerProgress;
        private readonly ISaveLoad _saveLoadService;
        private readonly IStaticData _staticDataService;

        public LoadProgressState(IGameStateMachine stateMachine, IPersistentProgress playerProgress,
            ISaveLoad saveLoadService, IStaticData staticDataService)
        {
            _staticDataService = staticDataService;
            _saveLoadService = saveLoadService;
            _playerProgress = playerProgress;
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            LoadProgressOrInitNew();
            _stateMachine.Enter<CreatePersistentEntitiesState>();
        }

        public void Exit()
        {
        }
        
        private void LoadProgressOrInitNew() =>
            _playerProgress.Progress = _saveLoadService.LoadProgress() ?? new PlayerProgress(_staticDataService.GameConfig);
    }
}