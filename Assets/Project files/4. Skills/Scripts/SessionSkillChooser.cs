using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectFiles.LevelInfrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectFiles.Skills
{
    public class SessionSkillChooser : MonoBehaviour
    {
        
        private const int DefaultNUllNumber = -1;
        private const int ULTIMATE_SLOT = 2;

        [SerializeField]
        private InfoSkillPanel _infoSkillPanel;
        [SerializeField]
        private GameObject _chooseSkillPanel;
        [SerializeField]
        private Button[] skillsButton;

        [SerializeField]
        private Image[] _skillImage;
        
        private List<Skill> _currentSkills;
        private List<Skill> _allPlayerSkill;
        private Skill[] _slotSkills;

        [SerializeField]
        private Image[] _slotsIcon;
        
        
        [SerializeField]
        private Sprite _maskCircle;
        [SerializeField]
        private Sprite _maskBox;
        
        [SerializeField]
        private Image[] _mask;
        [SerializeField]
        private Image[] _maskStroke;
        [SerializeField]
        private Image[] _maskBackground;

        [SerializeField]
        private GameObject[] _slotsBuyingIcon;

        [SerializeField]
        private Text[] _slotsCost;

        [SerializeField]
        private Text[] _skillName;

        private int[] _slotSkillId;

        private const string NULL_COST_TEXT = "0_0";

        private event Action OpenSkillsSlot;
        private int _choosenSkill;

        private void Start()
        {
            _infoSkillPanel.Init(this);
            _slotSkillId = new int[3];
            _slotSkills = new Skill[3];
            _currentSkills = new List<Skill>();
            _allPlayerSkill = new List<Skill>();
        }

        private void OnEnable()
        {
            OpenSkillsSlot += SetSkillsInSlots;
        }

        private void OnDisable()
        {
            OpenSkillsSlot -= SetSkillsInSlots;
        }

        public void OpenChoseSkillWindow()
        {
            Player.Player.player.gameObject.SetActive(false);
            OpenSkillsSlot?.Invoke();
            _chooseSkillPanel.SetActive(true);
        }

        public void CloseChoseSkillWindow()
        {
            Player.Player.player.gameObject.SetActive(true);
            _chooseSkillPanel.SetActive(false);
            Player.Player.player.StartGhost();
        }

        private void SetSkillsInSlots()
        {
            Skill _randomSkill = SkillsData.GetRandomSkill();
            if (_randomSkill == null)
            {
                _slotsCost[0].text = NULL_COST_TEXT;
                _slotSkillId[0] = DefaultNUllNumber;
                _slotsCost[1].text = NULL_COST_TEXT;
                _slotSkillId[1] = DefaultNUllNumber;
                _slotsCost[2].text = NULL_COST_TEXT;
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
            _slotsCost[0].text = _randomSkill.GetCost().ToString();
            _skillName[0].text = _randomSkill.name;
            skillsButton[0].interactable = true;
            if (_randomSkill.isUltimate)
            {
                _mask[0].sprite = _maskBox;
                _maskStroke[0].sprite = _maskBox;
                _maskBackground[0].sprite = _maskBox;
            }
            else
            {
                _mask[0].sprite = _maskCircle;
                _maskStroke[0].sprite = _maskCircle;
                _maskBackground[0].sprite = _maskCircle;
            }
            _randomSkill = SkillsData.GetRandomSkill();
            if (_randomSkill == null)
            {
                _slotSkills[0].ClearMaybeLevel();
                _slotsCost[1].text = NULL_COST_TEXT;
                _slotSkillId[1] = DefaultNUllNumber;
                _slotsCost[2].text = NULL_COST_TEXT;
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
            skillsButton[1].interactable = true;
            if (_randomSkill.isUltimate)
            {
                _mask[1].sprite = _maskBox;
                _maskStroke[1].sprite = _maskBox;
                _maskBackground[1].sprite = _maskBox;
            }
            else
            {
                _mask[1].sprite = _maskCircle;
                _maskStroke[1].sprite = _maskCircle;
                _maskBackground[1].sprite = _maskCircle;
            }
            _randomSkill = SkillsData.GetRandomSkill();
            if (_randomSkill == null)
            {
                _slotSkills[0].ClearMaybeLevel();
                _slotSkills[1].ClearMaybeLevel();
                _slotsCost[2].text = NULL_COST_TEXT;
                _slotSkillId[2] = DefaultNUllNumber;
                _slotsBuyingIcon[2].SetActive(true);
                return;
            }
            if (_randomSkill.isUltimate)
            {
                _mask[2].sprite = _maskBox;
                _maskStroke[2].sprite = _maskBox;
                _maskBackground[2].sprite = _maskBox;
            }
            else
            {
                _mask[2].sprite = _maskCircle;
                _maskStroke[2].sprite = _maskCircle;
                _maskBackground[2].sprite = _maskCircle;
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
            skillsButton[2].interactable = true;
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
            _choosenSkill = button;
            _infoSkillPanel.OpenInfoPanel(_slotSkills[button]);
        }
        
        public void BuySkill(Skill skill)
        {
            if (GameManager.StartScore < SkillsData.GetSkillFromId(skill.Id).GetCost()) return;
            GameManager.StartScore -= SkillsData.GetSkillFromId(skill.Id).GetCost();
            _slotsBuyingIcon[_choosenSkill].SetActive(true);
            skillsButton[_choosenSkill].interactable = false;
            _slotSkills[_choosenSkill].LevelUp();
            bool _exists = _allPlayerSkill.Exists(x => x.Id == _slotSkillId[_choosenSkill]);
            if (!_exists)
            {
                if (_currentSkills.Count == 3)
                {
                    AddNewSkill(_slotSkillId[_choosenSkill]);
                    UpdateSlotsSkillsCost();
                    return;
                }

                if (skill.isUltimate && _currentSkills.Exists(x => x.activeSlot == ULTIMATE_SLOT))
                    AddNewSkill(_slotSkillId[_choosenSkill]);
                else if (skill.isUltimate)
                    AddNewSkillInSlot(_slotSkillId[_choosenSkill], ULTIMATE_SLOT);
                else if (_currentSkills.Exists(x => x.activeSlot == ULTIMATE_SLOT))
                {
                    AddNewSkillInSlot(_slotSkillId[_choosenSkill]);
                }
                else if (_currentSkills.Count == 2)
                {
                    AddNewSkill(_slotSkillId[_choosenSkill]);
                }
                else
                {
                    AddNewSkillInSlot(_slotSkillId[_choosenSkill]);
                }
            }

            UpdateSlotsSkillsCost();
        }
        

        private void AddNewSkillInSlot(int newSkillId, int slot = DefaultNUllNumber)
        {
            Skill _currentSkill = SkillsData.GetSkillFromId(newSkillId);
            var number = slot;
            if (slot == DefaultNUllNumber || !skillsButton[number].gameObject.activeSelf)
            {
                _allPlayerSkill.Add(_currentSkill);
                number = slot switch
                {
                    DefaultNUllNumber when !skillsButton[0].gameObject.activeSelf => 0,
                    DefaultNUllNumber when !skillsButton[1].gameObject.activeSelf => 1,
                    DefaultNUllNumber => _currentSkills.Count,
                    _ => slot
                };

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
            _skillImage[number].sprite = _currentSkill.sprite;
        }    
        private void RemoveSkillFromSlot(Skill removedSkill, int slot = DefaultNUllNumber)
        {

            _currentSkills.Remove(removedSkill);

            skillsButton[slot].onClick.RemoveAllListeners();
            skillsButton[slot].gameObject.SetActive(false);
            removedSkill.activeSlot = -1;
            
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
            RemoveSkillFromSlot(oldSkill);
            AddNewSkillInSlot(newSkillId, oldSkill.activeSlot);
        }
    }
}