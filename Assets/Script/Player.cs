using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(TrailRenderer))]
public class Player : MonoBehaviour
{
    private float _hp=100;
    public static Player player;
    Rigidbody2D rb2d;
    [SerializeField] float VerticalSpeed;
    [SerializeField] float HorizontalSpeed;
    public GameObject SpawnPoint;
   // public float StartPos;
    public float damage=25f;
    // Start is called before the first frame update
    void Start()
    {
        if(player != null)
        {
            Destroy(gameObject);
        }
        player = this;
        rb2d = GetComponent<Rigidbody2D>();
        Spawn();
        // StartPos = player.transform.localPosition.y;
    }


    public void GoUp()
    {
        int direction = gameObject.transform.position.x == 0 ? 1 : (int)(gameObject.transform.position.x / Mathf.Abs(gameObject.transform.position.x));


        rb2d.AddForce(new Vector2(-VerticalSpeed*direction, HorizontalSpeed));
    }

    public void TrailEmmiting()
    {
        var trail = GetComponent<TrailRenderer>();
        trail.Clear();
        trail.emitting = !trail.emitting;

    }
    public void Spawn()
    {
        player.transform.position = SpawnPoint.transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            _hp -= collision.gameObject.GetComponent<Enemy>().EnemySccores.damage;
        }
    }
}
