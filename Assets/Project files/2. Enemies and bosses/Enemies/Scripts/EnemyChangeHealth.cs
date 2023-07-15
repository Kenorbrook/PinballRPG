using System.Collections;
using ProjectFiles.LevelInfrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectFiles.Enemies
{
    public class EnemyChangeHealth:Heath
    {
        [SerializeField]
        private Slider _healthBar;
        public override int TakeDamage(int damage)
        {
            int takenDamage;

            currentHp -= damage;
            healthBar.GetHit(currentHp);
            if (currentHp <= 0)
            {
                takenDamage = currentHp + damage;
                GetComponentInParent<Level>().EnemyKilled++;
                GameManager.StartScore += 100;
              

                Destroy(gameObject);
            }
            else
            {
                takenDamage = damage;
                animScale ??= StartCoroutine(hitAnim());
            }

            return takenDamage;
        }

        public override IEnumerator hitAnim()
        {
            float animSpeed = currentScale / 100;
           
            var _vec = new Vector3(animSpeed, animSpeed, 0);
            var _endScale = currentScale * 1.3f;
            

            while (sprite.transform.localScale.x < _endScale)
            {
                sprite.transform.localScale += _vec;
                yield return null;
            }

            while (sprite.transform.localScale.x > currentScale)
            {
                sprite.transform.localScale -= _vec;
                yield return null;
            }

            animScale = null;
        }
        

        public override void Init(SpriteRenderer spriteRenderer)
        {
            sprite = spriteRenderer;
            currentScale = sprite.transform.localScale.x;
            currentHp = maximumHealth;
            healthBar = GetComponent<IHealthBar>();
            healthBar.SetDefaultHealth(currentHp,_healthBar);
        }
    }
}