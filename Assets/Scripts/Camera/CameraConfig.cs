using System;
using UnityEngine;

[Serializable]
class CameraConfig
{
    public Transform cameraStart;
    public Transform cameraEnd;
    public float maxZoom;
    public float minZoom;
}