using System;
using TMPro;
using UnityEngine;

public class Controller321 : MonoBehaviour
{
    public event EventHandler OnCounterDone;

    [SerializeField] private int startCounter;
    [SerializeField] private GameObject background;
    [SerializeField] private TextMeshProUGUI nText;

    private int _currentTime;

    public void Setup(int initialCounter)
    {
        startCounter = initialCounter;

        _currentTime = startCounter;

        GetComponent<Animator>().SetTrigger(Animator.StringToHash("counterStart"));

        nText.text = _currentTime.ToString();
    }

    private void ReceiverFromAnimation()
    {
        nText.text = _currentTime.ToString();

        switch (_currentTime)
        {
            case 0:
                nText.text = "Go!";
                break;
            case < 0:
                OnCounterDone?.Invoke(this, EventArgs.Empty);

                Destroy(GetComponent<Animator>());
            
                return;
        }

        _currentTime--;

        if (!nText.gameObject.activeSelf) nText.gameObject.SetActive(true);
        if (!background.activeSelf) background.gameObject.SetActive(true);
    }

    private void EmptyFunction()
    {
    }
}