using UnityEngine;
using System;

public class PlayerCarry : MonoBehaviour
{
    public event Action<IngredientSO> OnItemChanged;

    [SerializeField] Transform handSocket;
    IngredientSO currentIngredient;
    GameObject heldIngredient;
    bool isProcessed = false;

    public bool IsHoldingItem => currentIngredient != null;

    public bool TryPickUp(IngredientSO ingredient, bool processedState = false)
    {
        if (IsHoldingItem) 
            return false; 

        currentIngredient = ingredient;
        isProcessed = processedState;
        Debug.Log("Picked up: " + ingredient.ingredientName + " | is Processed: " + isProcessed);
      
        SpawnVisual();
        OnItemChanged?.Invoke(currentIngredient);
        return true;
    }

    private void SpawnVisual()
    {
        if (heldIngredient != null) 
            Destroy(heldIngredient);

        GameObject prefabToSpawn = currentIngredient.GetModel(isProcessed);

        if (prefabToSpawn != null)
        {
            heldIngredient = Instantiate(prefabToSpawn, handSocket);
            heldIngredient.transform.localPosition = Vector3.zero;
            heldIngredient.transform.localRotation = Quaternion.identity;
        }
    }

    public IngredientSO ReleaseItem()
    {
        if (!IsHoldingItem) 
            return null;

        IngredientSO releasedItem = currentIngredient;
        currentIngredient = null;

        if(heldIngredient != null)
            Destroy(heldIngredient);

        OnItemChanged?.Invoke(null);
        return releasedItem;
    }

    public IngredientSO GetCurrentItem()
    {
        if (currentIngredient != null)
        {
            return currentIngredient;
        }
        return null;
    }

    public bool IsProcessed()
    {
        return isProcessed;
    }
}




