using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Gamestate currentState;
    [SerializeField] private LevelData activeLevelData;

    [SerializeField] private LevelData[] allLevelData;

    public delegate void ChangeStateDelegate(Gamestate newState);
    public event ChangeStateDelegate OnStateChanged;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        if(PlayerPrefs.HasKey(allLevelData[1].namaLevel))
        {
            LoadGame();
        }
        else
        {
            SaveGame();
        }
    }

    public LevelData[] GetLevelData()
    {
        return allLevelData;
    }

    public LevelData GetActiveLevelData()
    {
        return activeLevelData;
    }

    public void SetActiveLevel(int n)
    {
        activeLevelData = allLevelData[n];
    }

    public void UnlockNewLevel(int n)
    {
        allLevelData[n].isOpen = true;
        SaveGame();
    }

    public void ChangeState(Gamestate newState)
    {
        if(newState == currentState)
        {
            return;
        }

        currentState = newState;
        OnStateChanged?.Invoke(newState);
    }

    public void SaveGame()
    {
        for (int i = 0; i < allLevelData.Length; i++)
        {
            if(allLevelData[i].isOpen)
            {
                PlayerPrefs.SetInt(allLevelData[i].namaLevel, 1);
            }
        }
    }

    public void LoadGame()
    {
        for (int i = 0; i < allLevelData.Length; i++)
        {
            allLevelData[i].isOpen = PlayerPrefs.HasKey(allLevelData[i].namaLevel);
        }
    }
}


[System.Serializable]
public class LevelData
{
    public string namaLevel;
    public int levelIndex;
    public bool isOpen;
}