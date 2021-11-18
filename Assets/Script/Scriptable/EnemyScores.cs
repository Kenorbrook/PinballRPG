using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Enemy", fileName = "CircleDefault")]
public class EnemyScores : ScriptableObject
{
    public enum EnemyType
    {
        DefaultCircle
    }

    [SerializeField] EnemyType type;

    public float hp;
    public float damage;

}
