using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas>().planeDistance = 9;
        GetComponent<Canvas>().worldCamera 
        = Camera.main;
    }

   
}
