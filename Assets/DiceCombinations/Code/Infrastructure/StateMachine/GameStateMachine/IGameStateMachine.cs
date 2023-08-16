using DiceCombinations.Code.Infrastructure.ServiceContainer;
using DiceCombinations.Code.Infrastructure.StateMachine.States;

namespace DiceCombinations.Code.Infrastructure.StateMachine.GameStateMachine
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}