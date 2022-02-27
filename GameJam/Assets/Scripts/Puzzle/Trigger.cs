using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool isActive = false;
    [SerializeField]
    private Material mMaterial;
    [SerializeField]
    public Color mColorsActive;
    [SerializeField]
    public Color mColorsNoneActive;
    [SerializeField]
    public GameObject MovePlatform;

    public AudioClip ButtonOnClip;
    public AudioClip ButtonOffClip;

    private void Awake()
    {
        mMaterial.color = mColorsNoneActive;
    }

    private void Update()
    {
        if (isActive)
        {
            mMaterial.color = mColorsActive;
            if (MovePlatform != null)
            {
                MovePlatform.GetComponent<MovePlatform>().SetisAutomaticMoving();
            }
        }
        else
        {
            if (MovePlatform == null)
            {
                mMaterial.color = mColorsNoneActive;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        AudioManager.PlaySfx(ButtonOnClip);
    }
    private void OnTriggerStay(Collider other)
    {
        isActive = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isActive = false;
        AudioManager.PlaySfx(ButtonOffClip);
    }
}
