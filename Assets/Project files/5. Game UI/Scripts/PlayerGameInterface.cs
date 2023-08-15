using System;
using Michsky.MUIP;
using ProjectFiles.LevelInfrastructure;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerGameInterface : MonoBehaviour
{
    [SerializeField]
    private Image[] _health;
    [SerializeField]
    private Slider _healthBar;

    [SerializeField]
    private Text score;

    [SerializeField]
    private Button _pauseButton;
    [SerializeField]
    private ModalWindowManager _endRoundButton;

    [SerializeField]
    private Animator _animator;

    private static readonly int pause = Animator.StringToHash("Pause");

    private void Start()
    {
        _endRoundButton.onConfirm.AddListener(GameManager.LoseGame);
        _pauseButton.onClick.AddListener(GamePausedObject.TogglePause);
        _pauseButton.onClick.AddListener(ToggleAnimation);
    }


    public void UpdatePoint(int point)
    {
        score.text = point.ToString();
    }
    public void UpdateHealthBar(float value)
    {
        _healthBar.value = value;
    }
    public void UpdateHealth(int count)
    {
        for (int health = 0; health < _health.Length; health++)
        {
            _health[health].enabled = health < count;
        }
    }
    private void ToggleAnimation()
    {
        bool isPause = _animator.GetBool(pause);
        _animator.SetBool(pause, !isPause);
    }
}