using ProjectFiles.Skills;
using UnityEngine;
using UnityEngine.UI;

public class SkillInList : MonoBehaviour
{
    private Skill _skill;

    [SerializeField]
    private Image _skillImage;

    [SerializeField]
    private Button _openInfoPanelButton;
    
    public void Construct(Skill skill, InfoSkillPanel skillPanel)
    {
        _skill = skill;
        _skillImage.sprite = skill.sprite;
        _openInfoPanelButton.onClick.AddListener(()=>skillPanel.OpenInfoPanel(_skill));
    }
    
}
