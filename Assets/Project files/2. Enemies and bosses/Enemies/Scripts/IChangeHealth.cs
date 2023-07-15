using System.Collections;
using UnityEngine;

namespace ProjectFiles.Enemies
{
    public interface IChangeHealth
    {
        public int TakeDamage(int damage);
        public IEnumerator hitAnim();

        public void Init(SpriteRenderer spriteRenderer);
    }
}