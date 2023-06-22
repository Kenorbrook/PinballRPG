using UnityEngine;

namespace ProjectFiles.LevelInfrastructure
{
    public class RotationObject : MonoBehaviour
    {
        [SerializeField]
        private float _rotationSpeed=1;
        private void FixedUpdate()
        {
            transform.Rotate(0, 0, _rotationSpeed);
        }
    }
}