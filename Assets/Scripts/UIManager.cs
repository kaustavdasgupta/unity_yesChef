using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] float timeRemaining = 180f;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] TextMeshProUGUI finalScoreHeading;
    [SerializeField] GameObject pauseMenuPanel;
    [SerializeField] GameObject startScreenPanel;

    bool isGameActive = false;
    bool isPaused = false;

    private void Awake()
    {
        Time.timeScale = 0f;
        startScreenPanel.SetActive(true);
        isGameActive = false;
    }

    private void InitGame()
    {
        startScreenPanel.SetActive(false);
        Time.timeScale = 1f;

        isGameActive = true;
        
        if (gameOverPanel != null) 
            gameOverPanel.SetActive(false);

        UpdateTimerDisplay(timeRemaining);
    }

    private void Update()
    {
        if (!isGameActive) 
            return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay(timeRemaining);
        }
        else
        {
            timeRemaining = 0;
            UpdateTimerDisplay(0);
            EndGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) 
                Resume();
            else 
                Pause();
        }
    }

    private void UpdateTimerDisplay(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }

    private void EndGame()
    {
        isGameActive = false;
        bool isNewRecord = ScoreManager.Instance.RegisterFinalScore();

        if (ScoreManager.Instance != null)
            ScoreManager.Instance.RegisterFinalScore();

        if (finalScoreText != null)
            finalScoreText.text = ScoreManager.Instance.GetCurrentScore().ToString();

        if (finalScoreHeading != null)
        {
            if(isNewRecord)
                finalScoreHeading.text = "New High Score!";
            else
                finalScoreHeading.text = "Final Score";
        }

        Time.timeScale = 0f;
        
        if (gameOverPanel != null) 
            gameOverPanel.SetActive(true);
    }

    public void StartGame()
    {
        InitGame();
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}



