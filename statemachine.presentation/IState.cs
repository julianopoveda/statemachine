namespace statemachine.presentation;

public interface IState
{
    public IState NextState { get; set; }

    public IEnumerable<IState> NextStatesAllowed { get; set; }

    (IState nextState, object outputContext) Execute(object inputContext);
}