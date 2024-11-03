using UnityEngine;

[System.Serializable]
public class TargetInfo
{
    public IDamagable target;
    public Waypoint nearestWaypoint;
    public Vector3 sampledStandPosition;

    public void SetTarget(IDamagable target)
    {
        this.target = target;
    }

    public void SetWaypoint(Waypoint nearestWaypoint)
    {
        this.nearestWaypoint = nearestWaypoint;
    }

    public void SetPosition(Vector3 sampledStandPosition)
    {
        this.sampledStandPosition = sampledStandPosition;
    }

}
