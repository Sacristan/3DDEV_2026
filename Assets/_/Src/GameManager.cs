using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private List<Pickable> uncollectedPickables = new();

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

        if (uncollectedPickables.Count == 0)
        {
            Debug.Log("VICTORY");
        }
    }
}