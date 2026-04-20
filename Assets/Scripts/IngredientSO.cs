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
    public Color displayColor = Color.white;
    public GameObject prefab;

    [Header("Processing Requirements")]
    public bool needsChopping;
    public bool needsCooking;
}



