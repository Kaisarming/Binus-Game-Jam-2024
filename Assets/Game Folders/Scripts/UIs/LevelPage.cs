using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelPage : Page
{
    [SerializeField] private Button levelButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button homeButton;

    [SerializeField] private TMP_Text labelLevelName;
    [SerializeField] private GameObject lockImage;

    private int selectedLevel = 0;

    protected override void Start()
    {
        base.Start();

        homeButton.onClick.AddListener(() => GameManager.Instance.ChangeState(Gamestate.Menu));

        levelButton.onClick.AddListener(() =>
        {
            GameManager.Instance.SetActiveLevel(GameManager.Instance.GetLevelData()[selectedLevel].levelIndex);
            GameManager.Instance.ChangeState(Gamestate.Tutorial);
        });

        leftButton.onClick.AddListener(PrevisiousLevel);
        rightButton.onClick.AddListener(NextLevel);

        SetupButton();
    }

    private void PrevisiousLevel()
    {
        if(selectedLevel <= 0)
        {
            return;
        }
        selectedLevel--;
        SetupButton();
    }

    private void NextLevel()
    {
        if(selectedLevel < GameManager.Instance.GetLevelData().Length - 1)
        {
            selectedLevel++;
            SetupButton();
        }
    }

    private void SetupButton()
    {
        levelButton.interactable = GameManager.Instance.GetLevelData()[selectedLevel].isOpen;
        labelLevelName.text = GameManager.Instance.GetLevelData()[selectedLevel].namaLevel;
        lockImage.SetActive(!GameManager.Instance.GetLevelData()[selectedLevel].isOpen);

        leftButton.interactable = selectedLevel != 0;
        rightButton.interactable = selectedLevel != GameManager.Instance.GetLevelData().Length - 1;
    }
}
