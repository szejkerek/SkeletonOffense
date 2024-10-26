using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class SplineManager : MonoBehaviour
{
    public SplineContainer Spline { get; private set; }
    [SerializeField] int SampleCount = 40;
    
    List<Waypoint> waypoints = new();

    private void Awake()
    {
        Spline = GetComponent<SplineContainer>();
        for (int i = 0; i < SampleCount; i++)
        {
            float t = (float) i / SampleCount; 
            waypoints.Add(new Waypoint(percentage: t, position: (Vector3)Spline.EvaluatePosition(t)));
        }
    }

    public Waypoint GetClosest(Vector3 point)
    {
        Waypoint closestWaypoint = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (var waypoint in waypoints)
        {
            float distanceSqr = Vector3.SqrMagnitude(waypoint.position - point);
            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestWaypoint = waypoint;
            }
        }

        return closestWaypoint;
    }
    public Waypoint GetNext(float t)
    {
        foreach (Waypoint waypoint in waypoints.Where(waypoint => waypoint.percentage > t))
        {
            return waypoint;
        }

        return waypoints.Last();
    }

    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Count == 0)
        {
            return;
        }

        Gizmos.color = Color.green;
        foreach (var waypoint in waypoints)
        {
            Gizmos.DrawSphere(waypoint.position.Add(y: 1) , 0.5f);
        }
    }
}