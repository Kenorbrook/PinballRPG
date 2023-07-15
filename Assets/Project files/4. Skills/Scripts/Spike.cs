using ProjectFiles.Enemies;
using UnityEngine;

namespace ProjectFiles.Skills
{
    public class Spike : MonoBehaviour
    {
        private const float FORCE = 500f;

        public static int damage;

        private void Start()
        {
            var _rotation = transform.rotation.eulerAngles.z;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(FORCE * Mathf.Cos(_rotation * Mathf.Deg2Rad),
                FORCE * Mathf.Sin(_rotation * Mathf.Deg2Rad)));
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            bool isEnemy = col.gameObject.TryGetComponent(out IChangeHealth _enemy);
            if (isEnemy)
            {
                _enemy.TakeDamage(damage: damage);
            }

            gameObject.SetActive(false);
        }
    }
}