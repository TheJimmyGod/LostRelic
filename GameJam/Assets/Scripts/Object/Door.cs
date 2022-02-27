using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Environment
{
    private Vector3 mEndPosition;
    private Vector3 mStartPosition;
    [SerializeField]
    private float mSpeed = 10.0f;
    public GameObject mDoor;

    public List<SpecificButton> mTriggers;
    public AudioClip mDoorSound;

    private bool isOpened = false;
    private bool isComplete = false;
    private bool isPlayed = false;
    protected override void OnStart()
    {
        mStartPosition = mDoor.transform.position;
        mEndPosition = new Vector3(mDoor.transform.position.x, mDoor.transform.position.y + 10.0f, mDoor.transform.position.z);
    }

    protected override void OnUpdate()
    {
        if(isOpened == false)
        {
            for (int i = 0; i < mTriggers.Count; i++)
            {
                if (mTriggers[i].Active == false)
                {
                    isOpened = false;
                    break;
                }
                else
                    isOpened = true;
            }
        }


        if(isOpened)
        {
            if(isComplete == false)
            {
                if (isPlayed == false)
                {
                    AudioManager.PlaySfx(mDoorSound,0.85f);
                    isPlayed = true;
                }
                Vector3 heading = (mEndPosition - mDoor.transform.position).normalized;
                mDoor.transform.position += heading * mSpeed * Time.deltaTime;
                if(Vector3.Distance(mEndPosition, mDoor.transform.position) < 0.5f )
                {
                    isComplete = true;

                }
            }

        }
        else
        {
            Vector3 heading = (mStartPosition - mDoor.transform.position).normalized;
            mDoor.transform.position += heading * mSpeed * Time.deltaTime;
        }
    }

    public override void Interact(Transform player)
    {
        base.Interact(player);
    }
}
