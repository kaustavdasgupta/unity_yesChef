using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI ingredientListText;
    [SerializeField] TextMeshProUGUI floatingScoreText;

    CustomerWindow window;
    Coroutine timerCoroutine;
    Coroutine scoreCoroutine;

    private void Awake()
    {
        window = GetComponentInParent<CustomerWindow>();
    }

    public void UpdateOrderDisplay(Order order, List<IngredientSO> fulfilled)
    {
        if (order == null)
        {
            ClearDisplay();
            return;
        }

        string listContent = "";
        List<IngredientSO> tempFulfilled = new List<IngredientSO>(fulfilled);

        foreach (var req in order.requiredIngredients)
            listContent += $"<color={(tempFulfilled.Remove(req) ? "green" : "white")}>{req.ingredientName}</color>\n";

        ingredientListText.text = listContent;
        UpdateTimerUI();

        if (timerCoroutine == null)
            timerCoroutine = StartCoroutine(TimerRoutine());
    }

    private void UpdateTimerUI()
    {
        if (window == null) 
            return;

        float elapsed = window.GetOrderElapsedTime();
        timerText.text = window.HasActiveOrder() ? $"{(int)elapsed}s" : "";
    }

    private IEnumerator TimerRoutine()
    {
        while (window != null && window.HasActiveOrder())
        {
            UpdateTimerUI();
            yield return new WaitForSeconds(0.5f);
        }

        timerCoroutine = null;
    }

    public void ShowCompletionScore(int score)
    {
        if (scoreCoroutine != null) 
            StopCoroutine(scoreCoroutine);
        
        scoreCoroutine = StartCoroutine(ScoreRoutine(score));
    }

    private IEnumerator ScoreRoutine(int score)
    {
        floatingScoreText.text = (score >= 0 ? "+" : "") + score.ToString();

        yield return new WaitForSeconds(2f);

        floatingScoreText.text = "";
        scoreCoroutine = null;
    }

    public void ClearDisplay()
    {
        if (timerCoroutine != null) 
        { 
            StopCoroutine(timerCoroutine); 
            timerCoroutine = null; 
        }

        timerText.text = "";
        ingredientListText.text = "";
    }
}

