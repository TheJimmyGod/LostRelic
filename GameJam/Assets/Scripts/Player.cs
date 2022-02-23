using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField]
    private int mHealth;
    [SerializeField]
    public int mAccessibleDist = 3;
    public LayerMask mTargetMask;
    public Transform mTarget;
    public int Health
    {
        get
        {
            return mHealth;
        }
        set
        {
            mHealth = value;
        }
    }

    public bool Death
    {
        get
        {
            return mHealth <= 0;
        }
    }

    public void TakeDamage(int dmg)
    {
        
    }

    public void Fire()
    {

    }

    public void Interact()
    {
        if (mTarget)
            Disconnect();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, mTargetMask))
        {
            if(Vector3.Distance(transform.position, hit.transform.position) < mAccessibleDist)
            {
                hit.transform.GetComponent<Environment>().Interact(this.transform);
                mTarget = hit.transform;
            }
        }
    }

    public void Disconnect()
    {
        mTarget.GetComponent<Environment>().Disconnect();
        mTarget = null;
    }
}
