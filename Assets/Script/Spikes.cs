using Script;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField]
    private int damage;
    private float _force = 5f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        collision.rigidbody.velocity = Vector2.zero;
        collision.rigidbody.AddForce(-collision.contacts[0].normal * _force, ForceMode2D.Impulse);
        collision.gameObject.GetComponent<Player>().GetDamage(damage);
    }
}
