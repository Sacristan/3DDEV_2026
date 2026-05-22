using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI collectedText;
    [SerializeField] private GameObject gameWonContainer;
    [SerializeField] private GameObject gameLostContainer;

    private IEnumerator Start()
    {
        Pickable.OnPicked += OnPickablePickedCallback;
        GameManager.instance.OnGameOver += OnGameOverCallback;

        yield return new WaitUntil(() => GameManager.instance.IsReady);
        UpdateUI();
    }

    private void OnDestroy()
    {
        Pickable.OnPicked -= OnPickablePickedCallback;
    }

    private void OnPickablePickedCallback(Pickable obj)
    {
        UpdateUI();
    }

    private void OnGameOverCallback(bool isVictory)
    {
        if (isVictory) gameWonContainer.SetActive(true);
        else gameLostContainer.SetActive(false);
    }

    void UpdateUI()
    {
        var collectedCollectables = GameManager.instance.CollectedCollectablesCount;
        var totalCollectables = GameManager.instance.TotalCollectablesCount;

        collectedText.text = $"Collected {collectedCollectables}/{totalCollectables}";
    }
}