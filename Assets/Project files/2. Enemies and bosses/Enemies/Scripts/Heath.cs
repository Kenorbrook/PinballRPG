using System;
using System.Collections;
using UnityEngine;

namespace ProjectFiles.Enemies
{
    public abstract class Heath:MonoBehaviour,IChangeHealth{
        internal int currentHp;
        [SerializeField]
        internal int maximumHealth;
        internal IHealthBar healthBar;
        internal float currentScale;
        
        internal SpriteRenderer sprite;
        public abstract int TakeDamage(int damage);

        internal Coroutine animScale;
        public abstract IEnumerator hitAnim();

        public abstract void Init( SpriteRenderer spriteRenderer);

        public abstract void Reset();
    }
}