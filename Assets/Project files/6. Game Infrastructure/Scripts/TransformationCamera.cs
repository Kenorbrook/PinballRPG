using System.Collections;
using UnityEngine;

namespace ProjectFiles.LevelInfrastructure
{
    [RequireComponent(typeof(Camera))]
    public class TransformationCamera : MonoBehaviour
    {
        [SerializeField]
        private float _speedChangeLevel;

        public static Camera mainCamera;

        private static bool isCameraLookAtTheLevel => 
            mainCamera.transform.position.y < GameManager.CurrentLevels[GameManager.Level].transform.position.y;

        private void Awake()
        {
            mainCamera = Camera.main;
            //ChangeCameraSize();
        }

        private static void ChangeCameraSize()
        {
            
            float _ratio = 1f * Screen.height / Screen.width;
            if (mainCamera != null) mainCamera.orthographicSize = 2.5f * _ratio;
        }


        public IEnumerator MoveCamera()
        {
            Player.Player.DisablePlayer();
            while (isCameraLookAtTheLevel)
            {
                mainCamera.transform.position += new Vector3(0, Time.deltaTime * 300 * _speedChangeLevel, 0);

                yield return null;
            }

            Player.Player.EnablePlayer(GameManager.CurrentLevels[GameManager.Level].spawnPoint);
        }
    }
}