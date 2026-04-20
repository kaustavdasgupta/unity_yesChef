using UnityEngine;
using UnityEngine.UI;

public class FridgeUIController : MonoBehaviour
{
    [SerializeField] GameObject fridgeUIPanel;
    [SerializeField] PlayerCarry player;
    [SerializeField] InputManager inputManager;

    void Start()
    {
        FridgeBehaviour.OnFridgeOpened += OpenPanel;
        fridgeUIPanel.SetActive(false);
    }

    private void OpenPanel()
    {
        fridgeUIPanel.SetActive(true);
        inputManager.SetInputLock(true);
    }

    public void SelectIngredient(IngredientSO data)
    {
        if (player.TryPickUp(data))
        {
            ClosePanel();
        }
        else
        {
            Debug.Log("Hand is full! Cannot take " + data.ingredientName);
        }
    }

    public void ClosePanel()
    {
        fridgeUIPanel.SetActive(false);
        inputManager.SetInputLock(false);
    }

    private void OnDestroy()
    {
        FridgeBehaviour.OnFridgeOpened -= OpenPanel;
    }
}


