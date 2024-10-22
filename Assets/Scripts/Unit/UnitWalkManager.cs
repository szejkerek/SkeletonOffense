using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;

public class UnitWalkManager : MonoBehaviour
{
    SplineContainer splineContainer;
    NavMeshAgent navMeshAgent;
    UnitConfig config;

    float stuckCheckTimer = 0f;
    float splinePosition = 0f;        

    private void Awake()
    {
        config = GetComponent<Unit>().Config;
        navMeshAgent = GetComponent<NavMeshAgent>();
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

    public Vector3 GetOffsetSplinePositionByLength(float offsetPercent = 0.05f)
    {
        float splineLength = splineContainer.Spline.GetLength();
        float offsetDistance = splineLength * offsetPercent;
        float currentDistance = splineLength * splinePosition;

        float newSplinePosition = Mathf.Clamp01(Mathf.Clamp(currentDistance + offsetDistance, 0f, splineLength) / splineLength);
        splinePosition = newSplinePosition; //It might break game

        return splineContainer.EvaluatePosition(newSplinePosition);
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
        transform.position = position.Add(y: config.height / 2) + splineContainer.transform.position;
    }

    public void MoveToPoint(Vector3 targetPosition)
    {
        if (navMeshAgent == null || !navMeshAgent.isOnNavMesh)
        {
            Debug.LogWarning("Agent is not on navmesh!");
            return;
        }

        navMeshAgent.SetDestination(targetPosition);
        return;
    }
    public void StopNavMeshMovement()
    {
        if (navMeshAgent != null && navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.ResetPath();
        }
    }

    public bool HasReachedDestination()
    {
        if (navMeshAgent == null || !navMeshAgent.isOnNavMesh || navMeshAgent.pathPending)
            return false;

        if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
        {
            return navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance;
        }

        return false;
    }


    public bool IsStuck()
    {
        if (navMeshAgent == null || !navMeshAgent.hasPath)
            return false;

        if (navMeshAgent.velocity.sqrMagnitude < config.minStuckSpeed * config.minStuckSpeed)
        {
            stuckCheckTimer += Time.deltaTime;
        }
        else
        {
            stuckCheckTimer = 0f;
        }

        return stuckCheckTimer >= config.stuckThreshold;
    }
}
