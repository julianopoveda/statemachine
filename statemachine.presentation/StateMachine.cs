namespace statemachine.presentation;

public class StateMachine
{
    private IState CurrentState { get; set; }

    public StateMachine(IState firstState) => CurrentState = firstState;

    public void Start()
    {
        object context = null;
        
        while (CurrentState != null)
        {
            (CurrentState, context) = CurrentState.Execute(context);
        }
    }
}