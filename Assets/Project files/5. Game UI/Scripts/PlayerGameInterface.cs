using UnityEngine;
using UnityEngine.UI;

public class PlayerGameInterface : MonoBehaviour
{
    [SerializeField]
    private Image[] _health;
    [SerializeField]
    private Slider _healthBar;

    [SerializeField]
    private Text score;

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

}