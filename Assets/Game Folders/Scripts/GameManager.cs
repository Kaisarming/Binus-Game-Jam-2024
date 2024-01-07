using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnGameWin;
    
    public static GameManager Instance;

    [SerializeField] private Gamestate currentState;
    [SerializeField] private LevelData activeLevelData;

    [SerializeField] private LevelData[] allLevelData;

    private string path;

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
        //path = Application.persistentDataPath + "/Save";

        if(File.Exists(path))
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

        // event game win!
        if (newState == Gamestate.Result)
        {
            OnGameWin?.Invoke(this, EventArgs.Empty);
        }
        
        OnStateChanged?.Invoke(newState);
    }

    public void SaveGame()
    {
        //string json = JsonUtility.ToJson(allLevelData);
        //File.WriteAllText(path, json);
    }

    public void LoadGame()
    {
        //string json = File.ReadAllText(path);
        //allLevelData = JsonUtility.FromJson<LevelData[]>(json);
    }
}


[System.Serializable]
public class LevelData
{
    public string namaLevel;
    public int levelIndex;
    public bool isOpen;
}