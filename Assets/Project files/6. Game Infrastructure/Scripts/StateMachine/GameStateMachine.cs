using System;
using System.Collections.Generic;

public class GameStateMachine
{
    private readonly Dictionary<Type, IExitState> _states;
    private IExitState _activeState;

    public GameStateMachine(SceneLoader sceneLoader, LoadingScreen loadingScreen, AllServices allServices)
    {
        _states = new Dictionary<Type, IExitState>()
        {
            [typeof(BootstrapState)] = new BootstrapState(sceneLoader, allServices, this),
            [typeof(LoadLevelState)] = new LoadLevelState(sceneLoader, loadingScreen, allServices, this),
            [typeof(MainMenuState)] = new MainMenuState(sceneLoader, loadingScreen, allServices, this),
            [typeof(GameSceneState)] = new GameSceneState(sceneLoader, loadingScreen, allServices, this),
        };
    }


    public void Enter<TState>() where TState : class, IState
    {
        var _state = ChangeState<TState>();
        _state.Enter();
    }


    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
    {
        var _state = ChangeState<TState>();
        _state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitState
    {
        _activeState?.Exit();
        TState _state = GetState<TState>();
        _activeState = _state;
        return _state;
    }

    private TState GetState<TState>() where TState : class, IExitState => _states[typeof(TState)] as TState;
}