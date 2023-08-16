using DiceCombinations.Code.Infrastructure.StateMachine.GameStateMachine;
using DiceCombinations.Code.Infrastructure.StateMachine.States;
using DiceCombinations.Code.Services.CoroutineRunner;
using DiceCombinations.Code.Services.EntityContainer;
using DiceCombinations.Code.Services.Sound;
using UnityEngine;

namespace DiceCombinations.Code.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private SoundService _soundService;
        private GameStateMachine _gameStateMachine;
        
        private void Awake()
        {
            _gameStateMachine = new GameStateMachine(ServiceContainer.ServiceContainer.Container, this, _soundService);
            _gameStateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }

        private void OnDestroy() => ServiceContainer.ServiceContainer.Container.Single<IEntityContainer>().Dispose();
    }
}