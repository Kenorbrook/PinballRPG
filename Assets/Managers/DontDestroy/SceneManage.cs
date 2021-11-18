using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManage : MonoBehaviour
{

    public void LoadScene(int i)
    {
       
            SceneManager.LoadScene(i);

    } 
    public void LoadSceneAsync(int i)
    {

        MenuManager.LoadingGameScene[i].allowSceneActivation = true;

    }
}
