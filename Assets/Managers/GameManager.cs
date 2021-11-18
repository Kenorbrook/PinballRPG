using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool isGameStarted = false;
    public static GameObject[] Levels;
    public static int Level = 0;
    private void Awake()
    {
        Levels = GameObject.FindGameObjectsWithTag("Level");
    }
    public void ClickEvent()
    {
        
        if (!isGameStarted)
        {
            isGameStarted = true;
            _StartGame();
        }
        else
        {
            Player.player.GoUp();
        }
    }

    void _StartGame()
    {
        
        for(int i=0; i < FindObjectsOfType<MovebleInStart>().Length; i++)
        {
            FindObjectsOfType<MovebleInStart>()[i].Move();
        }

    }
}
