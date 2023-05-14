using Script;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private static float force = 50f;

    public static int damage;
    // Start is called before the first frame update
    void Start()
    {
        var _rotation = transform.rotation;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(force*Mathf.Cos(Mathf.Deg2Rad*_rotation.z),force*Mathf.Sin(Mathf.Deg2Rad*_rotation.z)));
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        bool isEnemy = col.gameObject.TryGetComponent(out Enemy _enemy);
        if (isEnemy)
        {
            _enemy.TakeDamage(damage: damage);
        }

        gameObject.SetActive(false);
    }
}
