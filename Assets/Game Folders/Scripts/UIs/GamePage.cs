using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePage : Page
{
    [SerializeField] private TMP_Text labelTime;
    [SerializeField] private TMP_Text labelItem;

    private void OnEnable()
    {
        GameSetting.Instance.OnItemCollected += Instance_OnItemCollected;
        TimerController.Instance.OnTickChanged += Instance_OnTickChanged;
    }

    private void OnDisable()
    {
        GameSetting.Instance.OnItemCollected -= Instance_OnItemCollected;
        TimerController.Instance.OnTickChanged -= Instance_OnTickChanged;
    }

    private void Instance_OnItemCollected(int totalItem)
    {
        labelItem.text = $"Heart : <color=red>{totalItem}</color>";
    }

    private void Instance_OnTickChanged(object sender, System.EventArgs e)
    {
        labelTime.text = $"Timer : <color=green>{sender}</color>";
    }
}
