using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPage : Page
{
    [SerializeField] private TMP_Text labelResult;
    [SerializeField] private GameObject frameLabelCoin;
    [SerializeField] private TMP_Text labelCoin;
    [SerializeField] private TMP_Text labelCoinOnGamePlay;
    [SerializeField] private TMP_Text labelTime;

    [SerializeField] private Button homeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button nextButton;

    protected override void Start()
    {
        base.Start();

        homeButton.onClick.AddListener(() => ChangeScene("Main Menu"));
        restartButton.onClick.AddListener(() => ChangeScene("Gameplay"));
        nextButton.onClick.AddListener(() =>
        {
            GameManager.Instance.UnlockNewLevel(GameManager.Instance.GetActiveLevelData().levelIndex + 1);
            GameManager.Instance.SetActiveLevel(GameManager.Instance.GetActiveLevelData().levelIndex + 1);

            ChangeScene("Gameplay");
        });

        nextButton.gameObject.SetActive(GameSetting.Instance.isWin);

        if (GameSetting.Instance.isWin)
        {
            labelResult.text = $"<color=green>YOU WIN !</color>";
            frameLabelCoin.SetActive(true);
            labelCoin.text = labelCoinOnGamePlay.text;
        }
        else
        {
            labelResult.text = $"<color=red>YOU LOSE !</color>";
            frameLabelCoin.SetActive(false);
        }
    }
}
