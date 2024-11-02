using UnityEngine;
using UnityEngine.AI;

public class UnitNavMeshWalker : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    UnitConfig config;
    public void Initialize(UnitConfig config)
    {
        this.config = config;   
        navMeshAgent = GetComponent<NavMeshAgent>();
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