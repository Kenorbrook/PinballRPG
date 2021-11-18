using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOne : MonoBehaviour
{
    [SerializeField] GameObject[] ImageKilling = new GameObject[3];
    
    public void EnemyKilling()
    {
        int i = 0;
        bool flag = false;
        while (!flag)
        {
            if (!ImageKilling[i].activeSelf)
            {
                ImageKilling[i].SetActive(true);
                flag = true;
            }
            i++;
        }
        if(i == ImageKilling.Length)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
