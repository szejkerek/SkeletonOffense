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
