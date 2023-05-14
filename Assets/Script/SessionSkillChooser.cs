using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Script.Managers;
using UnityEngine;
using UnityEngine.UI;

public class SessionSkillChooser : MonoBehaviour
{
    private const int DefaultNUllNumber = -1;
    private const int ULTIMATE_SLOT = 2;

    [SerializeField]
    private Button[] skillsButton;

    private List<Skill> _currentSkills;
    private List<Skill> _allPlayerSkill;
    private Skill[] _slotSkills;

    [SerializeField]
    private Image[] _slotsIcon;

    [SerializeField]
    private GameObject[] _slotsBuyingIcon;

    [SerializeField]
    private Text[] _slotsCost;

    [SerializeField]
    private Text[] _skillName;

    private int[] _slotSkillId;

    private string _nullCostText = "0_0";

    private void Start()
    {
        _slotSkillId = new int[3];
        _slotSkills = new Skill[3];
        _currentSkills = new List<Skill>();
        _allPlayerSkill = new List<Skill>();
    }

    private void OnEnable()
    {
        GameManager.OpenSkillsSlot += SetSkillsInSlots;
    }

    private void OnDisable()
    {
        GameManager.OpenSkillsSlot -= SetSkillsInSlots;
    }

    private void SetSkillsInSlots()
    {
        Skill _randomSkill = SkillsData.GetRandomSkill();
        if (_randomSkill == null)
        {
            _slotsCost[0].text = _nullCostText;
            _slotSkillId[0] = DefaultNUllNumber;
            _slotsCost[1].text = _nullCostText;
            _slotSkillId[1] = DefaultNUllNumber;
            _slotsCost[2].text = _nullCostText;
            _slotSkillId[2] = DefaultNUllNumber;
            _slotsBuyingIcon[0].SetActive(true);
            _slotsBuyingIcon[1].SetActive(true);
            _slotsBuyingIcon[2].SetActive(true);
            return;
        }

        _slotsBuyingIcon[0].SetActive(false);
        _slotSkills[0] = _randomSkill;
        _slotsIcon[0].sprite = _randomSkill.sprite;
        _slotSkillId[0] = _randomSkill.Id;
        Debug.Log(_randomSkill.currentLevel);
        _slotsCost[0].text = _randomSkill.GetCost().ToString();
        _skillName[0].text = _randomSkill.name;
        _randomSkill = SkillsData.GetRandomSkill();
        if (_randomSkill == null)
        {
            _slotSkills[0].ClearMaybeLevel();
            _slotsCost[1].text = _nullCostText;
            _slotSkillId[1] = DefaultNUllNumber;
            _slotsCost[2].text = _nullCostText;
            _slotSkillId[2] = DefaultNUllNumber;
            _slotsBuyingIcon[1].SetActive(true);
            _slotsBuyingIcon[2].SetActive(true);
            return;
        }

        _slotsIcon[1].sprite = _randomSkill.sprite;
        _slotsBuyingIcon[1].SetActive(false);
        _slotSkills[1] = _randomSkill;
        _slotSkillId[1] = _randomSkill.Id;
        _slotsCost[1].text = _randomSkill.GetCost().ToString();
        _skillName[1].text = _randomSkill.name;
        _randomSkill = SkillsData.GetRandomSkill();
        if (_randomSkill == null)
        {
            _slotSkills[0].ClearMaybeLevel();
            _slotSkills[1].ClearMaybeLevel();
            _slotsCost[2].text = _nullCostText;
            _slotSkillId[2] = DefaultNUllNumber;
            _slotsBuyingIcon[2].SetActive(true);
            return;
        }

        _slotsIcon[2].sprite = _randomSkill.sprite;
        _slotsBuyingIcon[2].SetActive(false);
        _slotSkills[2] = _randomSkill;
        _slotSkillId[2] = _randomSkill.Id;
        _slotsCost[2].text = _randomSkill.GetCost().ToString();
        _skillName[2].text = _randomSkill.name;
        _slotSkills[0].ClearMaybeLevel();
        _slotSkills[1].ClearMaybeLevel();
        _slotSkills[2].ClearMaybeLevel();
    }

