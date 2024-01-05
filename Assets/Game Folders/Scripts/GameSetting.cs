using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    public static GameSetting Instance;

    private int coinTemp = 0;
    private int healthTemp = 0;

    [SerializeField] private GameObject[] allLevels;

    public delegate void ItemCollectDelegate(ItemType tipe , int jumlah);
    public event ItemCollectDelegate OnItemCollected;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        SetupLevel();
    }

    private void SetupLevel()
    {
        Instantiate(allLevels[GameManager.Instance.GetActiveLevelData().levelIndex], transform.position, Quaternion.identity);
    }

    public void CollectItem(ItemType tipe)
    {
        switch (tipe)
        {
            case ItemType.Coin:
                coinTemp++;
                OnItemCollected?.Invoke(ItemType.Coin, coinTemp);
                break;
            case ItemType.Health:
                healthTemp++;
                OnItemCollected?.Invoke(ItemType.Health, healthTemp);
                break;
        }
    }
}

public enum ItemType
{
    Coin,
    Health
}
