namespace statemachine.presentation;

public interface IState
{
    public Type NextState { get; set; }

    public IEnumerable<Type> NextStatesAllowed { get; set; }

    (Type nextState, object outputContext) Execute(object inputContext);
}