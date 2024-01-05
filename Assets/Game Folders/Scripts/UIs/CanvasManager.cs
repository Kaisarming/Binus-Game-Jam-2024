using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] protected Page[] allPages;

    private void Start()
    {
        allPages = GetComponentsInChildren<Page>(true);
    
        GameManager.Instance.OnStateChanged += Instance_OnStateChanged;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnStateChanged -= Instance_OnStateChanged;
    }

    private void Instance_OnStateChanged(Gamestate newState)
    {
        switch (newState)
        {
            case Gamestate.Menu:
                SetPage(PageName.Menu);
                break;
            case Gamestate.Game:
                break;
            case Gamestate.Pause:
                break;
            case Gamestate.GameOver:
                break;
            case Gamestate.Credit:
                SetPage(PageName.Credit);
                break;
        }
    }

    private void SetPage(PageName nama)
    {
        foreach (var item in allPages)
        {
            item.gameObject.SetActive(false);
        }

        Page findPage = Array.Find(allPages, p => p.namaPage == nama);
        if(findPage != null)
        {
            findPage.gameObject.SetActive(true);
        }
    }
}
