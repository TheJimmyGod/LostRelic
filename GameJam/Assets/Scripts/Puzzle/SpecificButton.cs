using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificButton : MonoBehaviour
{
    private bool isActive = false;
    public bool Active { get { return isActive; } }
    public GameObject mKeyObject;
    [SerializeField]
    private Material mMaterial;
    [SerializeField]
    public Color mColorsActive;
    [SerializeField]
    public Color mColorsNoneActive;

    public AudioClip ButtonOnClip;
    public AudioClip ButtonOffClip;

    void Start()
    {
        mMaterial.color = mColorsNoneActive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject == mKeyObject)
        {
            AudioManager.PlaySfx(ButtonOnClip);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.gameObject == mKeyObject)
        {
            mMaterial.color = mColorsActive;
            isActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject == mKeyObject)
        {
            mMaterial.color = mColorsNoneActive;
            isActive = false;
            AudioManager.PlaySfx(ButtonOffClip);
        }
    }
}
