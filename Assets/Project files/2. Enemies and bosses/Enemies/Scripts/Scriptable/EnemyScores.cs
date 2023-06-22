using UnityEngine;

namespace ProjectFiles.Enemies.Scriptable
{
    [CreateAssetMenu(menuName = "Enemy", fileName = "CircleDefault")]
    public class EnemyScores : ScriptableObject
    {
        public enum EnemyType
        {
            DefaultCircle, Boss
        }

        public EnemyType type;

        public int hp;
        public int damage;

    }
}
