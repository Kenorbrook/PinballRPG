using System;
using System.Collections;
using System.Threading.Tasks;
using Script.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    [RequireComponent(typeof(Rigidbody2D), typeof(TrailRenderer))]
    public class Player : MonoBehaviour
    {
        private static int health;
        private static int _hp;
        private static int maxhp = 100;
        public static Player player;
        private Rigidbody2D _rb2d;

        private TrailRenderer _trail;

        [SerializeField]
        float VerticalSpeed;

        [SerializeField]
        float HorizontalSpeed;

        public GameObject SpawnPoint;

        private float _ghost;

        public Slider healthBar;
        // public float StartPos;
        private static int Damage => 25;
        [HideInInspector]
        public float bonusDamage = 1;
        [HideInInspector]
        public float lifeSteel;
        [HideInInspector]
        public float defence;

        public Action dealDamage;

        public static bool isSpike;
        public static bool isDischarge;

        [SerializeField]
        private GameObject _spikes;
        [SerializeField]
        private GameObject[] _satellite;
        
        private SpriteRenderer _spriteRenderer;

        // Start is called before the first frame update
        void Start()
        {
            Spawn();
            _hp = maxhp;
            health = 3;
            healthBar.value = (float)_hp/maxhp;
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
            return health;
        }

        public void GetDamage(int damage)
        {
            if (_ghost > 0 || damage==0) return;
            _hp -= (int)(damage*(1-defence));
            if (_hp <= 0)
            {
                if (health == 0)
                {
                    GameManager.LoseGame();
                    return;
                }
                health--;
                _hp = maxhp;
            }
            healthBar.value = (float)_hp/maxhp;
            GameManager.instance.UpdateUiHp();

            Debug.Log("HP-" + _hp);
            StartGhost();
        }

        public void StartHeal(float newHealth, float timer)
        {
           StartCoroutine(Heal(newHealth,timer));
        }

        private void UseSpikes()
        {
            Instantiate(_spikes);
        }

        public void UseSatellite(int level)
        {
            _satellite[0].SetActive(true);
            switch (level)
            {
                case 0:
                    break;
                case 1:
                    _satellite[1].SetActive(true);
                    break;
                case 2:
                    _satellite[2].SetActive(true);
                    _satellite[3].SetActive(true);
                    break;
            }
        }
        public void OffSatellite()
        {
            foreach (var satellite in _satellite)
            {
                if(satellite.activeSelf)
                    satellite.SetActive(false);
            }
        }
        
        private IEnumerator Heal(float newHealth, float timer)
        {
            for (; timer > 0; timer--)
            {
                _hp += (int)newHealth;
                if (_hp > maxhp)
                    _hp = maxhp;
                healthBar.value = (float)_hp/maxhp;
                yield return new WaitForSeconds(1f);
            }
        }
        
        public async void StartGhost()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = Color.gray;
            while (_ghost < 3f)
            {
                _ghost += 0.5f;
                await Task.Delay(500);
            }

            _spriteRenderer.color = Color.black;
            _ghost = 0;
        }
        

        private void Spawn()
        {
            transform.position = SpawnPoint.transform.position;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (isSpike)
            {
                    UseSpikes();
            }
            bool isEnemy = collision.gameObject.TryGetComponent(out Enemy _enemy);
            if (!isEnemy) return;
            GetDamage(_enemy.EnemyScores.damage); 
            int inflictedDamage = InflictedDamage(_enemy);
            _hp += (int)(inflictedDamage * lifeSteel);
            dealDamage?.Invoke();
        }

        public int InflictedDamage(Enemy enemy)
        {
           return enemy.TakeDamage(damage:(int)(Damage*bonusDamage));
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Enemy")) return;
            if (isDischarge)
            {
                    
            }
        }
    }
}