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
        public int movementDirection = 1;
        public int movingTo;
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
                yield return PathElements[movingTo];

                if (PathElements.Length == 1)
                {
                    continue;
                }

                if (pathType == PathType.Liner)
                {
                    if (movingTo <= 0)
                    {
                        movementDirection = 1;
                    }
                    else if (movingTo >= PathElements.Length - 1)
                    {
                        movementDirection = -1;
                    }
                }

                movingTo += movementDirection;

                if (pathType != PathType.Loop) continue;
                if (movingTo >= PathElements.Length)
                {
                    movingTo = 0;
                }

                if (movingTo < 0)
                {
                    movingTo = PathElements.Length - 1;
                }
            }
        }
    }
}