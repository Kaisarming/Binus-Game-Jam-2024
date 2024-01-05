using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditCard : MonoBehaviour
{
    [SerializeField] private Button discordButton;
    [SerializeField, TextArea(3, 3)] private string uri;

    private void Start()
    {
        discordButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.MainkanSuara("Click");
            Application.OpenURL(uri);
        });
    }
}