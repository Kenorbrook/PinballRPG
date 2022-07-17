using System;
using System.Collections;
using Script.Managers;
using UnityEngine;

namespace Script
{
    [RequireComponent(typeof(Rigidbody2D), typeof(TrailRenderer))]
    public class Player : MonoBehaviour
    {
        private static int _hp;
        public static Player player;
        private Rigidbody2D _rb2d;

        private TrailRenderer _trail;

        [SerializeField]
        float VerticalSpeed;

        [SerializeField]
        float HorizontalSpeed;

        public GameObject SpawnPoint;

        private float _ghost;

        // public float StartPos;
        public int damage = 25;


        // Start is called before the first frame update
        void Start()
        {
            Spawn();
            _hp = 3;
            if (player != null)
            {
                Destroy(gameObject);
            }

            _trail = GetComponent<TrailRenderer>();
            player = this;
            _rb2d = GetComponent<Rigidbody2D>();
            // StartPos = player.transform.localPosition.y;
        }


        public void GoUp(int direction)
        {
            
                _rb2d.velocity = Vector2.zero;
                _rb2d.AddForce(new Vector2(-VerticalSpeed * direction, HorizontalSpeed));
           
        }

        public void TrailEmitting()
        {
            _trail.Clear();
            _trail.emitting = !_trail.emitting;
        }

        public static int GetHp()
        {
            return _hp;
        }

        public void GetDamage()
        {
            if (_ghost > 0) return;
            _hp -= 1;
            GameManager.instance.UpdateUiHp();
            if (_hp == 0)
            {
                GameManager.LoadMenu();
                return;
            }

            Debug.Log("HP-" + _hp);
            StartCoroutine(StartGhost());
        }

        private IEnumerator StartGhost()
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
            while (_ghost < 3f)
            {
                _ghost += 0.5f;
                yield return new WaitForSeconds(0.5f);
            }

            GetComponent<SpriteRenderer>().color = Color.black;
            _ghost = 0;
        }


        private void Spawn()
        {
            transform.position = SpawnPoint.transform.position;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Enemy>())
            {
                _hp -= collision.gameObject.GetComponent<Enemy>().EnemyScores.damage;
            }
        }
    }
}