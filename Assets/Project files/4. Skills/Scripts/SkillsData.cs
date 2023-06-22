using System.Collections.Generic;
using UnityEngine;

namespace ProjectFiles.Skills
{
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

        private static void ClearAllSkills()
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
            Player.Player.player.lifeSteel = skill.value[skill.currentLevel];
        }

        public void LifeSteelOffSkill()
        {
            Player.Player.player.lifeSteel = 0;
        }

        public void CluelessSkill()
        {
            Player.Player.player.gameObject.layer = 3;
            Player.Player.player.dealDamage += CluelessOffSkill;
        }

        public void CluelessOffSkill()
        {
            Player.Player.player.gameObject.layer = 0;
            Player.Player.player.dealDamage -= CluelessOffSkill;
        }

        public void BerserkSkill(Skill skill)
        {
            Player.Player.player.bonusDamage = 1f + skill.value[skill.currentLevel];
        }

        public void BerserkOffSkill()
        {
            Player.Player.player.bonusDamage = 1f;
        }

        public void DefenceSkill(Skill skill)
        {
            Player.Player.player.defence = skill.value[skill.currentLevel];
        }

        public void DefenceOffSkill()
        {
            Player.Player.player.defence = 0;
        }

        public void HealSkill(Skill skill)
        {
            Player.Player.player.StartHeal(skill.value[skill.currentLevel], skill.duration[skill.currentLevel]);
        }

        public void HealOffSkill()
        {

        }


        public void SatelliteSkill(Skill skill)
        {
            Player.Player.player.UseSatellite(skill.currentLevel);
        }

        public void DischargeSkill(Skill skill)
        {
            Player.Player.player.UseDischarge((int) skill.value[skill.currentLevel]);
        }

        public void DischargeSkillOff()
        {
            Player.Player.player.OffDischarge();
        }

        public void SatelliteSkillOff()
        {
            Player.Player.player.OffSatellite();
        }

        public void MagnetismSkill()
        {
            Player.Player.player.UseMagnetism();
        }

        public void MagnetismSkillOff()
        {
            Player.Player.player.OffMagnetism();
        }

        public void SpikesSkill(Skill skill)
        {
            Spike.damage = (int) skill.value[skill.currentLevel];
            Player.Player.isSpike = true;
        }

        public void SpikesSkillOff()
        {
            Player.Player.isSpike = false;
        }
    }
}