using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Gamestate currentState;

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