using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private List<Pickable> uncollectedPickables = new();

    [SerializeField] private float gameWonRestartTimer = 3f;

    private bool isGameWon = false;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        Pickable[] collectables = FindObjectsByType<Pickable>(FindObjectsSortMode.None);
        uncollectedPickables = new(collectables);
    }

    public void OnPickableCollected(Pickable pickable)
    {
        Debug.Log($"{nameof(OnPickableCollected)} {pickable.gameObject.name}");
        uncollectedPickables.Remove(pickable);

        if (uncollectedPickables.Count == 0) GameWon();
    }

    void GameWon()
    {
        if (isGameWon) return;

        isGameWon = true;
        Debug.Log("VICTORY");
        StartCoroutine(GameWonRoutine());
    }

    IEnumerator GameWonRoutine()
    {
        Debug.Log("Trigger Restart Started");
        yield return new WaitForSeconds(gameWonRestartTimer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}