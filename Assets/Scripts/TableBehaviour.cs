using System.Collections;
using UnityEngine;

public class TableBehaviour : MonoBehaviour, IInteractable
{
    [SerializeField] float chopTime = 2f;
    [SerializeField] Transform itemPlacementSlot;
    float interactionRange = 3.0f;

    IngredientSO itemOnTable;
    GameObject vegetableOnTable;
    float currentProgress = 0f;
    bool isDoneChopping = false;
    PlayerCarry playerRef;

    public bool Interact(PlayerCarry player)
    {
        playerRef = player;

        if (itemOnTable == null && player.IsHoldingItem)
        {
            IngredientSO held = player.GetCurrentItem();

            if (held.type == IngredientType.Vegetable && held.needsChopping)
            {
                if (player.IsProcessed())
                    return false;

                itemOnTable = player.ReleaseItem();
                currentProgress = 0f;
                isDoneChopping = false;
                vegetableOnTable = Instantiate(itemOnTable.GetModel(false),itemPlacementSlot.transform.position, Quaternion.identity);
                
                return true;
            }
        }

        if (itemOnTable != null && !player.IsHoldingItem && isDoneChopping)
        {
            player.TryPickUp(itemOnTable, true);
            itemOnTable = null;
            
            if (vegetableOnTable != null) 
                Destroy(vegetableOnTable);
           
            return true;
        }

        return false;
    }

    private void Update()
    {
        if (itemOnTable != null && !isDoneChopping)
        {
            CheckProximityAndChop();
        }
    }

    private void CheckProximityAndChop()
    {
        Debug.Log(Vector3.Distance(transform.position, playerRef.transform.position));

        if (playerRef != null && Vector3.Distance(transform.position, playerRef.transform.position) <= interactionRange)
        {
            currentProgress += Time.deltaTime;

            if (currentProgress >= chopTime)
            {
                FinishChopping();
            }
        }
    }

    private void FinishChopping()
    {
        isDoneChopping = true;
        
        if (vegetableOnTable != null) 
            Destroy(vegetableOnTable);

        vegetableOnTable = Instantiate(itemOnTable.GetModel(true), itemPlacementSlot.transform.position, Quaternion.identity);
    }
}


