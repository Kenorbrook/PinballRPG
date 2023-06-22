using ProjectFiles.Enemies;
using UnityEngine;

namespace ProjectFiles.LevelInfrastructure
{
    public class Level : MonoBehaviour
    {
        private int _enemyKilled;
        public int LevelId;
        public Enemy[] _enemies;

        public int height;
        private bool IsAllEnemyKilled => EnemyKilled == _enemies.Length;

        public int EnemyKilled
        {
            get => _enemyKilled;
            set
            {
                _enemyKilled = value;
                if (value == 0) return;
                checkEndOfRound();
            }
        }


        private void Start()
        {
            InitDefaultValues();
        }

        private void InitDefaultValues()
        {
            _enemies = GetComponentsInChildren<Enemy>();
            EnemyKilled = 0;
        }

        private void checkEndOfRound()
        {
            Debug.Log($"EnemyKilled - {EnemyKilled}. Enemy on field - {_enemies.Length}");
            
            if (!IsAllEnemyKilled) return;
            
            if (LevelConstructor.isBossFight)
                GameManager.instance.OpenChoseSkillWindow();
            
            LevelConstructor.NewLevelCollider.enabled = true;
        }
    }
}