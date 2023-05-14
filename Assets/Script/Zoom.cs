using System.Collections;
using System.Collections.Generic;
using Script.Managers;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    Vector3 zoom = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = (prevTouchDeltaMag - touchDeltaMag) / 100;

            if (zoom.x <= 1 & deltaMagnitudeDiff > 0)
            {
                deltaMagnitudeDiff = 0;
            }

            if (zoom.x >= 3 & deltaMagnitudeDiff < 0)
            {
                deltaMagnitudeDiff = 0;
            }

            zoom = new Vector3(zoom.x -= deltaMagnitudeDiff, zoom.y -= deltaMagnitudeDiff, 1);
            if (EditManager.ChoosenObject!=null) 
                EditManager.ChoosenObject.transform.localScale = zoom;
        }
    }
}