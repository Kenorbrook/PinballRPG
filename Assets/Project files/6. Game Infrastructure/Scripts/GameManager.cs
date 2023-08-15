using System;
using System.Collections.Generic;
using ProjectFiles.MainMenu;
using ProjectFiles.MovingObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ProjectFiles.LevelInfrastructure
{
    public class GameManager : GamePausedObject
    {
        public static PlayerData playerData;

        private const string MAIN_MENU_SCENE = "MainMenu";
        public static int Record
        {
            get => playerData.record;
            private set => playerData.record = value;
        }

        public static int StartScore
        {
            get => _startScore;
            set
            {
                _startScore = value;
                Player.Player.player.@interface.UpdatePoint(value);
                if (_startScore > Record)
                {
                    Record = _startScore;
                }
            }
        }

        private static int _startScore;
        public static GameManager instance;
        private static bool _isGameStarted;
        public static bool isReverse;

        public static List<Level> CurrentLevels =>
            ((LevelConstructFactory) AllServices.Container.GetSingle<ILevelConstructFactory>()).currentLevels;

        public static int Level;


        [SerializeField]
        private Text _score;

        private static Text score;


        [SerializeField]
        private Animation _animRight;

        [SerializeField]
        private Animation _animLeft;


        private void Awake()
        {
            Level = 0;
            instance = this;

            score = _score;
        }

        private void Start()
        {
            
            UnPause();
        }


        public static void ClickEvent(bool isRight)
        {
            
            if(isPause) return;
            if (!_isGameStarted)
            {
                _isGameStarted = true;
            }

            isRight = isReverse ? !isRight : isRight;
            Player.Player.player.GoUp(isRight ? 1 : -1);
            PlayHintAnim(isRight);
        }

        private static void PlayHintAnim(bool isRight)
        {
            var anim = isRight ? instance._animRight : instance._animLeft;

            anim.transform.position = Player.Player.player.transform.position;
            anim.Stop();
            anim.Play();
        }

        private static void UpdateScoreTextGame()
        {
            if (score == null) return;
            score.text = StartScore.ToString();
        }

        private bool _go;


        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.A) ||
                Input.GetKeyUp(KeyCode.D))
            {
                _go = false;
            }

            if (_go) return;
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                _go = true;
                ClickEvent(true);
            }

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                _go = true;
                ClickEvent(false);
            }
        }


        public static void LoseGame()
        {
            LoadMenu();
        }

        private static void LoadMenu()
        {
            _isGameStarted = false;
            SceneLoaderFromMenu.stateMachine.Enter<LoadLevelState, string>(MAIN_MENU_SCENE);
        }


        protected override void ConfirmPause()
        {
            
        }

        protected override void ConfirmUnPause()
        {
        }
    }
}

public class PlayerData
{
    public int record;
}