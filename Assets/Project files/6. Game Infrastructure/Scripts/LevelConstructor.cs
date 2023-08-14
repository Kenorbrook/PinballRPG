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
        private static bool isBossLevel => (GameManager.Level-_tutorialLevel) % FREQUENCY_BOSS_ROOM == 0 && GameManager.Level-_tutorialLevel > 0;

        private const int FREQUENCY_BOSS_ROOM = 4;

        private readonly ILevelConstructFactory _levelFactory =
            AllServices.Container.GetSingle<ILevelConstructFactory>();

        private static int _tutorialLevel;
        private void Awake()
        {
            InitDefaultValue();
        }

        private static BossInterface _bossInterface;

        public static void Construct(BossInterface bossInterface, int tutorialLevel =0)
        {
            _bossInterface = bossInterface;
            _tutorialLevel = tutorialLevel;
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
            NewLevelCollider.offset -= new Vector2(0,((float) Screen.height / Screen.width-2)>0?100:0);
            isBossFight = false;
        }


        private void MoveCameraOnNewLevel()
        {
            if (_mainCamera == null) throw new ApplicationException("Camera is missing");
            GameManager.Level++;
            NewLevelCollider.enabled = false;

            isBossFight = isBossLevel;
            if (GameManager.Level >= _levelFactory.GetLevelCount())
                if (isBossFight) _levelFactory.CreateRandomBossLevel(Levels, _bossInterface);
                else _levelFactory.CreateRandomLevel(Levels);

            StartCoroutine(_mainCamera.GetComponent<TransformationCamera>().MoveCamera());
            //DestroyPreviousLevel();
        }
    }
}