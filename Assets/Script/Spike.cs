using Script;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private static float force = 500f;

    public static int damage;
    // Start is called before the first frame update
    void Start()
    {
        var _rotation = transform.rotation.eulerAngles.z;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(force*Mathf.Cos(_rotation*Mathf.Deg2Rad),force*Mathf.Sin(_rotation*Mathf.Deg2Rad)));
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
