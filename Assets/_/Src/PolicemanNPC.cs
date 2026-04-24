using System;
using UnityEngine;
using UnityEngine.AI;

public class PolicemanNPC : MonoBehaviour
{
    [SerializeField] private float closeEnoughDistance = 0.5f;

    private NavMeshAgent _agent;
    private Player _player;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = FindAnyObjectByType<Player>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, _player.transform.position);

        if (distance > closeEnoughDistance)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_player.transform.position);
        }
        else
        {
            _agent.isStopped = true;
        }
    }
}