using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditPage : Page
{
    [SerializeField] private Button closeButton;

    protected override void Start()
    {
        base.Start();

        closeButton.onClick.AddListener(() => GameManager.Instance.ChangeState(Gamestate.Menu));
    }
}
