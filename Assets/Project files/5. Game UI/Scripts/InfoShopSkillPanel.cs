using System.Globalization;
using ProjectFiles.Skills;
using UnityEngine;
using UnityEngine.UI;

public class InfoShopSkillPanel : MonoBehaviour
{
    [SerializeField]
    private Image _skillImage;

    [SerializeField]
    private Text _name;

    [SerializeField]
    private Text _description;

    [SerializeField]
    private Text[] _cost;

    [SerializeField]
    private Text[] _duration;

    [SerializeField]
    private Text[] _cooldown;

    private Skill _currentSkill;

    [SerializeField]
    private Image[] _currentLevel;

    [SerializeField]
    private GameObject _buySkillButton;

    [SerializeField]
    private GameObject _upgradeSkillButton;
    [SerializeField]
    private SkillInList _skillInList;
    [SerializeField]
    private SkillInList _ultInList;
    [SerializeField]
    private Transform _listSkills;

    private SessionSkillChooser _skillChooser;
    private InfoSkillPanel _skillPanel;


    public void Init(SessionSkillChooser skillChooser, InfoSkillPanel skillPanel)
    {
        _skillPanel = skillPanel;
        _skillChooser = skillChooser;
    }

    public void OpenInfoPanel(Skill skill)
    {
        _currentSkill = skill;
        gameObject.SetActive(true);
        _buySkillButton.SetActive(skill.currentLevel == -1);
        _upgradeSkillButton.SetActive(skill.currentLevel >= 0);
        SetDefaultValue();
    }

    public void CloseInfoPanel()
    {
        foreach (var _image in _currentLevel)
        {
            _image.enabled = false;
        }

        gameObject.SetActive(false);
    }

    public void BuySkill()
    {
        if (!_skillChooser.BuySkill(_currentSkill)) return;
        Instantiate(_currentSkill.isUltimate?_ultInList:_skillInList, _listSkills).Construct(_currentSkill, _skillPanel);
        _buySkillButton.SetActive(false);
    }

    public void UpgradeSkill()
    {
        if (_skillChooser.BuySkill(_currentSkill))
            _upgradeSkillButton.SetActive(false);
    }

    private void SetDefaultValue()
    {
        _skillImage.sprite = _currentSkill.sprite;
        
        _name.text = _currentSkill.skillName;
        if (_currentSkill.currentLevel >= 0)
            _currentLevel[_currentSkill.currentLevel].enabled = true;
        _description.text = _currentSkill.description;
        for (int level = 0; level < 3; level++)
        {
            _cost[level].text = _currentSkill.GetCost(level).ToString();
            _duration[level].text = _currentSkill.duration[level].ToString(CultureInfo.CurrentCulture);
            _cooldown[level].text = _currentSkill.cooldown[level].ToString(CultureInfo.CurrentCulture);
        }
    }
}