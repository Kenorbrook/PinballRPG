using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour, IHealthBar
{
    private Slider _healthBar;


    public void SetDefaultHealth(int defaultHp,  Slider healthBar)
    {
        _healthBar = healthBar;
        _healthBar.gameObject.SetActive(false);
        _healthBar.maxValue = defaultHp;
        _healthBar.value = defaultHp;
    }

    public void GetHit(int currentHp)
    {
        if(!_healthBar.IsActive())
            _healthBar.gameObject.SetActive(true);
        
        _healthBar.value = currentHp;
    }
}