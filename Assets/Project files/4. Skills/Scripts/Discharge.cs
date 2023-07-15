using System.Threading.Tasks;
using ProjectFiles.Enemies;
using UnityEngine;

namespace ProjectFiles.Skills
{
    public class Discharge : MonoBehaviour
    {
        [HideInInspector]
        public int damage = 20;

        private readonly RaycastHit2D[] _hits = new RaycastHit2D[10];
        private ContactFilter2D _filter2D;

        [SerializeField]
        private LineRenderer _effect;

        private void Start()
        {
            _filter2D = new ContactFilter2D();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Enemy")) return;

            var _position = transform.position;
            int lenght = Physics2D.Raycast(_position, col.transform.position - _position, _filter2D,
                _hits);

            for (int objectNumber = 0; objectNumber < lenght; objectNumber++)
            {
                if (_hits[objectNumber].transform == col.transform)
                {
                    break;
                }

                if (_hits[objectNumber].collider.gameObject.layer == 8) return;
            }


            Debug.Log("Hit enemy with skill");
            Effect(col.transform.position);
            IChangeHealth _enemy = col.GetComponent<IChangeHealth>();
            _enemy.TakeDamage(damage);
        }

        private async void Effect(Vector3 lastPoint)
        {
            _effect.SetPosition(1, lastPoint);
            _effect.SetPosition(0, transform.position);
            _effect.enabled = true;
            await Task.Delay(100);
            _effect.enabled = false;
        }
    }
}