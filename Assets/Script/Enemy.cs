using System;
using System.Collections;
using Script.Managers;
using Script.Scriptable;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class Enemy : MovingObj
    {
        public EnemyScores EnemyScores;
        private int _hp;
        private int _maxHp;
        private Slider _healthBar;
        private float force = 5f;
        private SpriteRenderer _sprite;
        public bool isBosses;
        private float _currentScale;
        private IBoss boss;
        private Coroutine animScale;

        private void Start()
        {
            base.MyStart();
            _sprite = GetComponent<SpriteRenderer>();
            _currentScale = _sprite.transform.localScale.x;
            _hp = EnemyScores.hp;
            _maxHp = _hp;
            
            //Debug.Log(_maxHp);
            
            if (!isBosses)
            {
                _healthBar = GetComponentInChildren<Slider>();
                _healthBar.gameObject.SetActive(false);
            }
            else
            {
                _healthBar = GameManager.instance.bossSlider;
                _healthBar.gameObject.SetActive(true);
                boss = GetComponent<IBoss>();
            }
            _healthBar.maxValue = _maxHp;
            _healthBar.value = _hp;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Player>() == null) return;
            collision.rigidbody.velocity = Vector2.zero;
            collision.rigidbody.AddForce(-collision.contacts[0].normal * force, ForceMode2D.Impulse);
            if (!isBosses && Math.Abs(_hp - _maxHp) < 0.01)
            {
                _healthBar.gameObject.SetActive(true);
            }

            _hp -= Player.player.damage;
            if (isBosses)
            {
                _currentScale = boss.GetDamage(_hp, _maxHp, _currentScale);
            }

            _healthBar.value = _hp;
            if (_hp <= 0)
            {
                GetComponentInParent<Level>().EnemyKilled++;
                GameManager.StartScore += isBosses ? 10 : 1;
                //GameManager.RedMoney++;
                if (isBosses)
                {
                    boss.EndAnim();
                }
                Destroy(gameObject);
            }
            else
            {
                if (animScale == null)
                    animScale = StartCoroutine(hitAnim());
            }
        }

        private IEnumerator hitAnim()
        {
            Vector3 _vec;
            float _endScale;
            if (isBosses)
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

            animScale = null;
        }
    }
}