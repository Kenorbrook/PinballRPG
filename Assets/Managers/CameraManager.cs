using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{
    [SerializeField] float speedChangeLevel;
    [SerializeField] GameObject BackGround;
    private void Awake()
    {
         float ratio = 1f* Screen.height / Screen.width;
         float CurrentHight = 10f/1.7f* ratio * BackGround.GetComponent<SpriteRenderer>().sprite.rect.size.x; 
         float ortSize = CurrentHight / 200f;
        Camera.main.orthographicSize = ortSize;
        
    }

    public IEnumerator MoveCamera()
    {
        while (Camera.main.transform.position.y < GameManager.Levels[GameManager.Level].transform.position.y)
        {
            Camera.main.transform.position += new Vector3(0, speedChangeLevel, 0);
            yield return new WaitForEndOfFrame();
        }
        // Player.player.TrailEmmiting();
        yield return null;
    }
    //  Camera Maincamera;
    // Start is called before the first frame update
    /* {
         Maincamera = GetComponent<Camera>();
     }

     // Update is called once per frame
     void Update()
     {
         Debug.Log(Player.player.StartPos);
         Debug.Log(Player.player.transform.localPosition.y);
         if (Mathf.Abs(Player.player.transform.localPosition.y - Player.player.StartPos) > 10f)
         {
             for (int i = 0; i < GameManager.Levels.Length; i++)
             {
                 GameManager.Levels[i].transform.localPosition -= new Vector3(0, 1f, 0);

             }

         }
     }*/
}
