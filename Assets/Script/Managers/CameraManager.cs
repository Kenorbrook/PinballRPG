using System.Collections;
using UnityEngine;

namespace Script.Managers
{
    [RequireComponent(typeof(Camera))]
    public class CameraManager : MonoBehaviour
    {
        [SerializeField]
        private float _speedChangeLevel;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            float _ratio = 1f * Screen.height / Screen.width;
            if (_camera != null) _camera.orthographicSize = 2.5f * _ratio;
            /* float _currentHeight = 10f/1.7f* _ratio; 
             float _ortSize = _currentHeight / 200f;
             _camera.orthographicSize = _ortSize;*/
        }


        public IEnumerator MoveCamera()
        {
            while (_camera.transform.position.y < GameManager.CurrentLevels[GameManager.Level].transform.position.y)
            {
                _camera.transform.position += new Vector3(0, _speedChangeLevel, 0);
                if (Player.player.transform.position.y < transform.position.y - _camera.orthographicSize + 0.5f)
                {
                    Player.player.transform.position += new Vector3(0, _speedChangeLevel, 0);
                }

                yield return null;
            }

            if (LevelManagers.isBossFight)
            {
                GameManager.CurrentLevels[GameManager.Level]._enemies[0].GetComponent<IBoss>().StartAnim();
            }
            Player.player.TrailEmitting();
            if (Player.player.transform.position.y < transform.position.y - _camera.orthographicSize + 0.5f)
            {
                Player.player.transform.position =
                    new Vector3(0, transform.position.y - _camera.orthographicSize + 0.5f, 0);
            }
            Player.player.TrailEmitting();
            // Player.player.TrailEmitting();
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
}