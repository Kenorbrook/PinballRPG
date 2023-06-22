using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ProjectFiles.MainMenu
{
    public class SceneLoaderFromMenu : MonoBehaviour
    {
        private const string GAME_SCENE = "Game";
        private static AsyncOperation _loadingGameScene;

        [SerializeField]
        private Text _version;

        public static GameStateMachine stateMachine;

        private void Start()
        {
            _version.text = Application.version;
            //LoadGameSceneAsync();
        }

        private static void LoadGameSceneAsync()
        {
            _loadingGameScene = SceneManager.LoadSceneAsync(GAME_SCENE);
            _loadingGameScene.allowSceneActivation = false;
        }

        public void LoadGameScene()
        {
            stateMachine.Enter<LoadLevelState, string>(GAME_SCENE);
        }
    }
}