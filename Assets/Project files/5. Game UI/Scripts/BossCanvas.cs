using UnityEngine;

namespace ProjectFiles.GameUI
{
    public class BossCanvas : MonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas;
        private void Start()
        {
            _canvas.planeDistance = 9;
            _canvas.worldCamera
                = Camera.main;
        }
    }
}