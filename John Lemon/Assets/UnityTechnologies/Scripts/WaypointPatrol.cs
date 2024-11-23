using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    private int m_CurrentWaypintIndex;

    private void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    private void Update()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypintIndex = (m_CurrentWaypintIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypintIndex].position);
        }
    }
}