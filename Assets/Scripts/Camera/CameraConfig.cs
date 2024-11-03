using System;
using UnityEngine;

[Serializable]
public class CameraConfig
{
    public Transform cameraStart;
    public Transform cameraEnd;

    [Range(0f, 50f)] public float xConstrains = 20f;
    
    public float maxZoom;
    public float minZoom;
}