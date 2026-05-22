using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI collectedText;
    [SerializeField] private GameObject gameWonContainer;
    [SerializeField] private GameObject gameLostContainer;
    [SerializeField] private Image damageFXImage;
    [SerializeField] private float damageAppearTime = 3f;

    private Player _player;
    bool showPlayerDamageFX = false;

    private IEnumerator Start()
    {
        _player = FindAnyObjectByType<Player>();

        _player.OnPolicemanNearby += OnPolicemanNearbyPlayerCallback;
        _player.OnPolicemanWentAway += OnPolicemanWentAwayCallback;

        Pickable.OnPicked += OnPickablePickedCallback;
        GameManager.instance.OnGameOver += OnGameOverCallback;

        yield return new WaitUntil(() => GameManager.instance.IsReady);
        UpdateUI();
    }

    void Update()
    {
        float targetAlpha = showPlayerDamageFX ? 1f : 0f;

        Color dmgColor = damageFXImage.color;
        dmgColor.a = Mathf.MoveTowards(dmgColor.a, targetAlpha, Time.deltaTime * damageAppearTime);

        damageFXImage.color = dmgColor;
    }

    private void OnDestroy()
    {
        Pickable.OnPicked -= OnPickablePickedCallback;
    }

    void OnPolicemanNearbyPlayerCallback()
    {
        showPlayerDamageFX = true;
    }

    void OnPolicemanWentAwayCallback()
    {
        showPlayerDamageFX = false;
    }

    private void OnPickablePickedCallback(Pickable obj)
    {
        UpdateUI();
    }

    private void OnGameOverCallback(bool isVictory)
    {
        if (isVictory) gameWonContainer.SetActive(true);
        else gameLostContainer.SetActive(true);
    }

    void UpdateUI()
    {
        var collectedCollectables = GameManager.instance.CollectedCollectablesCount;
        var totalCollectables = GameManager.instance.TotalCollectablesCount;

        collectedText.text = $"Collected {collectedCollectables}/{totalCollectables}";
    }
}