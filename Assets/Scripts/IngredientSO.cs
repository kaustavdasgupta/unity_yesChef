using UnityEngine;

public enum IngredientType 
{ 
    Cheese, 
    Vegetable,
    Meat 
}

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Kitchen/Ingredient")]
public class IngredientSO : ScriptableObject
{
    public string ingredientName;
    public IngredientType type;
    public int scoreValue;

    [Header("Visuals")]
    public GameObject defaultPrefab;  
    public GameObject processedPrefab;

    [Header("Processing Requirements")]
    public bool needsChopping;
    public bool needsCooking;

    public GameObject GetModel(bool isProcessed)
    {
        if (isProcessed && processedPrefab != null)
            return processedPrefab;

        return defaultPrefab;
    }
}



