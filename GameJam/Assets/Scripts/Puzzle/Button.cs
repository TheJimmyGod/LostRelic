using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private bool doorIsOpening = false;
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private float doorSpeed;
    [SerializeField]
    private Transform endTransform;
    private float maxHight = 0;
    private float tiggerCount = 0;
    private Vector3 targetPosition;
    private Vector3 startPosition;

    [SerializeField]
    private GameObject[] tiggers;

    private void Awake()
    {
        startPosition = door.transform.position;
    }

    private void Update()
    {
       
        //Active 
        for (int i = 0; i < tiggers.Length; ++i)
        {
            if (tiggers[i].GetComponent<Tigger>().isActive)
            {
                tiggerCount++;
            }
        }

        //Debug.Log(tiggerCount);
        //Debug.Log( tiggers.Length);
        if (tiggerCount == tiggers.Length)
        {
            doorIsOpening = true;
            tiggerCount = 0;
        }
        else
        {
            tiggerCount = 0;
        }

        //Open door
        if (doorIsOpening && door.transform.position != endTransform.position)
        {
            MoveDoor(endTransform.position);
        }
        else if(!doorIsOpening && door.transform.position != startPosition)
        {
            MoveDoor(startPosition);
        }
    }

    void MoveDoor(Vector3 targetPosition)
    {
        Vector3 heading = targetPosition - door.transform.position;
        door.transform.position += (heading / heading.magnitude) * doorSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        doorIsOpening = false;
    }
}
