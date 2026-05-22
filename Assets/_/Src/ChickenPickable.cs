using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ChickenPickable : Pickable
{
    [SerializeField] private float closeEnoughDistance = 0.5f;
    [SerializeField] private float wanderRange = 10f;
    [SerializeField] private float minWaitingTime = 1f;
    [SerializeField] private float maxWaitingTime = 5f;

    NavMeshAgent _agent;
    Animator _animator;

    private IEnumerator Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitingTime, maxWaitingTime));

            if (GetRandomWanderPoint(transform.position, wanderRange, out Vector3 wanderLoc))
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
            
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
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