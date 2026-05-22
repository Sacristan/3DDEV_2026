using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public event Action<bool> OnGameOver;

    [SerializeField] private float gameWonRestartTimer = 3f;

    private Player _player;
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

        _player = FindAnyObjectByType<Player>();
        _player.OnCapturedByPoliceman += OnPlayerGotCapturedByPolicemanCallback;

        IsReady = true;
    }

    public void OnPickableCollected(Pickable pickable)
    {
        Debug.Log($"{nameof(OnPickableCollected)} {pickable.gameObject.name}");
        uncollectedPickables.Remove(pickable);

        if (uncollectedPickables.Count == 0) GameWon();
    }

    void OnPlayerGotCapturedByPolicemanCallback()
    {
        _player.OnCapturedByPoliceman -= OnPlayerGotCapturedByPolicemanCallback;
        GameLost();
    }

    void GameWon()
    {
        Debug.Log("VICTORY");
        GameOver(isVictory: true);
    }

    void GameLost()
    {
        Debug.Log("YOU LOSE!");
        GameOver(isVictory: false);
    }

    void GameOver(bool isVictory)
    {
        if (isGameOver) return;
        isGameOver = true;
        OnGameOver?.Invoke(isVictory);
        StartCoroutine(RestartGameRoutine());
    }

    IEnumerator RestartGameRoutine()
    {
        Debug.Log("Trigger Restart Started");
        yield return new WaitForSeconds(gameWonRestartTimer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}