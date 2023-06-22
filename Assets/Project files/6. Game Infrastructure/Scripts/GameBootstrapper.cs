using ProjectFiles.MainMenu;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    private GameStateMachine _stateMachine;

    [SerializeField]
    private LoadingScreen _loadingScreenPrefab;

    private void Awake()
    {
        _stateMachine = new GameStateMachine(new SceneLoader(this), Instantiate(_loadingScreenPrefab), AllServices.Container);
        SceneLoaderFromMenu.stateMachine = _stateMachine;
        _stateMachine.Enter<BootstrapState>();
        DontDestroyOnLoad(this);
    }
}