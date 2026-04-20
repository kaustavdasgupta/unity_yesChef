using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Order
{
    public List<IngredientSO> requiredIngredients = new List<IngredientSO>();
    public float startTime;
    public bool isComplete = false;

    public Order(List<IngredientSO> availableIngredients)
    {
        startTime = Time.time;
        int count = Random.Range(0, 2) == 0 ? 2 : 3;

        for (int i = 0; i < count; i++)
            requiredIngredients.Add(availableIngredients[Random.Range(0, availableIngredients.Count)]);
    }

    public int CalculateScore()
    {
        int totalValue = 0;

        foreach (var ingredient in requiredIngredients)
            totalValue += ingredient.scoreValue;

        float elapsedSeconds = Time.time - startTime;
        return totalValue - Mathf.FloorToInt(elapsedSeconds);
    }
}

