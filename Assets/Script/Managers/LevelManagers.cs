using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

namespace Script.Managers
{
    public class LevelManagers : MonoBehaviour
    {
        [SerializeField]
        private Transform Levels;
        public static BoxCollider2D NewLevelCollider;
        public static bool isBossFight;
        private void Awake()
        {
            NewLevelCollider = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>() == null) return;
            //Player.player.TrailEmmiting();
            if (Camera.main == null) throw new ApplicationException("Camera is missing");
            Level _newLevel;
            if (GameManager.Level % 2 != 0 || GameManager.Level == 0)
            {
                isBossFight = false;
                Random _rand = new Random(DateTime.Now.Millisecond);
                int _level = _rand.Next(0,GameManager.Levels.Length);
                GetComponent<BoxCollider2D>().enabled = false;
                _newLevel = Instantiate(GameManager.Levels[_level],new Vector3(0,Camera.main.transform.position.y+GameManager.Levels[_level].height,0) ,new Quaternion(),Levels);
            }
            else
            {
                isBossFight = true;
                Random _rand = new Random(DateTime.Now.Millisecond);
                int _level = _rand.Next(0,GameManager.Bosses.Length);
                GetComponent<BoxCollider2D>().enabled = false;
                _newLevel = Instantiate(GameManager.Bosses[_level],new Vector3(0,Camera.main.transform.position.y+GameManager.Bosses[_level].height,0) ,new Quaternion(),Levels);

            }
            
            GameManager.CurrentLevels.Add(_newLevel);
            GameManager.Level++;
            StartCoroutine(Camera.main.GetComponent<CameraManager>().MoveCamera());
            Destroy(GameManager.CurrentLevels[GameManager.Level - 1].gameObject, 3f);
        }

   
    }
}
