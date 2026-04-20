using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] float timeRemaining = 181f;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject gameOverPanel;

    bool isGameActive = false;

    private void Start()
    {
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

        if (ScoreManager.Instance != null)
            ScoreManager.Instance.RegisterFinalScore();

        Time.timeScale = 0f;
        
        if (gameOverPanel != null) 
            gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}



