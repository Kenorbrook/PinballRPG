using System.Collections.Generic;
using UnityEngine;

namespace ProjectFiles.MovingObjects
{
    public class MovingObjectOnPath : MonoBehaviour
    {
        public enum MovementType
        {
            Moving,
            Leaping
        }

        public MovementType movementType = MovementType.Moving;
        public MovementPath Path;
        public float speed = 1;
        public float maxDistance = .1f;

        private IEnumerator<Transform> _pointInPath;


        public void Start()
        {
            if (Path == null)
            {
                return;
            }

            _pointInPath = Path.GetNextPathPoint();

            _pointInPath.MoveNext();

        }


        private void Update()
        {
            if (_pointInPath == null || _pointInPath.Current == null)
            {
                return;
            }

            var _position = transform.position;
            var _position1 = _pointInPath.Current.position;
            _position = movementType switch
            {
                MovementType.Moving => Vector3.MoveTowards(_position, _position1,
                    Time.deltaTime * speed),
                MovementType.Leaping => Vector3.Lerp(_position, _position1,
                    Time.deltaTime * speed),
                _ => _position
            };
            transform.position = _position;
            var _disturbance = (_position - _position1).sqrMagnitude;
            if (_disturbance < maxDistance * maxDistance)
            {
                _pointInPath.MoveNext();
            }
        }
    }
}