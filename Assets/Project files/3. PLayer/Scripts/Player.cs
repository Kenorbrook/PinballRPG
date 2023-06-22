using System;
using System.Collections;
using System.Threading.Tasks;
using ProjectFiles.Enemies;
using ProjectFiles.LevelInfrastructure;
using ProjectFiles.Skills;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectFiles.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(TrailRenderer))]
    public class Player : MonoBehaviour
    {
        private static int health;
        private static int _hp;
        private const int MAX_HEALTH_POINT = 100;
        public static Player player;
        private Rigidbody2D _rb2d;

        private TrailRenderer _trail;

        [SerializeField]
        private float VerticalSpeed;

        [SerializeField]
        private float HorizontalSpeed;

        public GameObject SpawnPoint;

        private float _ghost;

        private Slider healthBar;

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

        [SerializeField]
        private Discharge _discharge;

        [SerializeField]
        private Magnetism _magnetism;

        [SerializeField]
        private GameObject _spikes;

        [SerializeField]
        private GameObject[] _satellite;

        private SpriteRenderer _spriteRenderer;

        // Start is called before the first frame update
        private void Start()
        {
            Spawn();
            isSpike = false;
            _discharge.gameObject.SetActive(false);
            _hp = MAX_HEALTH_POINT;
            health = 3;
//            healthBar.value = (float) _hp / MAX_HEALTH_POINT;
            
            _trail = GetComponent<TrailRenderer>();
            _rb2d = GetComponent<Rigidbody2D>();
            // StartPos = player.transform.localPosition.y;
        }

        private void FixedUpdate()
        {
            if (_magnetism.gameObject.activeSelf)
            {
                if (_magnetism.navigationObj == null) return;
                _rb2d.AddForce((_magnetism.navigationObj.transform.position - transform.position).normalized *
                               _magnetism.force);
            }
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
            if (_ghost > 0 || damage == 0) return;
            _hp -= (int) (damage * (1 - defence));
            if (_hp <= 0)
            {
                if (health == 0)
                {
                    GameManager.LoseGame();
                    return;
                }

                health--;
                _hp = MAX_HEALTH_POINT;
            }

            healthBar.value = (float) _hp / MAX_HEALTH_POINT;
            GameManager.instance.UpdateUiHp();

            Debug.Log("HP-" + _hp);
            StartGhost();
        }

        public void StartHeal(float newHealth, float timer)
        {
            StartCoroutine(Heal(newHealth, timer));
        }

        private void UseSpikes()
        {
            Instantiate(_spikes, transform);
        }


        public void UseDischarge(int damage)
        {
            _discharge.gameObject.SetActive(true);
            _discharge.damage = damage;
        }

        public void OffDischarge()
        {
            _discharge.gameObject.SetActive(false);
        }

        public void UseMagnetism()
        {
            _magnetism.gameObject.SetActive(true);
        }

        public void OffMagnetism()
        {
            _magnetism.gameObject.SetActive(false);
        }

        public static void DisablePlayer()
        {
            player.gameObject.SetActive(false);
            player.TrailEmitting();
        }


        public static void EnablePlayer()
        {
            player.gameObject.SetActive(true);
            player.transform.position =
                new Vector3(0,
                    TransformationCamera.mainCamera.transform.position.y -
                    TransformationCamera.mainCamera.orthographicSize + 0.5f, 0);
            player.TrailEmitting();
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
                if (satellite.activeSelf)
                    satellite.SetActive(false);
            }
        }

        private IEnumerator Heal(float newHealth, float timer)
        {
            for (; timer > 0; timer--)
            {
                _hp += (int) newHealth;
                if (_hp > MAX_HEALTH_POINT)
                    _hp = MAX_HEALTH_POINT;
                healthBar.value = (float) _hp / MAX_HEALTH_POINT;
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
            _hp += (int) (inflictedDamage * lifeSteel);
            dealDamage?.Invoke();
        }

        public int InflictedDamage(Enemy enemy)
        {
            return enemy.TakeDamage(damage: (int) (Damage * bonusDamage));
        }
    }
}