using System;
using UnityEngine;

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