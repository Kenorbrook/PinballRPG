using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public static AsyncOperation[] LoadingGameScene;
    // Start is called before the first frame update
  
    void Start()
    {
        LoadingGameScene = new AsyncOperation[SceneManager.sceneCountInBuildSettings];
        for (int i = 1; i < LoadingGameScene.Length; i++)
        {
            LoadingGameScene[i] = SceneManager.LoadSceneAsync(1);
            LoadingGameScene[i].allowSceneActivation = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
