using System;
using System.Collections.Generic;
using Script.Managers.IMovableInStart;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static PlayerData playerData;

        public static int RedMoney => playerData.redMoney;

        public static int Record
        {
            get => playerData.record;
            private set => playerData.record = value;
        }
        public static int StartScore {
            get => _startScore;
            set
            {
                _startScore = value;
                UpdateScoreTextGame();
                if (_startScore > Record)
                {
                    Record = _startScore;
                }
            }
        }
        private static int _startScore;
        public static GameManager instance;
        private static bool _isGameStarted;
        public static Level[] Levels;
        public static Level[] Bosses;
        public static List<Level> CurrentLevels;
        public static int Level = 0;

        public Slider bossSlider;
        
        [SerializeField]
        private Text _score;

        private static Text score;
        
        [SerializeField]
        private Image[] Health;
        private void Awake()
        {
            Level = 0;
            instance = this;
            Levels = Resources.LoadAll<Level>("Levels");
            Bosses = Resources.LoadAll<Level>("Bosses");
            score = _score;
        }

        private void Start()
        {
            StartScore = 0;
            LevelManagers.isBossFight = false;
            CurrentLevels = new List<Level> {FindObjectOfType(typeof(Level)) as Level};
        }

        public static void ClickEvent(bool isRight)
        {
        
            if (!_isGameStarted)
            {
                _isGameStarted = true;
                startGame();
            }
            Player.player.GoUp(isRight?1:-1);
        
        }

        private static void UpdateScoreTextGame()
        {
            if (score == null) return;
            score.text = StartScore.ToString();
        }
#if UNITY_EDITOR
        private bool go = false;
        private void Update()
        {
           if( Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                go = false;
            }
            if (go) return;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                go = true;
                if (!_isGameStarted)
                {
                    _isGameStarted = true;
                    startGame();
                }
                Player.player.GoUp(1);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                go = true;
                if (!_isGameStarted)
                {
                    _isGameStarted = true;
                    startGame();
                }
                Player.player.GoUp(-1);
            }
        }
#endif
        public void UpdateUiHp()
        {
            int _i = 1;
            foreach (var _health in Health)
            {
                if (_i <= Player.GetHp())
                {
                    _health.enabled = true;
                }
                else
                {
                    _health.enabled = false;
                    return;
                }

                _i++;
            }
        }
        
        private static void startGame()
        {
            var _movable = FindObjectsOfType<MovableInStart>();
            foreach (var _t in _movable)
            {
                _t.Move();
            }
        }

        public static void LoadMenu()
        {
            _isGameStarted = false;
            SceneManage.LoadScene(0);
        }

        public static void CreateNewLevel()
        {
            
        }
    }
}

public class PlayerData
{
    public int redMoney;
    public int record;
}
