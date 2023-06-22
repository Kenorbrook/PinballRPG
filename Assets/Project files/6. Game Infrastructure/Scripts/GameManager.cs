using System;
using System.Collections.Generic;
using ProjectFiles.MovingObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ProjectFiles.LevelInfrastructure
{
    public class GameManager : MonoBehaviour
    {
        public bool isEditor;

        public static PlayerData playerData;

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
        public static List<Level> CurrentLevels => ((LevelConstructFactory)AllServices.Container.GetSingle<ILevelConstructFactory>())._currentLevels;
        public static int Level;

        public static Action OpenSkillsSlot;
        public Slider bossSlider;

        [SerializeField]
        private Text _score;

        private static Text score;

        [SerializeField]
        private Image[] Health;

        [SerializeField]
        private Animation _animRight;

        [SerializeField]
        private Animation _animLeft;

        [SerializeField]
        private MovableInStart[] _movable;

        [SerializeField]
        private GameObject ChooseSkillWindow;

        private void Awake()
        {
            Level = 0;
            instance = this;

            score = _score;
        }

        private void Start()
        {
            StartScore = 0;
        }

        public static void ClickEvent(bool isRight)
        {
            if (!_isGameStarted)
            {
                _isGameStarted = true;
                instance.startGame();
            }

            Player.Player.player.GoUp(isRight ? 1 : -1);
            if (!isRight)
            {
                instance._animLeft.transform.position = Player.Player.player.transform.position;
                instance._animLeft.Stop();
                instance._animLeft.Play();
            }
            else
            {
                instance._animRight.transform.position = Player.Player.player.transform.position;
                instance._animRight.Stop();
                instance._animRight.Play();
            }
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

        public void UpdateUiHp()
        {
            int _i = 1;
            foreach (var _health in Health)
            {
                if (_i <= Player.Player.GetHp())
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

        public void OpenChoseSkillWindow()
        {
            Player.Player.player.gameObject.SetActive(false);
            OpenSkillsSlot?.Invoke();
            ChooseSkillWindow.SetActive(true);
        }

        public void CloseChoseSkillWindow()
        {
            Player.Player.player.gameObject.SetActive(true);
            ChooseSkillWindow.SetActive(false);
            Player.Player.player.StartGhost();
        }

        private void startGame()
        {
            foreach (var _t in _movable)
            {
                _t.Move();
            }
        }

        public static void LoseGame()
        {
            LoadMenu();
        }

        public static void LoadMenu()
        {
            _isGameStarted = false;
            SceneManager.LoadScene("MainMenu");
        }
    }
}

public class PlayerData
{
    public int record;
}