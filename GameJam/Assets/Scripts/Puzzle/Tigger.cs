using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tigger : MonoBehaviour
{
    public bool isActive = false;
    [SerializeField]
    private Material mMaterial;
    [SerializeField]
    public Color mColorsActive;
    [SerializeField]
    public Color mColorsNoneActive;

    private void Update()
    {
        if (isActive)
        {
            mMaterial.color = mColorsActive;
        }
        else
        {
            mMaterial.color = mColorsNoneActive;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        isActive = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isActive = false;
    }
}
