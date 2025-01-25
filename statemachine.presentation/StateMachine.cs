using Microsoft.Extensions.DependencyInjection;

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
        Type nextState;

        while (CurrentState != null)
        {
            (nextState, context) = CurrentState.Execute(context);
            CurrentState = (IState)Services.GetService(nextState);
        }
    }
}