using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPage : Page
{
    [SerializeField] private Image bar;
    [SerializeField] private float modifier;

    private float loadingTime = 0;

    protected override void Start()
    {
        base.Start();
        loadingTime = 0;
    }

    private void Update()
    {
        loadingTime += Time.deltaTime / modifier;

        bar.fillAmount = loadingTime;
        if(loadingTime >= 1f)
        {
            ChangeScene("Gameplay");
        }
    }
}