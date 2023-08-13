using System.Globalization;
using ProjectFiles.Skills;
using UnityEngine;
using UnityEngine.UI;

public class InfoSkillPanel : MonoBehaviour
{

    [SerializeField]
    private Text _skillName;
    [SerializeField]
    private Text[] _cost;
    [SerializeField]
    private Text[] _duration;
    [SerializeField]
    private Text[] _cooldown;

    [SerializeField]
    private Image _skillImage;
    [SerializeField]
    private Image _ultImage;
    [SerializeField]
    private GameObject _skillObj;
    [SerializeField]
    private GameObject _ultObj;

    [SerializeField]
    private GameObject[] _blocks;
    [SerializeField]
    private Button[] _replaceSkill;

    [SerializeField]
    private SessionSkillChooser _skillChooser;
    public void OpenInfoPanel(Skill skill)
    {
        for (var slotNumber = 0; slotNumber < 3; slotNumber++)
        {
            _replaceSkill[slotNumber].onClick.RemoveAllListeners();
            int _number = slotNumber;
            _replaceSkill[slotNumber].onClick.AddListener(()=>_skillChooser.ReplaceSkillInSlot(skill,_number));
        }

        _skillName.text = skill.skillName;
        _skillObj.SetActive(!skill.isUltimate);
        _ultObj.SetActive(skill.isUltimate);
        _blocks[0].SetActive(skill.isUltimate);
        _blocks[1].SetActive(skill.isUltimate);
        _blocks[2].SetActive(!skill.isUltimate);
        if (skill.isUltimate)
            _ultImage.sprite = skill.sprite;
        else
            _skillImage.sprite = skill.sprite;
        for (int number = 0; number < 3; number++)
        {
            _cooldown[number].text = skill.cooldown[number].ToString(CultureInfo.CurrentCulture);
            _duration[number].text = skill.duration[number].ToString(CultureInfo.CurrentCulture);
            _cost[number].text = skill.GetCost(number).ToString(CultureInfo.CurrentCulture);
        }
        gameObject.SetActive(true);
    }
}