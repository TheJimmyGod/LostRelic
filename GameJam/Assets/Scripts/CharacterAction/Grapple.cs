using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField]
    private float mMaxDistance = 100.0f;
    [SerializeField]
    private float mPower = 1000.0f;
    [SerializeField]
    private float mCoolTime = 1.5f;
    private float mCurrentCoolTime = 0.0f;
    private Vector3 mGrapplePoint;
    private LineRenderer mLineRenderer;
    private SpringJoint mSpringJoint;
    public LayerMask mLayerMask;
    public Transform mPlayer;
    public Transform mStartPos;
    public bool isGrapped = false;

    void Awake()
    {
        mLineRenderer = GetComponent<LineRenderer>();
        mLineRenderer.enabled = false;
    }

    void Update()
    {
        if(mCurrentCoolTime > 0.0f)
        {
            mCurrentCoolTime -= Time.deltaTime;
            return;
        }
        if (Input.GetMouseButtonDown(0))
            StartGrapple();
        else if (Input.GetMouseButtonUp(0))
            StopGrapple();
    }

    void LateUpdate()
    {
        DrawRope();
    }

    private void StartGrapple()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, mMaxDistance,mLayerMask))
        {
            mLineRenderer.enabled = true;
            isGrapped = true;
            mGrapplePoint = hit.point;
            mSpringJoint = mPlayer.gameObject.AddComponent<SpringJoint>();
            mSpringJoint.autoConfigureConnectedAnchor = false;
            mSpringJoint.connectedAnchor = mGrapplePoint;

            float distFromPoint = Vector3.Distance(mPlayer.position, mGrapplePoint);

            mSpringJoint.maxDistance = distFromPoint * 0.8f;
            mSpringJoint.minDistance = distFromPoint * 0.25f;

            mSpringJoint.spring = 4.5f;
            mSpringJoint.damper = 5.0f;
            mSpringJoint.massScale = 3.5f;

            mLineRenderer.positionCount = 2;
            mPlayer.GetComponent<PlayerController>().mCharacterController.enabled = false;
            Vector3 dir = (mGrapplePoint - mPlayer.position).normalized;
            dir.y = 0.0f;
            mPlayer.GetComponent<Rigidbody>().AddForce(dir* mPower, ForceMode.Force);
        }
    }

    private void DrawRope()
    {
        if (!mSpringJoint)
            return;

        mLineRenderer.SetPosition(0, mStartPos.position);
        mLineRenderer.SetPosition(1, mGrapplePoint);
    }

    public void StopGrapple()
    {
        if (isGrapped == false)
            return;
        mLineRenderer.positionCount = 0;
        mCurrentCoolTime = mCoolTime;
        Destroy(mSpringJoint);
        StartCoroutine(DelayedRemoveRigidBody());
    }

    private IEnumerator DelayedRemoveRigidBody()
    {
        mPlayer.GetComponent<Rigidbody>().AddForce(mPlayer.position.normalized * 20.0f, ForceMode.Force);
        mPlayer.GetComponent<PlayerController>().State = new LandState();
        yield return new WaitUntil(() => mPlayer.GetComponent<PlayerController>().isGrounded == true 
        && mPlayer.GetComponent<Rigidbody>().velocity.y <= 0.0f);
       
        mPlayer.GetComponent<PlayerController>().mCharacterController.enabled = true;
        mPlayer.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Destroy(mPlayer.GetComponent<Rigidbody>());
        mLineRenderer.enabled = false;
        isGrapped = false;
    }
}
