using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ControlState mState;
    public CharacterController mCharacterController;
    public Vector3 mVelocity = Vector3.zero;
    public bool isGrounded = true;

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

    private Animator mAnimator;

    private float mSpeed = 0.0f;
    private float mTrunSmoothVelocity = 0.0f;
    private Player mPlayer;
    public Grapple mGrapple;

    [SerializeField]
    private float mFootStepCooltime = 0.15f;
    [SerializeField]
    private float mCrouchStepCooltime = 0.6f;
    [SerializeField]
    private float mPullCooltime = 0.75f;
    private float mCurrentFootStepTime = 0.0f;
    private float mCurrentCrouchStepTime = 0.0f;
    private float mCurrentPullingTime = 0.0f;
    private bool isPlayed = false;
    private bool isPlayedForRope = false;

    public AudioClip[] mAudioClips;
    public AudioClip mDeathClip;
    public AudioClip mPullClip;
    public AudioClip mRopeClip;

    internal ControlState State { get => mState; set => mState = value; }

    void Start()
    {
        mState = new IdleState();
        mPlayer = gameObject.GetComponent<Player>();
        mCharacterController = GetComponent<CharacterController>();
        mAnimator = transform.Find("Character").gameObject.GetComponent<Animator>();
        mSpeed = mRunSpeed;
    }

    void Update()
    {
        mState = mState.Handle();
        if (Input.GetKeyDown(KeyCode.Escape))
            StartCoroutine(LevelManager.ReturnCurrentLevel());
        StateUpdate(ControlUpdate());

    }

    bool ControlUpdate()
    {
        if(transform.position.y < -10.0f && isGrounded == false && isPlayed == false)
        {
            if (isPlayed == false)
            {
                AudioManager.PlaySfx(mDeathClip, 1.0f);
                isPlayed = true;
            }
        }
        if (transform.position.y < -15.0f && isGrounded == false)
        {
            if (transform.GetComponent<Rigidbody>())
                transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            mPlayer.TakeDamage(mPlayer.Health + 1);
            if (isPlayed)
                isPlayed = false;
        }

        isGrounded = Physics.CheckSphere(mGroundCheck.position, mGroundDistance, mGroundMask);

        mAnimator.SetBool("Ground", isGrounded);
        if (mCharacterController.enabled == false)
        {
            if(mState.ToString() == "IdleState")
                mAnimator.SetBool("Roping", false);
            return false;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(x, 0.0f, z).normalized;

        mSpeed = ((isGrounded) && (mState.ToString() == "CrouchState"
            || mState.ToString() == "CrouchRunState" || mState.ToString() == "MoveObjectState")) ? mCrouchSpeed : mRunSpeed;

        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref mTrunSmoothVelocity, mTurnSmooth);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
            Vector3 moveDirection = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
            mCharacterController.Move(moveDirection.normalized * mSpeed * Time.deltaTime);
        }
        return true;
    }

    void StateUpdate(bool isEnabled)
    {
        if(mState.ToString() == "FireState")
        {
            if (mGrapple.isGrapped)
            {
                if(isPlayedForRope == false)
                {
                    if (!AudioManager.Instance.sfxSource.isPlaying)
                        AudioManager.PlaySfx(mRopeClip, 0.8f);
                    isPlayedForRope = true;
                }
                mAnimator.SetFloat("Speed", 0.0f);
                mAnimator.SetBool("Crouching", false);
                mAnimator.SetBool("Roping", true);
            }
        }
        if (!isEnabled)
            return;
        switch (mState.ToString())
        {
            case "IdleState":
                {
                    mAnimator.SetFloat("Speed", 0.0f);
                    mAnimator.speed = 1.0f;
                    mAnimator.SetBool("Jumping", false);
                    mAnimator.SetBool("Roping", false);
                    mAnimator.SetBool("Pulling", false);
                    mAnimator.SetBool("Crouching", false);
                    isPlayedForRope = false;
                }
                break;
            case "RunState":
                {
                    mAnimator.SetFloat("Speed", mSpeed);
                    if(isGrounded)
                    {
                        if (mCurrentFootStepTime <= 0.0f)
                        {
                            int random = Random.Range(0, 3);
                            AudioManager.PlaySfx(mAudioClips[random], 0.45f);
                            mCurrentFootStepTime = mFootStepCooltime;
                        }
                        else
                        {
                            mCurrentFootStepTime -= Time.deltaTime;
                        }
                    }

                }
                break;
            case "CrouchState":
                {
                    mAnimator.SetBool("Crouching", true);
                }
                break;
            case "CrouchRunState":
                {
                    mAnimator.SetFloat("Speed", mSpeed);
                    if (isGrounded)
                    {
                        if (mCurrentCrouchStepTime <= 0.0f)
                        {
                            int random = Random.Range(0, 3);
                            if (!AudioManager.Instance.sfxSource.isPlaying)
                                AudioManager.PlaySfx(mAudioClips[random], 0.10f);
                            mCurrentCrouchStepTime = mCrouchStepCooltime;
                        }
                        else
                        {
                            mCurrentCrouchStepTime -= Time.deltaTime;
                        }
                    }
                }
                break;
            case "InteractState":
                {
                    mPlayer.Interact();
                    if(mPlayer.mTarget)
                    {
                        mAnimator.SetBool("Crouching", false);
                        mAnimator.SetBool("Pulling", true);
                    }
                }
                break;
            case "MoveObjectState":
                {
                    if (isGrounded)
                    {
                        if (Input.GetKey(KeyCode.W) ||
                            Input.GetKey(KeyCode.A) ||
                            Input.GetKey(KeyCode.S) ||
                            Input.GetKey(KeyCode.D))
                        {
                            if (mCurrentPullingTime <= 0.0f)
                            {
                                if(!AudioManager.Instance.sfxSource.isPlaying)
                                    AudioManager.PlaySfx(mPullClip, 0.70f);
                                mCurrentPullingTime = mPullCooltime;
                            }
                            else
                            {
                                mCurrentPullingTime -= Time.deltaTime;
                            }
                        }

                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        mAnimator.SetBool("Pulling", false);
                        mState = new IdleState();
                    }
                    else if(mPlayer.mTarget == null)
                    {
                        mAnimator.SetBool("Pulling", false);
                        mState = new IdleState();
                    }
                }
                break;
            case "JumpState":
                {
                    mAnimator.SetBool("Crouching", false);
                    mAnimator.SetBool("Jumping", true);
                    mVelocity.y = Mathf.Sqrt(mJumpHeight * -2.0f * mGravity);
                    if (mVelocity.y >= mJumpHeight)
                        mState = new LandState();
                }
                break;
            case "LandState":
            case "LandMoveState":
                {
                    mAnimator.speed = 1.25f;
                    if (isGrounded && mVelocity.y <= 0.0f)
                    {
                        int random = Random.Range(0, 3);
                        if (!AudioManager.Instance.sfxSource.isPlaying)
                            AudioManager.PlaySfx(mAudioClips[random], 0.70f);
                        mAnimator.SetBool("Jumping", false);
                        mVelocity.y = -2.0f;
                        mState = new IdleState();
                        mAnimator.speed = 1.0f;
                    }
                }
                break;
            default:
                break;
        }
        mVelocity.y += mGravity * Time.deltaTime;

        mCharacterController.Move(mVelocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(mState.ToString() == "FireState")
        {
            mGrapple.StopGrapple();
            mState = new IdleState();
        }
    }


}
