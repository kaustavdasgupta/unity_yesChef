using System;
using UnityEngine;

public class FridgeBehaviour : MonoBehaviour, IInteractable
{
    public static event Action OnFridgeOpened;

    public bool Interact(PlayerCarry player)
    {
        if (player.IsHoldingItem)
        {
            Debug.Log("Hand is full!");
            return false;
        }

        OnFridgeOpened?.Invoke();
        return true;
    }
}


