using System;
using UnityEngine;
using UnityEngine.AI;

public class PolicemanNPC : MonoBehaviour
{
    public event System.Action OnReachedTarget; 
    
    [SerializeField] private float closeEnoughDistance = 0.5f;

    private NavMeshAgent _agent;
    private Player _player;
    private Animator _animator;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = FindAnyObjectByType<Player>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, _player.transform.position);

        if (distance > closeEnoughDistance)
        {
            _agent.SetDestination(_player.transform.position);
            UpdateMovement(isMoving: true);
        }
        else
        {
            ReachedTarget();
        }
    }

    void ReachedTarget()
    {
        UpdateMovement(isMoving: false);
        OnReachedTarget?.Invoke();
    }

    void UpdateMovement(bool isMoving)
    {
        _agent.isStopped = !isMoving;
        _animator.SetBool("IsWalking", isMoving);
    }
}