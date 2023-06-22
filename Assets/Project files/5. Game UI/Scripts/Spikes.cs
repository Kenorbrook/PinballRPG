using UnityEngine;

namespace ProjectFiles.GameUI
{
    public class Spikes : MonoBehaviour
    {
        [SerializeField]
        private int damage;

        private const float FORCE = 5f;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;
            collision.rigidbody.velocity = Vector2.zero;
            collision.rigidbody.AddForce(-collision.contacts[0].normal * FORCE, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<Player.Player>().GetDamage(damage);
        }
    }
}