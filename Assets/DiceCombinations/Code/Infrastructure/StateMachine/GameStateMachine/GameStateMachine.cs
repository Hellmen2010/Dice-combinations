using System;
using System.Collections.Generic;
using DiceCombinations.Code.Infrastructure.StateMachine.States;
using DiceCombinations.Code.Services.CoroutineRunner;
using DiceCombinations.Code.Services.DiceCombinationsCalculator;
using DiceCombinations.Code.Services.EntityContainer;
using DiceCombinations.Code.Services.Factories.GameFactory;
using DiceCombinations.Code.Services.Factories.UIFactory;
using DiceCombinations.Code.Services.PersistentProgress;
using DiceCombinations.Code.Services.SaveLoad;
using DiceCombinations.Code.Services.SceneLoader;
using DiceCombinations.Code.Services.Sound;
using DiceCombinations.Code.Services.StaticData;

namespace DiceCombinations.Code.Infrastructure.StateMachine.GameStateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(
            ServiceContainer.ServiceContainer container,
            ICoroutineRunner coroutineRunner,
            ISoundService soundService)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, container, soundService, coroutineRunner),
                [typeof(LoadProgressState)] = new LoadProgressState(this, container.Single<IPersistentProgress>(),
                    container.Single<ISaveLoad>(), container.Single<IStaticData>()),
                [typeof(CreatePersistentEntitiesState)] = new CreatePersistentEntitiesState(this, container.Single<IUIFactory>(), 
                    soundService, container.Single<IStaticData>(), container.Single<IPersistentProgress>()),
                [typeof(MenuState)] = new MenuState(this, container.Single<ISceneLoader>(), container.Single<IEntityContainer>(), 
                    container.Single<IUIFactory>()),
                [typeof(GameCreationState)] = new GameCreationState(this, container.Single<ISceneLoader>(), 
                    container.Single<IUIFactory>(), container.Single<IGameFactory>(), container.Single<IPersistentProgress>(), 
                    container.Single<IEntityContainer>()),
                [typeof(IdleState)] = new IdleState(this, container.Single<IEntityContainer>()),
                [typeof(SpinState)] = new SpinState(this, container.Single<IEntityContainer>(), container.Single<IPersistentProgress>(), 
                    container.Single<IStaticData>(), container.Single<IDiceCombinationsCalculator>()),
            };
        }

        public void Enter<TState>() where TState : class, IState =>
            ChangeState<TState>().Enter();

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload> =>
            ChangeState<TState>().Enter(payload);

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
        
        ~GameStateMachine() => _activeState.Exit();

    }
}