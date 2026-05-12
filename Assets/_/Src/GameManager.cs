using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private float gameWonRestartTimer = 3f;

    private PolicemanNPC _policemanNpc;
    private List<Pickable> uncollectedPickables = new();

    private bool isGameOver = false;

    public int CollectedCollectablesCount => TotalCollectablesCount - uncollectedPickables.Count;
    public int TotalCollectablesCount { get; private set; }
    public bool IsReady { get; private set; }

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        Pickable[] collectables = FindObjectsByType<Pickable>(FindObjectsSortMode.None);
        TotalCollectablesCount = collectables.Length;

        uncollectedPickables = new(collectables);

        _policemanNpc = FindAnyObjectByType<PolicemanNPC>();
        _policemanNpc.OnReachedTarget += PolicemanNpcOnOnReachedTarget;

        IsReady = true;
    }

    public void OnPickableCollected(Pickable pickable)
    {
        Debug.Log($"{nameof(OnPickableCollected)} {pickable.gameObject.name}");
        uncollectedPickables.Remove(pickable);

        if (uncollectedPickables.Count == 0) GameWon();
    }

    void PolicemanNpcOnOnReachedTarget()
    {
        _policemanNpc.OnReachedTarget -= PolicemanNpcOnOnReachedTarget;
        GameLost();
    }

    void GameWon()
    {
        Debug.Log("VICTORY");
        GameOver();
    }

    void GameLost()
    {
        Debug.Log("YOU LOSE!");
        GameOver();
    }

    void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        StartCoroutine(RestartGameRoutine());
    }

    IEnumerator RestartGameRoutine()
    {
        Debug.Log("Trigger Restart Started");
        yield return new WaitForSeconds(gameWonRestartTimer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}