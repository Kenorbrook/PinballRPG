using UnityEngine;

public class Magnetism : MonoBehaviour
{
    public int force = 20;
    public Transform navigationObj;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spike"))
        {
            if (force > 0) force = -force;
            navigationObj = other.transform;
        }

        if (other.CompareTag("Enemy"))
        {
            if (force < 0) force = -force;
            navigationObj = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Spike") || other.CompareTag("Enemy"))
        {
            navigationObj = null;
        }
    }
}