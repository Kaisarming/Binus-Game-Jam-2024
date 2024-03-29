using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnGameWin;
    public event EventHandler OnGameLose;

    public static GameManager Instance;

    [SerializeField] private Gamestate currentState;
    [SerializeField] private LevelData activeLevelData;

    [SerializeField] private LevelData[] allLevelData;

    private string path;

    public delegate void ChangeStateDelegate(Gamestate newState);

    public event ChangeStateDelegate OnStateChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        path = Application.persistentDataPath + "/Save.dat";

        if (File.Exists(path))
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
        if (newState == currentState)
        {
            return;
        }

        currentState = newState;

        // event game win!
        if (newState == Gamestate.Result)
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.MainkanSuara("Win");
            }
            
            OnGameWin?.Invoke(this, EventArgs.Empty);
        }
        
        // event game lose
        if (newState == Gamestate.GameOver)
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.MainkanSuara("Lose");
            }
            
            OnGameLose?.Invoke(this, EventArgs.Empty);
        }

        OnStateChanged?.Invoke(newState);
    }

    public void SaveGame()
    {
        var json = JsonConvert.SerializeObject(allLevelData, formatting: Formatting.Indented);
        File.WriteAllText(path, json);
    }

    public void LoadGame()
    {
        string json = File.ReadAllText(path);
        allLevelData = JsonConvert.DeserializeObject<LevelData[]>(json);
    }
}


[System.Serializable]
public class LevelData
{
    public string namaLevel;
    public int levelIndex;
    public bool isOpen;
}
