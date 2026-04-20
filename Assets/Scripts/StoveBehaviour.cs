using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoveBehaviour : MonoBehaviour, IInteractable
{
    [SerializeField] float cookTime = 6f;
    [SerializeField] List<CookSlot> slots = new List<CookSlot>(2);

    public bool Interact(PlayerCarry player)
    {
        if (player.IsHoldingItem && player.GetCurrentItem().type == IngredientType.Meat && player.GetCurrentItem().needsCooking)
        {
            if(player.IsProcessed())
                return false;

            foreach (var slot in slots)
            {
                if (slot.itemOnSlot == null)
                {
                    slot.itemOnSlot = player.ReleaseItem();
                    slot.meatOnSlot = Instantiate(slot.itemOnSlot.GetModel(false), slot.placementSlot.transform.position, Quaternion.identity);

                    StartCoroutine(CookRoutine(slot));
                    return true;
                }
            }

            Debug.Log("Both stove slots are full!");
        }
        else if (!player.IsHoldingItem)
        {
            foreach (var slot in slots)
            {
                if (slot.itemOnSlot != null && !slot.isCooking)
                {
                    player.TryPickUp(slot.itemOnSlot, true);
                    slot.itemOnSlot = null;

                    if (slot.meatOnSlot != null)
                        Destroy(slot.meatOnSlot);

                    return true;
                }
            }
        }

        return false;
    }

    private IEnumerator CookRoutine(CookSlot slot)
    {
        slot.isCooking = true;
        yield return new WaitForSeconds(cookTime);

        if (slot.meatOnSlot != null) 
            Destroy(slot.meatOnSlot);

        slot.meatOnSlot = Instantiate(slot.itemOnSlot.GetModel(true), slot.placementSlot.transform.position, Quaternion.identity);
        slot.isCooking = false;
        Debug.Log("Cooking complete!");
    }
}

[System.Serializable]
public class CookSlot
{
    public Transform placementSlot;
    [HideInInspector] public IngredientSO itemOnSlot;
    [HideInInspector] public GameObject meatOnSlot;
    public bool isCooking;
}


