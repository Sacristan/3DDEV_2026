using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI collectedText;

    private IEnumerator Start()
    {
        Pickable.OnPicked += PickableOnOnPicked;
        yield return new WaitUntil(() => GameManager.instance.IsReady);
        UpdateUI();
    }
    
    private void OnDestroy()
    {
        Pickable.OnPicked -= PickableOnOnPicked;
    }

    private void PickableOnOnPicked(Pickable obj)
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        var collectedCollectables = GameManager.instance.CollectedCollectablesCount;
        var totalCollectables = GameManager.instance.TotalCollectablesCount;

        collectedText.text = $"Collected {collectedCollectables}/{totalCollectables}";
    }
}