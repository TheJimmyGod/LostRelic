using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkableObject : Environment, IDamagable
{
    private Rigidbody rigidbody;
   

    protected override void OnStart()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
    }

    protected override void OnUpdate()
    {
        if (mTarget == null) return;
        if (Vector3.Distance(mTarget.position, this.transform.position) < mTarget.GetComponent<Player>().mAccessibleDist)
        {
            if(mTarget.GetComponent<PlayerController>().State.ToString() == "MoveObjectState")
            {
                Vector3 dir = transform.position - mTarget.transform.position;
                dir.y = 0.0f;
                rigidbody.AddForceAtPosition(dir * Time.deltaTime, transform.position,ForceMode.Force);
                transform.LookAt(mTarget);
            }
        }
        else
        {
            mTarget.GetComponent<Player>().Disconnect();
            mTarget = null;
            StartCoroutine(GetKinematic());
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
