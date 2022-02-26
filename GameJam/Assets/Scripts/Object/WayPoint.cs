using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private WayPointManager mManager;
    [SerializeField]
    private int id = 0;
    [SerializeField]
    private bool isComplete = false;
    public int ID { get { return id; } }
    
    void Start()
    {
        mManager = transform.parent.GetComponent<WayPointManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            if(isComplete)
            {
                StartCoroutine(LevelManager.GoNextLevel());
                return;
            }

            var comp = other.GetComponent<Player>();
            if (comp.mWaypoint.ID < ID)
            {
                comp.mWaypoint.mCheckPoint = transform.position;
                comp.mWaypoint.ID = ID;
            }
           
            mManager.RemoveWaypoint(this);
        }
    }
}
