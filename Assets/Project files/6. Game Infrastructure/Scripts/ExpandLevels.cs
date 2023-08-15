using UnityEngine;

public class ExpandLevels : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("sh/sw = " +(float)Screen.height / Screen.width);
        
            float scale = 2.0119f*Screen.width / Screen.height + 0.0131f;
            GetComponent<Transform>().localScale = new Vector3(scale ,scale,1);
        
    }

}
