using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;

public class UnitWalkManager : MonoBehaviour
{
    public float splinePosition = 0f;           
    
    SplineContainer splineContainer;
    NavMeshAgent navMeshAgent;
    UnitConfig config;

    private void Awake()
    {
        config = GetComponent<Unit>().Config;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void SetSpline(SplineManager newSpline)
    {
        splineContainer = newSpline.Spline;
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
        transform.position = position.Add(y: config.height / 2) + splineContainer.transform.position;
    }

    public void MoveToPoint(Waypoint waypoint)
    {
        if (navMeshAgent == null || !navMeshAgent.isOnNavMesh)
        {
            Debug.LogWarning("Agent is not on navmesh!");
            return;
        }

        navMeshAgent.SetDestination(waypoint.position);
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

        return navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance;
    }

}
