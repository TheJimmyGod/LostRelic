using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject mCamera;
    private CinemachineFreeLook mFreeLook;
    // Start is called before the first frame update
    void Awake()
    {
        mFreeLook = mCamera.GetComponent<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            mFreeLook.m_XAxis.m_MaxSpeed = 500.0f;
        }
        if(Input.GetMouseButtonUp(1))
        {
            mFreeLook.m_XAxis.m_MaxSpeed = 0.0f;
        }
        if(Input.mouseScrollDelta.y != 0)
        {
            mFreeLook.m_YAxis.m_MaxSpeed = 10.0f;
        }
    }
}
