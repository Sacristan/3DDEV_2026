using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public System.Action OnCapturedByPoliceman;

    public System.Action OnPolicemanNearby;
    public System.Action OnPolicemanWentAway;

    private PolicemanNPC _policemanNpc;
    [SerializeField] private float gameLostTimeWhenNearPoliceman = 5;

    Coroutine policemanNearbyRoutine;

    private void Start()
    {
        _policemanNpc = FindAnyObjectByType<PolicemanNPC>();
        _policemanNpc.OnReachedTarget += PolicemanNpcOnReachedTargetCallback;
        _policemanNpc.OnStartedFollowing += PolicemanNpcOnStartedFollowingCallback;
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