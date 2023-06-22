using ProjectFiles.Enemies;
using UnityEngine;

namespace ProjectFiles.Skills
{
    public class Satellite : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            bool isEnemy = other.TryGetComponent(out Enemy _enemy);
            if (!isEnemy) return;
            Player.Player.player.InflictedDamage(_enemy);
        }
    }
}