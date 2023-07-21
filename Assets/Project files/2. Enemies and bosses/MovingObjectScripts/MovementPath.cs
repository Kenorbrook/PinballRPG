using System.Collections.Generic;
using UnityEngine;

namespace ProjectFiles.MovingObjects
{
    public class MovementPath : MonoBehaviour
    {
        public enum PathType
        {
            Liner,
            Loop
        }

        public PathType pathType;
        private int _movementDirection = 1;
        private int _movingTo;
        public Transform[] PathElements;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            if (PathElements == null || PathElements.Length < 2)
            {
                return;
            }

            for (int i = 1; i < PathElements.Length; i++)
            {
                Gizmos.DrawLine(PathElements[i - 1].position, PathElements[i].position);
            }

            if (pathType == PathType.Loop)
            {
                Gizmos.DrawLine(PathElements[0].position, PathElements[PathElements.Length - 1].position);
            }
        }

        public IEnumerator<Transform> GetNextPathPoint()
        {
            if (PathElements == null || PathElements.Length < 1)
                yield break;
            while (true)
            {
                yield return PathElements[_movingTo];

                if (PathElements.Length == 1)
                {
                    continue;
                }

                if (pathType == PathType.Liner)
                {
                    if (_movingTo <= 0)
                    {
                        _movementDirection = 1;
                    }
                    else if (_movingTo >= PathElements.Length - 1)
                    {
                        _movementDirection = -1;
                    }
                }

                _movingTo += _movementDirection;

                if (pathType != PathType.Loop) continue;
                if (_movingTo >= PathElements.Length)
                {
                    _movingTo = 0;
                }

                if (_movingTo < 0)
                {
                    _movingTo = PathElements.Length - 1;
                }
            }
        }
    }
}