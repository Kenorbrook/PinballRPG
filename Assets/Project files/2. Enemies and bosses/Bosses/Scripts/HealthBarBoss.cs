using UnityEngine;
using UnityEngine.UI;

public class HealthBarBoss : MonoBehaviour, IHealthBar
{
    private Slider _healthBar;


    public void SetDefaultHealth(int defaultHp, Slider healthBar)
    {
        _healthBar = healthBar;
        _healthBar.maxValue = defaultHp;
        _healthBar.value = defaultHp;
    }

    public void GetHit(int currentHp)
    {
        _healthBar.value = currentHp;
    }
}