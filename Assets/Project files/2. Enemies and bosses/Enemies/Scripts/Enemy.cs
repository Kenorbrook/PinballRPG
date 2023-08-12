using UnityEngine;

namespace ProjectFiles.Enemies
{
    public class Enemy : MonoBehaviour
    {
        private const float FORCE_REPELLING = 5f;

        private IChangeHealth _changeHealth;
        private SpriteRenderer _sprite;



        private void Start()
        {
            GetComponents();
            SetDefaultValues();
        }

        public void ResetEnemy()
        {
            _changeHealth.Reset();
            gameObject.SetActive(true);
        }
        private void SetDefaultValues()
        {
            _changeHealth.Init(_sprite);
        }

        private void GetComponents()
        {
            _changeHealth = GetComponent<IChangeHealth>();
            _sprite = GetComponent<SpriteRenderer>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;
            collision.rigidbody.velocity = Vector2.zero;
            collision.rigidbody.AddForce(-collision.contacts[0].normal * FORCE_REPELLING, ForceMode2D.Impulse);
        }


    }
}