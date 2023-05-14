using Script;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        bool isEnemy = other.TryGetComponent(out Enemy _enemy);
        if (!isEnemy) return;
        Player.player.InflictedDamage(_enemy);
    }
}
