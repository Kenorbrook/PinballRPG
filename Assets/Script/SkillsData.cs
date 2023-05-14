using System.Collections.Generic;
using Script;
using UnityEngine;

public class SkillsData : MonoBehaviour
{
    [SerializeField]
    private List<Skill> _skillsContainer;
    private static List<Skill> skillsContainer;

    private static int countSkill => skillsContainer.Count;

    private void Start()
    {
        skillsContainer = _skillsContainer;
        ClearAllSkills();
    }

    public static Skill GetSkillFromId(int id)
    {
        return skillsContainer.Find(x => x.Id == id);
    }

    private void ClearAllSkills()
    {
        foreach (var _skill in skillsContainer)
        {
            _skill.ClearSkill();
        }
    }
    public static Skill GetRandomSkill()
    {
        int _numberSkill = Random.Range(0, countSkill);
        Skill _randomSkill = skillsContainer[_numberSkill];
        int _cycleCount = 0;
        while (_randomSkill.GetMaybeLevel() > 1)
        {
            _cycleCount++;
            if (_cycleCount > countSkill)
                return null;
            _randomSkill = skillsContainer[++_numberSkill % countSkill];
        }
        _randomSkill.SlotLevelUp();
        return _randomSkill;
    }


    public void DecelerationSkill()
    {
        Time.timeScale /= 2;
    }
    
    public void DecelerationOffSkill()
    {
        Time.timeScale *= 2;
    }

    public void LifeSteelSkill(Skill skill)
    {
        Player.player.lifeSteel = skill.value[skill.currentLevel];
    }
    public void LifeSteelOffSkill()
    {
        Player.player.lifeSteel = 0;
    }

    public void CluelessnessSkill()
    {
        Player.player.gameObject.layer = 3;
        Player.player.dealDamage += CluelessnessOffSkill;
    }
    
    public void CluelessnessOffSkill()
    {
        Player.player.gameObject.layer = 0;
        Player.player.dealDamage -= CluelessnessOffSkill;
    }

    public void BerserkSkill(Skill skill)
    {
        Player.player.bonusDamage = 1f+skill.value[skill.currentLevel];
    }
    
    public void BerserkOffSkill()
    {
        Player.player.bonusDamage = 1f;
    }
    
    public void DefenceSkill(Skill skill)
    {
        Player.player.defence = skill.value[skill.currentLevel];
    }
    
    public void DefenceOffSkill()
    {
        Player.player.defence = 0;
    }
    
    public void HealSkill(Skill skill)
    {
        Player.player.StartHeal(skill.value[skill.currentLevel],skill.duration[skill.currentLevel]);
    }
    
    public void HealOffSkill()
    {
        
    }


    public void SatelliteSkill(Skill skill)
    {
        Player.player.UseSatellite(skill.currentLevel);
    }
    public void SatelliteSkillOff()
    {
        Player.player.OffSatellite();
    }
}
