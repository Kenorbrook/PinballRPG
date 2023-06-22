using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectFiles.Skills
{
    [Serializable]
    public abstract class ActiveSkill : ScriptableObject
    {
        [SerializeField]
        private int id;

        public Sprite sprite;

        public int activeSlot;
        private int _level;

        public UnityEvent effect;
        public UnityEvent disableEffect;
        public string skillName;
        public bool isUltimate;
        public int Id => id;
        public float[] cooldown = new float[3];
        public float[] duration = new float[3];
        public float[] value = new float[3];

        [SerializeField]
        private int[] cost = new int[3];
        public int maybeLevel { get; private set; }

        public int currentLevel
        {
            get => _level - 1;
            private set => _level = value;
        }

        public void LevelUp()
        {
            currentLevel += 2;
            if (currentLevel < 2) return;
            currentLevel = 2;
            Debug.LogError("Try upgrade skill more than 3 level");
        }

        public void SlotLevelUp()
        {
            maybeLevel++;
        }

        public void ClearMaybeLevel()
        {
            maybeLevel = 0;
        }

        public int GetMaybeLevel()
        {
            return currentLevel + maybeLevel;
        }

        public int GetCost()
        {
            return cost[currentLevel + 1];
        }

        public void ClearSkill()
        {
            maybeLevel = 0;
            _level = 0;
            activeSlot = -1;
        }

       
        public abstract Task UseSkill();
        public abstract void DisableSkill();
    }
}