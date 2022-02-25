using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    private float tolerance;
    private float waitStart;
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Transform movingPlatform;
    [SerializeField]
    private Transform startTransform;
    [SerializeField]
    private Transform endTransform;
    [SerializeField]
    private bool isAutomaticMoving;
    private Vector3 targetPosition;

    private void Awake()
    {
        targetPosition = startTransform.position;
        tolerance = moveSpeed * Time.deltaTime;
    }

    private void Update()
    {

        if (movingPlatform.transform.position != targetPosition)
        {
            MovingPlatform();
        }
        else
        {
            ChangeTarget();
        }

    }

    private void MovingPlatform()
    {
        Vector3 heading = targetPosition - movingPlatform.transform.position;
        movingPlatform.transform.position += (heading / heading.magnitude) * moveSpeed * Time.deltaTime;
        if(heading.magnitude < tolerance)
        {
            movingPlatform.transform.position = targetPosition;
            waitStart = Time.time;
        }
    }


    private void ChangeTarget()
    {
        if (isAutomaticMoving)
        {
            if(Time.time - waitStart > waitTime)
            {
                NextPosition();
            }
        }
    }

    private void NextPosition()
    {
        if (targetPosition == startTransform.position)
        {
            targetPosition = endTransform.position;
        }
        else
        {
            targetPosition = startTransform.position;
        }

    }

    public void SetisAutomaticMoving()
    {
        isAutomaticMoving = true;
    }
}
