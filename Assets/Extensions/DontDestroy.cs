using UnityEngine;

namespace Extensions
{
    public class DontDestroy : MonoBehaviour
    {
        private void Awake()
        {
            for(int i =0; i < FindObjectsOfType<DontDestroy>().Length; i++)
            {
                if (FindObjectsOfType<DontDestroy>()[i] != this)
                {
                    Destroy(gameObject);
                }
            }
            DontDestroyOnLoad(gameObject);
        }

    }
}
