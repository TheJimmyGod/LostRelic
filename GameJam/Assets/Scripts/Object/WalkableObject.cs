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
            transform.position = mPos;
        if (mTarget == null) return;
        if(mTarget.GetComponent<PlayerController>().State.ToString() == "MoveObjectState")
        {
            Vector3 dir = (transform.position - mTarget.transform.position).normalized;
            dir.y = 0.0f;
            rigidbody.AddForce(dir * 100.0f * Time.deltaTime, ForceMode.Force);
            transform.LookAt(mTarget);
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
