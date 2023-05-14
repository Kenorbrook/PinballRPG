using System.Collections;
using System.Collections.Generic;
using Script.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    public static AsyncOperation[] LoadingGameScene;
    // Start is called before the first frame update

    [SerializeField]
    private Text _redMoney;

    [SerializeField]
    private Text _version;

    [SerializeField]
    private Text _record;

    void Start()
    {
        _version.text = Application.version;
        _record.text = GameManager.Record.ToString();
        LoadingGameScene = new AsyncOperation[SceneManager.sceneCountInBuildSettings];

        LoadingGameScene[1] = SceneManager.LoadSceneAsync(1);
        LoadingGameScene[1].allowSceneActivation = false;
    }
}