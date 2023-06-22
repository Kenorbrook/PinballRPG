using System;
using System.Collections;
using ProjectFiles.Bosses;
using ProjectFiles.Enemies.Scriptable;
using ProjectFiles.LevelInfrastructure;
using ProjectFiles.MovingObjects;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectFiles.Enemies
{
    public class Enemy : MovingObj
    {
        private const float FORCE_REPELLING = 5f;

        public EnemyScores EnemyScores;
        private int _currentHp;
        private Slider _healthBar;
        private SpriteRenderer _sprite;
        private float _currentScale;
        //TODO развязать босса и врага...
        private IBoss _boss;
        private Coroutine _animScale;

        private new void Start()
        {
            base.Start();
            GetComponents();
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            _currentScale = _sprite.transform.localScale.x;
            _currentHp = EnemyScores.hp;

            _healthBar.maxValue = _currentHp;
            _healthBar.value = _currentHp;
        }

        private void GetComponents()
        {
            _sprite = GetComponent<SpriteRenderer>();
            if (EnemyScores.type != EnemyScores.EnemyType.Boss)
            {
                _healthBar = GetComponentInChildren<Slider>();
                _healthBar.gameObject.SetActive(false);
            }
            else
            {
                _healthBar = GameManager.instance.bossSlider;
                _healthBar.gameObject.SetActive(true);
                _boss = GetComponent<IBoss>();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;
            collision.rigidbody.velocity = Vector2.zero;
            collision.rigidbody.AddForce(-collision.contacts[0].normal * FORCE_REPELLING, ForceMode2D.Impulse);
        }

        public int TakeDamage(int damage)
        {
            int takenDamage;
            if (EnemyScores.type != EnemyScores.EnemyType.Boss && Math.Abs(_currentHp - EnemyScores.hp) < 0.01)
            {
                _healthBar.gameObject.SetActive(true);
            }

            _currentHp -= damage;
            if (EnemyScores.type == EnemyScores.EnemyType.Boss)
            {
                _currentScale = _boss.GetDamage(_currentHp, EnemyScores.hp, _currentScale);
            }

            _healthBar.value = _currentHp;
            if (_currentHp <= 0)
            {
                takenDamage = _currentHp + damage;
                GetComponentInParent<Level>().EnemyKilled++;
                GameManager.StartScore += EnemyScores.type == EnemyScores.EnemyType.Boss ? 1000 : 100;
                if (EnemyScores.type == EnemyScores.EnemyType.Boss)
                {
                    _healthBar.gameObject.SetActive(false);
                    _boss.EndAnim();
                }

                Destroy(gameObject);
            }
            else
            {
                takenDamage = damage;
                _animScale ??= StartCoroutine(hitAnim());
            }

            return takenDamage;
        }

        private IEnumerator hitAnim()
        {
            Vector3 _vec;
            float _endScale;
            if (EnemyScores.type == EnemyScores.EnemyType.Boss)
            {
                _vec = new Vector3(0.01f, 0.01f, 0);
                _endScale = _currentScale * 1.1f;
            }
            else
            {
                _vec = new Vector3(0.001f, 0.001f, 0);
                _endScale = _currentScale * 1.3f;
            }

            while (_sprite.transform.localScale.x < _endScale)
            {
                _sprite.transform.localScale += _vec;
                yield return null;
            }

            while (_sprite.transform.localScale.x > _currentScale)
            {
                _sprite.transform.localScale -= _vec;
                yield return null;
            }

            _animScale = null;
        }
    }
}