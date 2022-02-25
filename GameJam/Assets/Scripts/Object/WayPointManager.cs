using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    [SerializeField]
    private List<WayPoint> mWayPoints;
    void Awake()
    {
        mWayPoints = new List<WayPoint>();
        foreach(var waypoint in GameObject.FindGameObjectsWithTag("WayPoint"))
        {
            mWayPoints.Add(waypoint.GetComponent<WayPoint>());
        }
    }
    public void RemoveWaypoint(WayPoint way)
    {
        mWayPoints.Remove(way);
        Destroy(way.gameObject);
    }
}
