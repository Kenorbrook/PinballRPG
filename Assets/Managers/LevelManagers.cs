using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagers : MonoBehaviour
{
   
    
    private void Start()
    {

       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>()!=null )
        {
            GameManager.Level++;
            //Player.player.TrailEmmiting();
            StartCoroutine(Camera.main.GetComponent<CameraManager>().MoveCamera());
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

   
}
