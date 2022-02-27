using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private GameObject mPlayer;
    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == mPlayer || other.tag == "Objective")
            other.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == mPlayer || other.tag == "Objective")
        {
            other.transform.parent = null;
            mPlayer.transform.parent = null;
        }
    }
}
