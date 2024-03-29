using System;
using TMPro;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class TimerController : MonoBehaviour
{
    public event EventHandler OnTickChanged; // event detik berubah
    public event EventHandler OnTimesUp; // event waktu habis

    [SerializeField] private int initialTime;
    [SerializeField] private int initialCounterStart;
    [SerializeField] private float timerSpeed = 1; // 1 = normal
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject pfCounterStart;
    private GameObject _counterStart;
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
        // find player
        if (GameObject.FindWithTag("Player") == null)
        {
            Destroy(gameObject);
            
            return;
        }

        var player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.IsDead = true;

        // update text
        SetTextTimer(initialTime.ToString());

        // start timer
        StartTimer();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameWin += EventStopTimer;
            GameManager.Instance.OnGameLose += EventStopTimer;
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameWin -= EventStopTimer;
            GameManager.Instance.OnGameLose -= EventStopTimer;
        }
    }

    private void EventStopTimer(object sender, EventArgs e)
    {
        if (!_isTimeRunning) return;
        
        StopTimer();
    }

    /// <summary>
    /// start timer directly
    /// </summary>
    public void StartTimer()
    {
        _counterStart = Instantiate(pfCounterStart, transform);
        var controller321 = _counterStart.GetComponent<Controller321>();
        controller321.Setup(initialCounterStart);
        controller321.OnCounterDone += ActivateTimer;

        // set text timer
        timeText.text = initialTime.ToString();

        // declaration variable _currentTime
        _currentTime = initialTime;
        _stepCount = _currentTime;
    }

    private void ActivateTimer(object sender, EventArgs e)
    {
        // find player
        var player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.IsDead = false;

        ResumeTimer();

        Destroy(_counterStart);
    }

    /// <summary>
    /// start timer with specific time
    /// </summary>
    /// <param name="initialTime">initial timer</param>
    public void StartTimer(int newInitialTime)
    {
        this.initialTime = newInitialTime;

        StartTimer();
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