using UnityEngine;
using UnityEngine.UI;

namespace ProjectFiles.GameUI
{
    public class VersionHandler : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Text>().text = Application.version;
        }
    }
}