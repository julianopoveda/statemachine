using Microsoft.Extensions.DependencyInjection;
using statemachine.presentation.DTO;

namespace statemachine.presentation;

public class StateMachine
{
    private IState CurrentState { get; set; }
    public IServiceProvider Services { get; }

    public StateMachine(IState firstState, IServiceProvider service)
    {
        CurrentState = firstState;
        Services = service;
    }


    public void Start()
    {
        object context = null;
        Type nextState = default;

        while (CurrentState != null)
        {
            (nextState, context) = CurrentState.Execute(context);
            CurrentState = nextState != null ? Services.GetService(nextState) as IState : null;
        }
    }
}