    private void UpdateSlotsSkillsCost()
    {
        for (var _index = 0; _index < _slotSkills.Length; _index++)
        {
            if (_slotSkills != null)
                _slotsCost[_index].text = _slotSkills[_index].GetCost().ToString();
        }
    }

    public void ChooseSkill(int button)
    {
        if (GameManager.StartScore < SkillsData.GetSkillFromId(_slotSkillId[button]).GetCost()) return;
        GameManager.StartScore -= SkillsData.GetSkillFromId(_slotSkillId[button]).GetCost();
        _slotsBuyingIcon[button].SetActive(true);
        _slotSkills[button].LevelUp();
        bool _exists = _allPlayerSkill.Exists(x => x.Id == _slotSkillId[button]);
        if (!_exists)
        {
            if (_currentSkills.Count == 3)
            {
                AddNewSkill(_slotSkillId[button]);
                UpdateSlotsSkillsCost();
                return;
            }

            Skill _skill = _slotSkills[button];
            if (_skill.isUltimate && _currentSkills.Exists(x => x.activeSlot == ULTIMATE_SLOT))
                AddNewSkill(_slotSkillId[button]);
            else if (_skill.isUltimate)
                AddNewSkillInSlot(_slotSkillId[button], ULTIMATE_SLOT);
            else if (_currentSkills.Exists(x => x.activeSlot == ULTIMATE_SLOT))
            {
                AddNewSkillInSlot(_slotSkillId[button]);
            }
            else if (_currentSkills.Count == 2)
            {
                AddNewSkill(_slotSkillId[button]);
            }
            else
            {
                AddNewSkillInSlot(_slotSkillId[button]);
            }
        }

        UpdateSlotsSkillsCost();
    }

    private void AddNewSkillInSlot(int newSkillId, int slot = DefaultNUllNumber, bool isNew = true)
    {
        Skill _currentSkill = SkillsData.GetSkillFromId(newSkillId);
        var number = slot;
        if (slot == DefaultNUllNumber || !skillsButton[number].gameObject.activeSelf)
        {
            _allPlayerSkill.Add(_currentSkill);
            if (slot == DefaultNUllNumber && !skillsButton[0].gameObject.activeSelf)
            {
                number = 0;
            }
            else if (slot == DefaultNUllNumber && !skillsButton[1].gameObject.activeSelf)
                number = 1;
            else
                number = slot == DefaultNUllNumber ? _currentSkills.Count : slot;

            skillsButton[number].gameObject.SetActive(true);
        }

        _currentSkills.Add(_currentSkill);


        _currentSkill.activeSlot = number;

        async void Call()
        {
            skillsButton[number].interactable = false;
            await _currentSkill.UseSkill();
            await Task.Delay((int) (_currentSkill.cooldown[_currentSkill.currentLevel] * 1000));
            skillsButton[number].interactable = true;
        }

        skillsButton[number].onClick.RemoveAllListeners();
        skillsButton[number].onClick.AddListener(Call);
        skillsButton[number].GetComponent<Image>().sprite = _currentSkill.sprite;
    }

    private void AddNewSkill(int newSkillId)
    {
        Skill _currentSkill = SkillsData.GetSkillFromId(newSkillId);
        _allPlayerSkill.Add(_currentSkill);
    }


    private void ReplaceSkill(int newSkillId, int oldSkillId)
    {
        Skill oldSkill = _currentSkills.Find(x => x.Id == oldSkillId);
        Skill newSkill = SkillsData.GetSkillFromId(newSkillId);
        if ((oldSkill.isUltimate && !newSkill.isUltimate) || (!oldSkill.isUltimate && newSkill.isUltimate)) return;
        _currentSkills.Remove(oldSkill);
        AddNewSkillInSlot(newSkillId, oldSkill.activeSlot);
    }
}