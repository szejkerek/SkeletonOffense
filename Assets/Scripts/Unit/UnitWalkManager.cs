using UnityEngine;
using UnityEngine.Splines;

public class UnitWalkManager : MonoBehaviour
{
    SplineContainer splineContainer;

    float splinePosition = 0f;        
    UnitConfig config;

    private void Awake()
    {
        config = GetComponent<Unit>().Config;
    }

    public void SetSpline(SplineContainer newSpline)
    {
        splineContainer = newSpline;
    }

    public float WalkAlongSpline()
    {
        if (splineContainer == null)
        {
            Debug.LogWarning($"Spline is not set on {name}");
            return splinePosition;
        }

        MoveUnit();
        RotateUnit();

        return splinePosition;
    }

    void RotateUnit()
    {
        Vector3 tangent = splineContainer.Spline.EvaluateTangent(splinePosition);
        transform.forward = tangent.normalized;
    }

    void MoveUnit()
    {
        splinePosition += (config.walkSpeed * Time.deltaTime) / splineContainer.Spline.GetLength();
        splinePosition = Mathf.Clamp01(splinePosition);
        Vector3 position = splineContainer.Spline.EvaluatePosition(splinePosition);
        transform.position = position + splineContainer.transform.position;
    }
}
