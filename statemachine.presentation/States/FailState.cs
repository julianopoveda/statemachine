namespace statemachine.presentation.States;

public class FailState:IState
{
    public Type NextState { get; set; }
    public IEnumerable<Type> NextStatesAllowed { get; set; }

    public FailState()
    {
        NextState = null;
        NextStatesAllowed = null;
    }

    public (Type nextState, object outputContext) Execute(object inputContext)
    {
        Console.WriteLine("Fail");
        return (null, null);
    }
}