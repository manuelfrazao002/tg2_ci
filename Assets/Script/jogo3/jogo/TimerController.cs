using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerController : MonoBehaviour
{

    public float timeLimit = 60f;
    private float timeRemaining;
    private bool timerRunning = true;

    public Text timerText;
    public GameOverManager gameOverManager;

    private Coroutine blinkCoroutine;

    IEnumerator BlinkTimer()
    {
        while(true)
        {
            timerText.enabled = !timerText.enabled;
            yield return new WaitForSeconds(0.3f);
        }
    }


    void Start()
    {
        timeRemaining = timeLimit;
        UpdateTimerUI();
    }

    void Update()
    {
        if (timerRunning)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0f)
            {
                timeRemaining = 0f;
                timerRunning = false;

                gameOverManager.ShowGameOver();
            }

            UpdateTimerUI();

            if (timeRemaining <= 3f && blinkCoroutine == null)
            {
                blinkCoroutine = StartCoroutine(BlinkTimer());
            }
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int timeInt = Mathf.CeilToInt(timeRemaining);
            timerText.text = timeInt.ToString() + "s";

            if (timeInt <= 5)
            {
                timerText.color = Color.red;
            }
            else if (timeInt <= 10)
            {
                timerText.color = new Color(1f, 0.5f, 0f);
            }
            else
            {
                timerText.color = Color.gray;
            }
        }
    }

    public void StopTimer() => timerRunning = false;
}