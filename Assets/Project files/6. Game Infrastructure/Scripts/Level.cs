using System;
using ProjectFiles.Enemies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProjectFiles.LevelInfrastructure
{
    public class Level : MonoBehaviour
    {
        private int _enemyKilled;
        public int LevelId;

        [HideInInspector]
        public Enemy[] _enemies;

        [SerializeField]
        private List[] _levelVariables;

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
            DisableObjectForVariable();
            InitDefaultValues();
        }

        public void ResetLevel()
        {
            EnemyKilled = 0;
            foreach (var enemy in _enemies)
            {
                enemy.ResetEnemy();
            }
        }

        private void DisableObjectForVariable()
        {
            if (_levelVariables == null || _levelVariables.Length <= 0) return;

            int variable = Random.Range(0, _levelVariables.Length);
            foreach (var objects in _levelVariables[variable].levelObjects)
            {
                objects.SetActive(false);
            }
        }

        private void InitDefaultValues()
        {
            _enemies = GetComponentsInChildren<Enemy>();
            EnemyKilled = 0;
            checkEndOfRound();
        }

        private void checkEndOfRound()
        {
            Debug.Log($"EnemyKilled - {EnemyKilled}. Enemy on field - {_enemies.Length}");

            if (!IsAllEnemyKilled) return;

            LevelConstructor.NewLevelCollider.enabled = true;
        }

        [Serializable]
        private class List
        {
            public GameObject[] levelObjects;
        }
    }
}