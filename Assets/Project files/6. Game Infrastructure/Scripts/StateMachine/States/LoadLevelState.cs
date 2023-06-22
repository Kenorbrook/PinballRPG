public class LoadLevelState : IPayloadState<string>
{
    private const string MAIN_MENU_SCENE = "MainMenu";
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _allServices;
    private readonly GameStateMachine _stateMachine;
    private readonly LoadingScreen _loadingScreen;

    public LoadLevelState(SceneLoader sceneLoader, LoadingScreen loadingScreen, AllServices allServices,
        GameStateMachine stateMachine)
    {
        _sceneLoader = sceneLoader;
        _allServices = allServices;
        _stateMachine = stateMachine;
        _loadingScreen = loadingScreen;
    }

    public void Enter(string payload)
    {
        _loadingScreen.Show();
        _sceneLoader.Load(payload, onLoaded: () => OnLoaded(payload == MAIN_MENU_SCENE));
    }

    private void OnLoaded(bool isLoadMenu)
    {
        if (isLoadMenu)
            _stateMachine.Enter<MainMenuState>();
        else

            _stateMachine.Enter<GameSceneState>();
    }

    public void Exit()
    {
    }
}