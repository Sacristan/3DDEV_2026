using System;
using System.Collections;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public System.Action OnCapturedByPoliceman;

    public System.Action OnPolicemanNearby;
    public System.Action OnPolicemanWentAway;

    [SerializeField] private float gameLostTimeWhenNearPoliceman = 5;

    private FirstPersonController _playerController;
    private PolicemanNPC _policemanNpc;
    Coroutine policemanNearbyRoutine;

    private void Start()
    {
        _playerController = GetComponent<FirstPersonController>();

        _policemanNpc = FindAnyObjectByType<PolicemanNPC>();
        _policemanNpc.OnReachedTarget += PolicemanNpcOnReachedTargetCallback;
        _policemanNpc.OnStartedFollowing += PolicemanNpcOnStartedFollowingCallback;
        
        GameManager.instance.OnGameOver += OnGameOverCallback;
    }
    
    void OnGameOverCallback(bool isVictory)
    {
        _playerController.enabled = false;
    }

    void PolicemanNpcOnReachedTargetCallback()
    {
        Debug.Log(nameof(PolicemanNpcOnReachedTargetCallback));
        StopPlayerPolicemanNearbyDamageRoutine();
        OnPolicemanNearby?.Invoke();
        policemanNearbyRoutine = StartCoroutine(PlayerPolicemanNearbyDamageRoutine());
    }

    void PolicemanNpcOnStartedFollowingCallback()
    {
        Debug.Log(nameof(PolicemanNpcOnStartedFollowingCallback));
        OnPolicemanWentAway?.Invoke();
        StopPlayerPolicemanNearbyDamageRoutine();
    }

    void StopPlayerPolicemanNearbyDamageRoutine()
    {
        if (policemanNearbyRoutine != null)
        {
            StopCoroutine(policemanNearbyRoutine);
            policemanNearbyRoutine = null;
        }
    }

    IEnumerator PlayerPolicemanNearbyDamageRoutine()
    {
        yield return new WaitForSeconds(gameLostTimeWhenNearPoliceman);
        OnCapturedByPoliceman?.Invoke();
        policemanNearbyRoutine = null;
    }
}