using UnityEngine;

namespace Script.Scriptable
{
    [CreateAssetMenu(menuName = "Enemy", fileName = "CircleDefault")]
    public class EnemyScores : ScriptableObject
    {
        private enum EnemyType
        {
            DefaultCircle
        }

        [SerializeField]
        private EnemyType type;

        public int hp;
        public int damage;

    }
}
