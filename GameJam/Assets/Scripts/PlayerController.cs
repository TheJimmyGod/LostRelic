using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ControlState mState;
    private CharacterController mCharacterController;
    private Vector3 mVelocity = Vector3.zero;
    private bool isGrounded = true;

    [SerializeField]
    private float mRunSpeed = 7.0f;
    [SerializeField]
    private float mCrouchSpeed = 2.0f;
    [SerializeField]
    private float mGravity = -9.8f;
    [SerializeField]
    private float mTurnSmooth = 0.5f;
    [SerializeField]
    private float mJumpHeight = 2.0f;
    [SerializeField]
    private float mGroundDistance = 0.75f;

    public Transform mCamera;
    public Transform mGroundCheck;
    public LayerMask mGroundMask;

    private float mSpeed = 0.0f;
    private float mTrunSmoothVelocity = 0.0f;
    private Player mPlayer;

    internal ControlState State { get => mState; set => mState = value; }

    void Start()
    {
        mState = new IdleState();
        mPlayer = gameObject.GetComponent<Player>();
        mCharacterController = GetComponent<CharacterController>();
        mSpeed = mRunSpeed;
    }

    void Update()
    {
        mState = mState.Handle();

        if (mState.ToString() == "InteractState")
            mPlayer.Interact();

        isGrounded = Physics.CheckSphere(mGroundCheck.position, mGroundDistance, mGroundMask);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(x, 0.0f, z).normalized;

        mSpeed = ((isGrounded) && (mState.ToString() == "CrouchState" 
            || mState.ToString() == "CrouchRunState" || mState.ToString() == "MoveObjectState")) ? mCrouchSpeed : mRunSpeed;
        if(direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref mTrunSmoothVelocity,mTurnSmooth);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

            Vector3 moveDirection = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
            mCharacterController.Move(moveDirection.normalized * mSpeed * Time.deltaTime);
        }


        switch (mState.ToString())
        {
            case "JumpState":
                {
                    mVelocity.y = Mathf.Sqrt(mJumpHeight * -2.0f * mGravity);
                    if (mVelocity.y >= mJumpHeight)
                        mState = new LandState();
                }
                break;
            case "LandState":
                {
                    if (isGrounded && mVelocity.y < 0.0f)
                    {
                        mVelocity.y = -2.0f;
                        mState = new IdleState();
                    }
                }
                break;
            default:
                break;
        }

        mVelocity.y += mGravity * Time.deltaTime;
        mCharacterController.Move(mVelocity * Time.deltaTime);
    }
}
