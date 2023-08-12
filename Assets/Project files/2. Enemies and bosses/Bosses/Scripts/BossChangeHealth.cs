using System.Collections;
using ProjectFiles.LevelInfrastructure;
using UnityEngine;

namespace ProjectFiles.Enemies
{
    public class BossChangeHealth:Heath
    {
        private IHealthBar _healthBar;
        private Boss _boss;

        private float startScale;
        public override int TakeDamage(int damage)
        {
            int takenDamage;
          

            currentHp -= damage;
            
            currentScale = _boss.GetDamage(currentHp,maximumHealth, currentScale);
            
            _healthBar.GetHit(currentHp);
            if (currentHp <= 0)
            {
                takenDamage = currentHp + damage;
                GetComponentInParent<Level>().EnemyKilled++;
                GameManager.StartScore += 1000 ;
                
                _boss.EndAnim();
                

                gameObject.SetActive(false);
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
           
            Vector3 _vec = new Vector3(animSpeed, animSpeed, 0);
            float _endScale = currentScale * 1.1f;
            
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
            startScale = currentScale;
            currentHp = maximumHealth;
            _boss = GetComponent<Boss>();
            _healthBar = GetComponent<IHealthBar>();
            _healthBar.SetDefaultHealth(currentHp, _boss.@interface.GetHealthBar());
        }

        public override void Reset()
        {
            currentHp = maximumHealth;
            currentScale = startScale;
            sprite.transform.localScale =new Vector3(currentScale, currentScale, currentScale);
            _healthBar.SetDefaultHealth(currentHp, _boss.@interface.GetHealthBar());
            _boss.Reset();
        }
    }
}