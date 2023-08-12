using UnityEngine;

public class ExpandLevels : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("sh/sw = " +(float)Screen.height / Screen.width);
        if ((float)Screen.height / Screen.width > 2)
        {
            float scale = 3 - (float) Screen.height / Screen.width;
            GetComponent<Transform>().localScale = new Vector3(scale ,scale,1);
        }
    }

}
