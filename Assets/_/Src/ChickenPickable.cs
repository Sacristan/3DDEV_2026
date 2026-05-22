using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class ChickenPickable : Pickable
{
    private const float MinWaitingTime = 1f;
    private const float MaxWaitingTime = 5f;

    private const float WanderRange = 10f;

    [SerializeField] private float closeEnoughDistance = 0.5f;

    NavMeshAgent _agent;
    Animator _animator;

    private IEnumerator Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(MinWaitingTime, MaxWaitingTime));

            if (GetRandomWanderPoint(transform.position, WanderRange, out Vector3 wanderLoc))
            {
                _agent.SetDestination(wanderLoc);
                UpdateMovement(true);
                yield return new WaitUntil(() => Vector3.Distance(transform.position, wanderLoc) < closeEnoughDistance);
            }

            UpdateMovement(false);
            yield return null;
        }
    }


    bool GetRandomWanderPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }


    void UpdateMovement(bool isMoving)
    {
        _agent.isStopped = !isMoving;
        _animator.SetBool("IsWalking", isMoving);
    }
}