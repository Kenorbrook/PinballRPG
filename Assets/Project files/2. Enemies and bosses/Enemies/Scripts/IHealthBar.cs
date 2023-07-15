using UnityEngine.UI;

public interface IHealthBar
{
    public void SetDefaultHealth(int defaultHp, Slider healthBar);

    public void GetHit(int currentHp);
}