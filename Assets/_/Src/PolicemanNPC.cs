using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PolicemanNPC : MonoBehaviour
{
    public event System.Action OnReachedTarget;

    [SerializeField] private float closeEnoughDistance = 0.5f;

    private NavMeshAgent _agent;
    private Player _player;
    private Animator _animator;

    public enum PolicemanState
    {
        None,
        FollowingTarget,
        ReachedTarget
    }

    private PolicemanState _state = PolicemanState.None;

    public PolicemanState CurrentState
    {
        get => _state;
        set
        {
            if (_state != value)
            {
                _state = value;
                UpdateState();
            }
        }
    }

    IEnumerator Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = FindAnyObjectByType<Player>();
        _animator = GetComponentInChildren<Animator>();

        yield return new WaitForSeconds(3f);
        CurrentState = PolicemanState.FollowingTarget;
    }

    private void Update()
    {
        if (CurrentState == PolicemanState.None) return;

        float distance = Vector3.Distance(transform.position, _player.transform.position);

        if (distance > closeEnoughDistance)
        {
            _agent.SetDestination(_player.transform.position);
            CurrentState = PolicemanState.FollowingTarget;
        }
        else
        {
            CurrentState = PolicemanState.ReachedTarget;
        }
    }

    void UpdateState()
    {
        UpdateMovement(isMoving: CurrentState == PolicemanState.FollowingTarget);

        if (CurrentState == PolicemanState.ReachedTarget)
        {
            OnReachedTarget?.Invoke();
        }
    }

    void UpdateMovement(bool isMoving)
    {
        _agent.isStopped = !isMoving;
        _animator.SetBool("IsWalking", isMoving);
    }
}