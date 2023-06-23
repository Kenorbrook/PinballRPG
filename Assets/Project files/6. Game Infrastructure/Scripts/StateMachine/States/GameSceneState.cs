using ProjectFiles.LevelInfrastructure;
using UnityEngine;

public class GameSceneState : IState
{
    private static bool isTutorial
    {
        get => bool.Parse(PlayerPrefs.GetString("Tutorial", false.ToString()));
        set => PlayerPrefs.SetString("Tutorial", value.ToString());
    }

    private readonly LoadingScreen _loadingScreen;
    private readonly GameStateMachine _stateMachine;
    private readonly AllServices _allServices;

    public GameSceneState(SceneLoader sceneLoader, LoadingScreen loadingScreen, AllServices allServices,
        GameStateMachine stateMachine)
    {
        _loadingScreen = loadingScreen;
        _stateMachine = stateMachine;
        _allServices = allServices;
    }

    public void Enter()
    {
        OnLoaded();
        _loadingScreen.Hide();
    }

    private void OnLoaded()
    {
        GameObject initialPoint = GameObject.FindGameObjectWithTag("PlayerSpawn");
        GameObject levelContainer = GameObject.FindGameObjectWithTag("LevelContainer");
        var gameFactory = _allServices.GetSingle<IFactory>();
        var levelFactory = _allServices.GetSingle<ILevelConstructFactory>();
        GameObject _hud = gameFactory.CreateHud();
        gameFactory.CreatePlayer(initialPoint).@interface = _hud.GetComponent<PlayerGameInterface>();
        GameManager.StartScore = 0;
        LevelConstructor.Construct(_hud.GetComponent<BossInterface>());
        gameFactory.CreateSkillData();
        if (isTutorial)
            levelFactory.CreateStartLevel(levelContainer.transform);
        else
        {
            isTutorial = true;
            levelFactory.CreateTutorialLevels(levelContainer.transform);
        }
    }


    public void Exit()
    {
    }
}