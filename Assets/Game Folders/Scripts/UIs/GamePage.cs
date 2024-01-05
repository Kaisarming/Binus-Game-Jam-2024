using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePage : Page
{
    [SerializeField] private TMP_Text labelTimer;
    [SerializeField] private TMP_Text labelHeart;
    [SerializeField] private TMP_Text labelCoin;

    private void OnEnable()
    {
        if(GameSetting.Instance == null)
        {
            return;
        }
        GameSetting.Instance.OnItemCollected += Instance_OnItemCollected;
    }

    private void OnDisable()
    {
        GameSetting.Instance.OnItemCollected -= Instance_OnItemCollected;
    }

    private void Instance_OnItemCollected(ItemType tipe, int jumlah)
    {
        switch (tipe)
        {
            case ItemType.Coin:
                labelCoin.text = $"{jumlah}";
                break;
            case ItemType.Health:
                labelHeart.text = $"{jumlah}";
                break;
        }
    }
}