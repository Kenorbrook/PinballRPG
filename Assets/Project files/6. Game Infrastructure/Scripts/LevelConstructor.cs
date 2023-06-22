using System;
using TMPro;
using UnityEngine;
using Random = System.Random;

namespace ProjectFiles.LevelInfrastructure
{
    public class LevelConstructor : MonoBehaviour
    {
        [SerializeField]
        private Transform Levels;

        public static BoxCollider2D NewLevelCollider;
        public static bool isBossFight;
        private static Random Rand => new Random(DateTime.Now.Millisecond);

        private Camera _mainCamera;
        private static bool isBossLevel => GameManager.Level % FREQUENCY_BOSS_ROOM == 0 && GameManager.Level > 0;

        private const int FREQUENCY_BOSS_ROOM = 2;

        private readonly ILevelConstructFactory _levelFactory = AllServices.Container.GetSingle<ILevelConstructFactory>();
        private void Awake()
        {
            InitDefaultValue();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Player.Player>() == null) return;
            MoveCameraOnNewLevel();
        }

        private void InitDefaultValue()
        {
            _mainCamera = Camera.main;
            NewLevelCollider = GetComponent<BoxCollider2D>();
            isBossFight = false;
        }


        private void MoveCameraOnNewLevel()
        {
            if (_mainCamera == null) throw new ApplicationException("Camera is missing");
            GameManager.Level++;
            NewLevelCollider.enabled = false;

            //isBossFight = isBossLevel;
            if (false) _levelFactory.CreateRandomBossLevel(Levels); else _levelFactory.CreateRandomLevel(Levels);

            StartCoroutine(_mainCamera.GetComponent<TransformationCamera>().MoveCamera());
            //DestroyPreviousLevel();
        }


        private static void DestroyPreviousLevel() =>
            Destroy(GameManager.CurrentLevels[GameManager.Level - 1].gameObject, 3f);

       
    }
}