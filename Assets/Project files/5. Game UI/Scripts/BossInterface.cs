using ProjectFiles.Skills;
using UnityEngine;
using UnityEngine.UI;

public class BossInterface : MonoBehaviour
{
    [SerializeField]
    private SessionSkillChooser _skillChooser;
    [SerializeField]
    private Slider _bossHealthBar;
    
    public void EnableBossHealth()
    {
        _bossHealthBar.gameObject.SetActive(true);
    }
    public void DisableBossHealth()
    {
        _bossHealthBar.gameObject.SetActive(false);
        _skillChooser.OpenChoseSkillWindow();
    }
    public Slider GetHealthBar()
    {
         return _bossHealthBar;
    }
}