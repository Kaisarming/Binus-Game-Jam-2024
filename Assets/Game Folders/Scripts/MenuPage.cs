using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPage : Page
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;

    protected override void Start()
    {
        base.Start();

        playButton.onClick.AddListener(() => ChangeScene("Gameplay"));
        exitButton.onClick.AddListener(() => Application.Quit());
    }
}
