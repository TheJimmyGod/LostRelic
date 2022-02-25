using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player" || other.tag == "Objective")
            other.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player" || other.tag == "Objective")
                other.transform.parent = null;
    }
}
