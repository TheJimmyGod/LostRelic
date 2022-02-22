using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkableObject : Environment, IDamagable
{
    private Rigidbody rigidbody;

    protected override void OnStart()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    protected override void OnUpdate()
    {
        if (mTarget == null) return;
        if (Vector3.Distance(mTarget.position, this.transform.position) < mVaildDistance)
        {
            if(mTarget.GetComponent<PlayerController>().State.ToString() == "MoveObjectState")
            {
                Vector3 dir = transform.position - mTarget.transform.position;
                rigidbody.AddForce(dir.normalized * 25.0f * Time.deltaTime, ForceMode.Force);
                transform.LookAt(mTarget);
            }
        }
        else
            mTarget = null;
    }
    public void TakeDamage(int dmg)
    {
        mHealth -= dmg;
    }

    public override void Interact(Transform player)
    {
        base.Interact(player);
    }
}
