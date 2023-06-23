public class BootstrapState : IState
{
    private const string BOOT_SCENE = "BootScene";
    private const string MAIN_MENU_SCENE = "MainMenu";
    private readonly SceneLoader _sceneLoader;
    private readonly GameStateMachine _stateMachine;
    private readonly AllServices _allServices;

    public BootstrapState(SceneLoader sceneLoader, AllServices allServices, GameStateMachine stateMachine)
    {
        _sceneLoader = sceneLoader;
        _stateMachine = stateMachine;
        _allServices = allServices;
        RegisterServices();
    }

    public void Enter()
    {
        _sceneLoader.Load(BOOT_SCENE, EnterLoadLevel);
    }

    public void Exit()
    {
    }

    private void EnterLoadLevel() => _stateMachine.Enter<LoadLevelState, string>(MAIN_MENU_SCENE);

    private void RegisterServices()
    {
        _allServices.RegisterServiceAsSingle<IAssetProvider>(new AssetProvider());
        _allServices.RegisterServiceAsSingle<IAssetLevelProvider>(new AssetLevelProvider());
        _allServices.RegisterServiceAsSingle<ISceneLoader>(_sceneLoader);
        _allServices.RegisterServiceAsSingle<IFactory>(new LevelFactory(_allServices.GetSingle<IAssetProvider>()));
        _allServices.RegisterServiceAsSingle<ILevelConstructFactory>(
            new LevelConstructFactory(_allServices.GetSingle<IAssetLevelProvider>()));
    }
}