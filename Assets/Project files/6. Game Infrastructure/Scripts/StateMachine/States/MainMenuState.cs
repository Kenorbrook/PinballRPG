using UnityEngine;

public class MainMenuState : IState
{
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingScreen _loadingScreen;
    private readonly GameStateMachine _stateMachine;
    private readonly AllServices _allServices;

    public MainMenuState(SceneLoader sceneLoader, LoadingScreen loadingScreen, AllServices allServices,
        GameStateMachine stateMachine)
    {
        _sceneLoader = sceneLoader;
        _loadingScreen = loadingScreen;
        _stateMachine = stateMachine;
        _allServices = allServices;
    }

    public void Enter()
    {
        _loadingScreen.Hide();
    }

    public void Exit()
    {
    }
}