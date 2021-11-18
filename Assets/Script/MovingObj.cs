using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObj : MonoBehaviour
{
   public enum MovementType{
        Moving,
        Lerping
    }

    public MovementType movementType = MovementType.Moving;
    public MovementPath Path;
    public float speed = 1;
    public float maxDistance = .1f;

    private IEnumerator<Transform> pointInPath;


    public void MyStart()
    {
        if (Path == null)
        {
            return;
        }

        pointInPath = Path.GetNextPathPoint();

        pointInPath.MoveNext();

        if(pointInPath.Current == null)
        {
            return;
        }
        transform.position = pointInPath.Current.position;
    }


    private void Update()
    {
        if(pointInPath == null || pointInPath.Current == null)
        {
            return;
        }
        if(movementType == MovementType.Moving)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime*speed);
        }
        else if (movementType == MovementType.Lerping)
        {
            transform.position = Vector2.Lerp(transform.position, pointInPath.Current.position, Time.deltaTime*speed);
        }
        var distancesqure = (transform.position - pointInPath.Current.position).sqrMagnitude;

        if(distancesqure < maxDistance * maxDistance)
        {
            pointInPath.MoveNext();
        }
    }
}
