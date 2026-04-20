using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerWindow : MonoBehaviour, IInteractable
{
    [SerializeField] List<IngredientSO> possibleIngredients;
    [SerializeField] float respawnDelay = 5f;
    [SerializeField] private OrderUI displayUI;

    Order currentOrder;
    List<IngredientSO> fulfilledIngredients = new List<IngredientSO>();
    bool isWaitingForOrder = false;

    private void Start()
    {
        SpawnNewOrder();
    }

    public bool Interact(PlayerCarry player)
    {
        if (currentOrder == null || isWaitingForOrder) 
            return false;
        if (player.IsHoldingItem)
        {
            IngredientSO heldItem = player.GetCurrentItem();

            if (IsNeeded(heldItem) && (heldItem.type == IngredientType.Cheese || player.IsProcessed()))
            {
                fulfilledIngredients.Add(player.ReleaseItem());
                displayUI.UpdateOrderDisplay(currentOrder, fulfilledIngredients);

                CheckForCompletion();
                return true;
            }
        }

        return false;
    }

    private bool IsNeeded(IngredientSO ingredient)
    {
        int requiredCount = currentOrder.requiredIngredients.FindAll(x => x == ingredient).Count;
        int currentCount = fulfilledIngredients.FindAll(x => x == ingredient).Count;

        return currentCount < requiredCount;
    }

    private void CheckForCompletion()
    {
        if (fulfilledIngredients.Count == currentOrder.requiredIngredients.Count)
            CompleteOrder();
    }

    public bool HasActiveOrder()
    {
        return currentOrder != null;
    }

    private void SpawnNewOrder()
    {
        isWaitingForOrder = false;
        fulfilledIngredients.Clear();
        currentOrder = new Order(possibleIngredients);
        displayUI.UpdateOrderDisplay(currentOrder, fulfilledIngredients);
    }

    private IEnumerator RespawnRoutine()
    {
        isWaitingForOrder = true;
        displayUI.ClearDisplay();

        yield return new WaitForSeconds(respawnDelay);

        isWaitingForOrder = false;
        SpawnNewOrder();
    }

    private void CompleteOrder()
    {
        int orderBaseValue = 0;
        foreach (var ing in currentOrder.requiredIngredients) orderBaseValue += ing.scoreValue;

        float timeTaken = Time.time - currentOrder.startTime;
        int finalScore = orderBaseValue - Mathf.FloorToInt(timeTaken);       
        
        ScoreManager.Instance.AddScore(finalScore);        
        displayUI.ShowCompletionScore(finalScore);
        
        currentOrder = null;
        fulfilledIngredients.Clear();

        StartCoroutine(RespawnRoutine());
    }

    public void StartWaiting()
    {
        StartCoroutine(RespawnRoutine());
    }

    public float GetOrderElapsedTime()
    {
        if (currentOrder != null)
            return (Time.time - currentOrder.startTime);
        else
            return 0f;
    } 
}



