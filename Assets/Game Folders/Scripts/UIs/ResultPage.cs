using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPage : Page
{
    [SerializeField] private TMP_Text labelResult;

    [SerializeField] private Button homeButton;
    [SerializeField] private Button restartButton;

    protected override void Start()
    {
        base.Start();

        homeButton.onClick.AddListener(() => ChangeScene("Main Menu"));
        restartButton.onClick.AddListener(() => ChangeScene("Gameplay"));

        if (GameSetting.Instance.isWin)
        {
            labelResult.text = $"<color=green>YOU WIN !</color>";
        }
        else
        {
            labelResult.text = $"<color=red>YOU LOSE !</color>";
        }
    }
}
