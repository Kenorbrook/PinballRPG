using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Animation))]
public class Enemy : MovingObj
{
    public EnemyScores EnemySccores;
    private float _hp;
    private float _maxHp;
    private Slider _healthBar;
    [SerializeField] float force = 100f;
    private void Start()
    {
        base.MyStart();
        _hp = EnemySccores.hp;
        _maxHp = _hp;
        _healthBar = GetComponentInChildren<Slider>();
        _healthBar.maxValue = _maxHp;
        //Debug.Log(_maxHp);
        _healthBar.value = _hp;
        _healthBar.gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            collision.rigidbody.velocity = Vector2.zero;
            collision.rigidbody.AddForce(-collision.contacts[0].normal * force, ForceMode2D.Impulse);
            if (_hp == _maxHp)
            {
                _healthBar.gameObject.SetActive(true);
            }
            _hp -= Player.player.damage;
           
            _healthBar.value = _hp;
            if (_hp <= 0)
            {
                GetComponentInParent<LevelOne>().EnemyKilling();
                gameObject.SetActive(false);
            }
            else
            {
                GetComponent<Animation>().Play();
            }
        }
    }
}
