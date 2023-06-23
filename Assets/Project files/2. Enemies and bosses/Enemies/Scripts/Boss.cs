using ProjectFiles.Bosses;
using UnityEngine;

namespace ProjectFiles.Enemies
{
    public abstract class Boss : MonoBehaviour, IBoss
    {
        public BossInterface @interface;
        public abstract float GetDamage(int hp, int maxHp, float currentScale);
        
        public abstract void StartAnim();
        
        public abstract void EndAnim();
    }
}