using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GamePage : Page
{
    [SerializeField] private TMP_Text labelNameLevel;
    [SerializeField] private TMP_Text labelHeart;
    [SerializeField] private TMP_Text labelCoin;

    protected override void Start()
    {
        base.Start();    
        GameSetting.Instance.OnItemCollected += Instance_OnItemCollected;
        SetLabelLevel(GameManager.Instance.GetActiveLevelData().namaLevel);
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

    public void SetLabelHeart(string text)
    {
        labelHeart.text = text;
    }
    
    public void SetLabelLevel(string text)
    {
        labelNameLevel.text = text;
    }
}