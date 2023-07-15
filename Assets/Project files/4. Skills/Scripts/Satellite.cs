using ProjectFiles.Enemies;
using UnityEngine;

namespace ProjectFiles.Skills
{
    public class Satellite : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            bool isEnemy = other.TryGetComponent(out IChangeHealth _enemy);
            if (!isEnemy) return;
            Player.Player.player.InflictedDamage(_enemy);
            
        }
    }
}