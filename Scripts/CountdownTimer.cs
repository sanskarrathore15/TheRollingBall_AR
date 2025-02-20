using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Assign in Inspector
    public Button startButton; // Assign the UI Button in Inspector
    public float totalTime = 180f; // 3 minutes in seconds

    private float currentTime;
    private bool isTimerRunning = false;

    public GameObject gameOverUI;
    public GameObject InfoBtn;

    public bool isRunning = true;

    void Start()
    {
        timerText.text = "Snow World";
        timerText.fontSize = 12;
        currentTime = totalTime;
   
    }



    public void StartTimer()
    {
        Debug.Log("Timer Started!");
        if (!isTimerRunning) // Prevent multiple countdownsm
        {
            timerText.fontSize = 12;
            startButton.gameObject.SetActive(false);
            isTimerRunning = true;
            //timerText.gameObject.SetActive(true); // Show the timer when clicked
            StartCoroutine(StartCountdown());
        }
    }

    public IEnumerator StartCountdown()
    {
      
            while (currentTime > 0 && isRunning)
            {
           
                UpdateTimerDisplay();
                yield return new WaitForSeconds(1f);
                currentTime--;
            }

        if (isRunning)
        {
            currentTime = 0;
            UpdateTimerDisplay();
            TimerFinished();
        }
        

    }

        void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerFinished()
    {
        Debug.Log("Timer Finished!");
        timerText.text = "Time Up!";
        isTimerRunning = false;
        gameOverUI.SetActive(true);
    }

    public string GetRemainingTime()
    {
        int minutes =  Mathf.FloorToInt((totalTime - currentTime) / 60);
        int seconds =  Mathf.FloorToInt((totalTime - currentTime) % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void moreInfo()
    {
        timerText.fontSize = 4;
        timerText.text = "The village of Snows is fallen due to rain help the snowyy to reach 'Falling Snows' by following the path and tackling the obstacles\n"+ "Press Start";
        InfoBtn.gameObject.SetActive(false);
    }


}
