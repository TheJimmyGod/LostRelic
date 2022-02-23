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
    private float setHight;
    private float maxHight = 0;
    private float tiggerCount = 0;

    [SerializeField]
    private GameObject[] tiggers;

    private void Awake()
    {
        maxHight = setHight + door.transform.position.y;
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

        Debug.Log(tiggerCount);
        Debug.Log( tiggers.Length);
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
        if (doorIsOpening && door.transform.position.y < maxHight)
        {
            door.transform.Translate(Vector3.up * Time.deltaTime * doorSpeed);
        }
    }
}
