using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] TextMeshProUGUI currentScoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    
    int currentScore = 0;

    private void Awake()
    {
        Instance = this;
        DisplayHighScore();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        currentScoreText.text = currentScore.ToString();
    }

    private void DisplayHighScore()
    {
        int storedHigh = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = storedHigh.ToString();
    }

    public bool RegisterFinalScore()
    {
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        bool isNewRecord = false;

        if (currentScore > savedHighScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save();
            isNewRecord = true;
        }

        DisplayHighScore();
        return isNewRecord;
    }

    public int GetCurrentScore()
    { 
        return currentScore; 
    }
}


