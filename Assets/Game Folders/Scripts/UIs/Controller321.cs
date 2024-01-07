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
    private int _once = 0;
    
    public void Setup(int initialCounter)
    {
        _once = 0;
        
        startCounter = initialCounter;

        _currentTime = startCounter;

        GetComponent<Animator>().SetTrigger(Animator.StringToHash("counterStart"));

        nText.text = _currentTime.ToString();
    }

    private void ReceiverFromAnimation()
    {
        // once execute
        if (_once < 1)
        {
            // play sound
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.MainkanSuara("321");
                _once++;
            }
        }

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