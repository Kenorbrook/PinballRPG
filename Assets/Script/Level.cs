using Script.Managers;
using UnityEngine;

namespace Script
{
    public class Level : MonoBehaviour
    {

        private int _enemyKilled;
        public int LevelId;
        public Enemy[] _enemies;

        public int height;
        public int EnemyKilled
        {
            get => _enemyKilled;
            set
            {
                _enemyKilled = value;
                checkEndOfRound();
            }
        }


        private void Start()
        {
            _enemies = GetComponentsInChildren<Enemy>();
            EnemyKilled = 0;
        }

        private void checkEndOfRound()
        {
            Debug.Log($"EnemyKilled - {EnemyKilled}. Enemy on field - {_enemies.Length}");
            if (EnemyKilled == _enemies.Length)
            {
                //TODO Create new level.
                LevelManagers.NewLevelCollider.enabled = true;
            }
        }

        
    }
}