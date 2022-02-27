using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            other.GetComponent<Player>().TakeDamage(other.GetComponent<Player>().Health + 1);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            other.GetComponent<Player>().TakeDamage(other.GetComponent<Player>().Health + 1);
        }
    }
}
