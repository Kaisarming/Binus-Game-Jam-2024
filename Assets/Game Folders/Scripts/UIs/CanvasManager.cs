using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// after player script execution
[DefaultExecutionOrder(1)]
public class CanvasManager : MonoBehaviour
{
    [SerializeField] protected Page[] allPages;
    [SerializeField] private Image fading;
   
    private GameObject _player;

    private void Awake()
    {
        // add listener heart from player
        _player = GameObject.FindWithTag("Player");
        if (_player != null)
        {
            var player = _player.GetComponent<Player>();
            player.HealthSystemPlayer.OnHealthChanged += HealthChanged;
        }
    }

    private void StateChangedEvent(Gamestate newstate)
    {
        Instance_OnStateChanged(newstate);
    }

    private void Start()
    {
        allPages = GetComponentsInChildren<Page>(true);

        GameManager.Instance.OnStateChanged += Instance_OnStateChanged;
        fading.color = new Color(0, 0, 0, 1f);
        fading.CrossFadeAlpha(0f, 1f, false);

        if (_player != null)
        {
            var player = _player.GetComponent<Player>();
            HealthChanged(sender: null, e: null);
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.OnStateChanged -= Instance_OnStateChanged;

        if (_player != null)
        {
            var player = _player.GetComponent<Player>();
            player.HealthSystemPlayer.OnHealthChanged -= HealthChanged;
        }
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
                GameSetting.Instance.isWin = false;
                SetPage(PageName.Result);
                break;
            case Gamestate.Credit:
                SetPage(PageName.Credit);
                break;
            case Gamestate.Level:
                SetPage(PageName.Level);
                break;
            case Gamestate.Result:
                GameSetting.Instance.isWin = true;
                SetPage(PageName.Result);
                break;
            case Gamestate.Tutorial:
                SetPage(PageName.tutorial);
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
        if (findPage != null)
        {
            findPage.gameObject.SetActive(true);
        }
    }

    private void HealthChanged(object sender, EventArgs e)
    {
        var player = _player.GetComponent<Player>();
        var gamePage = GetComponentInChildren<GamePage>();
        gamePage.SetLabelHeart(player.HealthSystemPlayer.GetHealth().ToString());
    }
}