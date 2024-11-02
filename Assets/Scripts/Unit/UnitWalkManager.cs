using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;

public class UnitWalkManager : MonoBehaviour
{
    public float SplinePosition {get; private set;}

    SplineContainer splineContainer;
    NavMeshAgent navMeshAgent;
    UnitConfig config;

    public void Initialize(Unit unit, SplineManager newSpline)
    {
        config = unit.Config;
        navMeshAgent = GetComponent<NavMeshAgent>();
        splineContainer = newSpline.Spline;
    }

    public void SetSplinePosition(float splinePosition)
    {
        if (splineContainer == null)
            return;

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
        transform.position = position.Add(y: config.height / 2) + splineContainer.transform.position;
    }

    public bool UnitOnPath()
    {
        return navMeshAgent != null && navMeshAgent.isOnNavMesh && navMeshAgent.isActiveAndEnabled;
    }
    public void MoveToPoint(Vector3 position)
    {
        if (!UnitOnPath())
        {
            Debug.LogWarning("Agent is not on navmesh!");
            return;
        }

        navMeshAgent.SetDestination(position);
        return;
    }
    public void StopNavMeshMovement()
    {
        if (!UnitOnPath())
        {
            Debug.LogWarning("Agent is not on navmesh!");
            return;
        }

        navMeshAgent.ResetPath();
    }

    public bool HasReachedDestination()
    {
        if (!UnitOnPath())
        {
            Debug.LogWarning("Agent is not on navmesh!");
            return false;
        }
        return navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance;
    }

}
