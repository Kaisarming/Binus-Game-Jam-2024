using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    public static GameSetting Instance;

    [SerializeField] private int tempItem;

    public delegate void ItemCollectDelegate(int totalItem);
    public event ItemCollectDelegate OnItemCollected;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void CollectItem()
    {
        tempItem++;
        OnItemCollected?.Invoke(tempItem);
    }
}
