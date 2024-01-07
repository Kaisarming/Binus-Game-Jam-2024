using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-5)]
public class GameSetting : MonoBehaviour
{
    public static GameSetting Instance;

    private int coinTemp = 0;
    private int healthTemp = 0;

    public bool isWin;

    [SerializeField] private GameObject[] allLevels;

    private GameObject _currentActiveLevel;
    
    public GameObject CurrentActiveLevel
    {
        get { return _currentActiveLevel; }
    }

    public delegate void ItemCollectDelegate(ItemType tipe, int jumlah);

    public event ItemCollectDelegate OnItemCollected;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        SetupLevel();
    }

    private void SetupLevel()
    {
        if (GameManager.Instance == null)
        {
            return;
        }

        _currentActiveLevel = Instantiate(allLevels[GameManager.Instance.GetActiveLevelData().levelIndex],
            transform.position, Quaternion.identity);
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