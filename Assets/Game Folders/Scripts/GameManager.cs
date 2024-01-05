using System;
using System.Collections;
using System.Collections.Generic;
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

    public void ChangeState(Gamestate newState)
    {
        if(newState == currentState)
        {
            return;
        }

        currentState = newState;
        OnStateChanged?.Invoke(newState);
    }
}


[System.Serializable]
public class LevelData
{
    public string namaLevel;
    public int levelIndex;
    public bool isOpen;
}