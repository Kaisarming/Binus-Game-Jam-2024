using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPage : Page
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button creditButton;
    [SerializeField] private Button exitButton;

    protected override void Start()
    {
        base.Start();

        playButton.onClick.AddListener(() => GameManager.Instance.ChangeState(Gamestate.Level));
        creditButton.onClick.AddListener(() => GameManager.Instance.ChangeState(Gamestate.Credit));
        exitButton.onClick.AddListener(() => Application.Quit());
    }
}
