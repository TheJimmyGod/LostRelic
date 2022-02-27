using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkableObject : Environment, IDamagable
{
    private new Rigidbody rigidbody;
    private Vector3 mPos;

    protected override void OnStart()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        mPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    protected override void OnUpdate()
    {
        if (transform.position.y < -15.0f)
        {
            transform.parent = null;
            transform.position = mPos;
        }
        if (mTarget == null) return;
        if(mTarget.GetComponent<PlayerController>().State.ToString() == "MoveObjectState")
        {
            if(Vector3.Distance(transform.position, mTarget.position) > 1.75f)
            {
                transform.position = Vector3.Lerp(transform.position, mTarget.position, Time.deltaTime * 2.0f);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(mTarget.position - transform.position), 5.0f * Time.deltaTime);
            }
            mTarget.LookAt(transform.position);
        }
    }
    public void TakeDamage(int dmg)
    {
        mHealth -= dmg;
    }

    public override void Interact(Transform player)
    {
        if (mCoolTime) return;
        base.Interact(player);
        rigidbody.isKinematic = false;
    }

    private IEnumerator GetKinematic()
    {
        mCoolTime = true;
        yield return new WaitForSeconds(1.5f);
        rigidbody.isKinematic = true;
        mCoolTime = false;
    }
}
