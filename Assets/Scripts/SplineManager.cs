using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class SplineManager : MonoBehaviour
{
    public int SampleCount = 40;
    public List<Waypoint> waypoints = new();

    public SplineContainer Spline => spline;
    SplineContainer spline;

    private void Awake()
    {
        spline = GetComponent<SplineContainer>();
        for (int i = 0; i < SampleCount; i++)
        {
            float t = (float) i / SampleCount; 
            waypoints.Add(new Waypoint(percentage: t, position: (Vector3)spline.EvaluatePosition(t)));
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
        foreach (var waypoint in waypoints)
        {
            if (waypoint.percentage > t)
                return waypoint;
        }

        return waypoints.Last();
    }

    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Count == 0)
            return;
        Gizmos.color = Color.green;
        foreach (var waypoint in waypoints)
        {
            Gizmos.DrawSphere(waypoint.position.Add(y: 1) , 0.5f);
        }
    }
}

[Serializable]
public class Waypoint
{
    public Waypoint(float percentage, Vector3 position)
    {
        this.percentage = percentage;
        this.position = position;
    }
    public float percentage;
    public Vector3 position;
}
