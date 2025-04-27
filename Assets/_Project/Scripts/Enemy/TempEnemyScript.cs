using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TempEnemyScript : MonoBehaviour
{
    public Transform player;
    public float followRange = 15f;
    public float wanderRadius = 30f;
    public float destinationThreshold = 1f;

    private NavMeshAgent agent;
    private Vector3 currentDestination;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        PickNewWanderDestination();
    }

    void Update()
    {
        if (player == null) return;

        PlayerController playerScript = player.GetComponent<PlayerController>();
        if (playerScript == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!playerScript.isHiding && distanceToPlayer <= followRange)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            if (Vector3.Distance(transform.position, currentDestination) <= destinationThreshold)
            {
                PickNewWanderDestination();
            }
            agent.SetDestination(currentDestination);
        }
    }

    void PickNewWanderDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection.y = 0; // Keep on same vertical plane
        Vector3 wanderTarget = transform.position + randomDirection;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(wanderTarget, out hit, wanderRadius, NavMesh.AllAreas))
        {
            currentDestination = hit.position;
        }
        else
        {
            currentDestination = transform.position; // fallback
        }
    }
}
