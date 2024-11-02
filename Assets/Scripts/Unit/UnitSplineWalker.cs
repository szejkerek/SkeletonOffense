using UnityEngine;
using UnityEngine.Splines;

public class UnitSplineWalker : MonoBehaviour
{
    public float SplinePosition { get; private set; }

    SplineContainer splineContainer;
    UnitConfig config;

    public void Initialize(SplineManager newSpline, UnitConfig unitConfig)
    {
        config = unitConfig;
        splineContainer = newSpline.Spline;
    }

    public void SetSplinePosition(float splinePosition)
    {
        if (splineContainer == null)
        {
            return;
        }

        splinePosition = Mathf.Clamp01(splinePosition);
        SplinePosition = splinePosition;
    }

    public float WalkAlongSpline()
    {
        if (splineContainer == null)
        {
            Debug.LogWarning($"Spline is not set on {name}");
            return SplinePosition;
        }

        MoveUnit();
        RotateUnit();

        return SplinePosition;
    }

    void RotateUnit()
    {
        Vector3 tangent = splineContainer.Spline.EvaluateTangent(SplinePosition);
        transform.forward = tangent.normalized;
    }

    void MoveUnit()
    {
        SplinePosition += (config.walkSpeed * Time.deltaTime) / splineContainer.Spline.GetLength();
        SplinePosition = Mathf.Clamp01(SplinePosition);
        Vector3 position = splineContainer.Spline.EvaluatePosition(SplinePosition);
        transform.position = position + Vector3.up * (config.height / 2) + splineContainer.transform.position;
    }
}