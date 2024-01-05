using System;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public event EventHandler OnTickChanged; // event detik berubah
    public event EventHandler OnTimesUp; // event waktu habis

    [SerializeField] private int initialTime;
    [SerializeField] private float timerSpeed = 1; // 1 = normal
    [SerializeField] private TextMeshProUGUI timeText;

    private float _currentTime;
    private bool _isTimeRunning;

    private float _stepCount;
    private const int StepTime = 1; // every a second trigger
    
    /// <summary>
    /// Set Initial Time to Integer Value
    /// </summary>
    public int InitialTime
    {
        // time should be equals or more than 0
        set => initialTime = value < 0 ? 0 : value;
    }

    /// <summary>
    /// Get current time (real time)
    /// </summary>
    public float CurrentTime => _currentTime;

    /// <summary>
    /// Add timer number
    /// </summary>
    /// <param name="addNumber">float value</param>
    public void AddTimer(float addNumber)
    {
        _currentTime += addNumber;
    }

    /// <summary>
    /// Set Timer Speed
    /// </summary>
    /// <param name="newSpeed">float value</param>
    public void SetTimerSpeed(float newSpeed)
    {
        timerSpeed = newSpeed;
    }

    // get timer speed
    public float GetTimerSpeed()
    {
        return timerSpeed;
    }

    private void Start()
    {
        // update text
        SetTextTimer(initialTime.ToString());
        
        // start timer
        StartTimer();
    }

    /// <summary>
    /// start timer directly
    /// </summary>
    public void StartTimer()
    {
        _isTimeRunning = true;
        
        // set text timer
        timeText.text = initialTime.ToString();
        
        // declaration variable _currentTime
        _currentTime = initialTime;
        _stepCount = _currentTime;
    }

    /// <summary>
    /// start timer with specific time
    /// </summary>
    /// <param name="initialTime">initial timer</param>
    public void StartTimer(int newInitialTime)
    {
        this.initialTime = newInitialTime;
        
        // set text timer
        timeText.text = initialTime.ToString();
        
        // declaration variable _currentTime
        _currentTime = initialTime;
        _stepCount = _currentTime;

        _isTimeRunning = true;
    }

    public void SetTextTimer(string text)
    {
        timeText.text = text;
    }
    
    public void ResumeTimer()
    {
        _isTimeRunning = true;
    }
    
    public void StopTimer()
    {
        _isTimeRunning = false;
    }
    
    private void Update()
    {
        // operate only if the time is still running
        if (!_isTimeRunning)
        {
            return;
        }

        if (_currentTime > 0)
        {
            var currentTimeInt = Mathf.CeilToInt(_currentTime);
            var stepCountInt = Mathf.CeilToInt(_stepCount);

            timeText.text = _currentTime.ToString("F1");

            // tick trigger / every step time trigger
            if (stepCountInt - currentTimeInt == StepTime)
            {
                // event trigger to on tick changed
                OnTickChanged?.Invoke(this, EventArgs.Empty);

                _stepCount = currentTimeInt;

                // Debug.Log("tick!");
            }

            // subtract current timer - realtime
            _currentTime -= Time.deltaTime * timerSpeed;
        }
        else
        {
            // times up
            _isTimeRunning = false;

            _currentTime = 0;

            timeText.text = _currentTime.ToString();

            // event trigger to on times up!
            OnTimesUp?.Invoke(this, EventArgs.Empty);

            Debug.Log("Times up!");
        }
    }
}