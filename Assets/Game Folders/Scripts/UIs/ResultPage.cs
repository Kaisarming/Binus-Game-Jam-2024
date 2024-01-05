using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPage : Page
{
    [SerializeField] private Button homeButton;
    [SerializeField] private Button restartButton;

    protected override void Start()
    {
        base.Start();

        homeButton.onClick.AddListener(() => ChangeScene("Main Menu"));
        restartButton.onClick.AddListener(() => ChangeScene("Gameplay"));
    }
}
