public interface IState : IExitState
{
    public void Enter();
}

public interface IExitState
{
    public void Exit();
}
public interface IPayloadState<TPayload> : IExitState
{
    public void Enter(TPayload payload);
}