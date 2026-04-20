using UnityEngine;

public class TrashBehaviour : MonoBehaviour, IInteractable
{
    public bool Interact(PlayerCarry player)
    {
        if (player.IsHoldingItem)
        {
            IngredientSO discardedItem = player.ReleaseItem();
            Debug.Log("Trashed: " + discardedItem.ingredientName);

            return true;
        }

        return false;
    }
}



