using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField]
    private int mHealth;
    [SerializeField]
    public int mAccessibleDist = 5;
    public LayerMask mTargetMask;
    public Transform mTarget;

    public struct WaypointInfo
    {
        public WaypointInfo(Vector3 pos, int id)
        {
            mCheckPoint = pos;
            ID = id;
        }
        public Vector3 mCheckPoint;
        public int ID;
    }

    public WaypointInfo mWaypoint;

    void Start()
    {
        mWaypoint = new WaypointInfo();
        mWaypoint.mCheckPoint = transform.position;
        mWaypoint.ID = 0;
    }

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
        mHealth -= dmg;
        if(mHealth <= 0)
        {
            Rebirth();
        }
    }

    public void Rebirth()
    {
        if(Death)
        {
            mHealth = 100;
            transform.position = new Vector3(mWaypoint.mCheckPoint.x, mWaypoint.mCheckPoint.y + 2.0f, mWaypoint.mCheckPoint.z);
        }
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
