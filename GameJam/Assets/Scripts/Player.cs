using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField]
    private int mHealth;
    [SerializeField]
    private int mVaildDistance = 400;
    public LayerMask mTargetMask;
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
        Collider[] collider = Physics.OverlapSphere(transform.localPosition, mVaildDistance, mTargetMask);
        if (collider.Length == 0) return;
        Transform nearestObject = null;
        float minimumDist = float.MaxValue;
        for (int i = 0; i < collider.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, collider[i].transform.position);
            if (minimumDist > dist)
            {
                minimumDist = dist;
                nearestObject = collider[i].transform;
            }
        }

        nearestObject?.transform.GetComponent<Environment>().Interact(this.transform);
    }
}
