using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BallDead : MonoBehaviour
{
    public Transform player; // Assign the Player GameObject
    public Button playButton; // Assign the Play Button in the Inspector
    public GameObject gameOverUI; // Assign the Game Over UI

    public TextMeshProUGUI livesText; // UI to show remaining lives
    public TextMeshProUGUI checkpointText; // UI to show current checkpoint
    public TextMeshProUGUI finalTimeText1;

    private static int lives = 3; // Starting lives

    public BallAttraction ballAttraction; // Reference to the BallAttraction script
    public CountdownTimer countdownTimer;

    public AudioSource audioSource;

    public GameObject timerCount;

    private void Start()
    {
        lives = 3;
        gameOverUI.SetActive(false); 
        playButton.gameObject.SetActive(false); 
        UpdateUI(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lives--; // Decrement lives
            audioSource.Play();

            if (lives > 0)
            {
                
                Debug.Log("lives"+ lives);
                UpdateUI();
                ballAttraction.Reset();
                playButton.gameObject.SetActive(true); // Show the play button
            }
            else
            {
                UpdateUI();
                ShowFinalTime();
                Debug.Log("Game Over!");
                timerCount.gameObject.SetActive(false);
                gameOverUI.SetActive(true); // Show game over UI
                playButton.gameObject.SetActive(false); // Hide the play button
                player.gameObject.SetActive(false); // Hide the player
            }
        }
    }

    public void ShowFinalTime()
    {
        finalTimeText1.text =  countdownTimer.GetRemainingTime()+" sec";
        
    }

    public void UpdateUI()
    {
        if(lives>=0)
        livesText.text = "Lives: " + lives;
        
        //checkpointText.text = "Checkpoint: " + (ballAttraction.lastCheckpointIndex + 1); // +1 for user-friendly display
    }

    
}