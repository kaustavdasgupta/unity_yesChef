using UnityEngine;

public class PlayerController : MonoBehaviour
{
    IInputProvider inputProvider;
    IMovable movable;
    
    void Start()
    {
        inputProvider = GetComponent<IInputProvider>();
        movable = GetComponent<IMovable>();

        if (inputProvider != null)
            inputProvider.OnMoveInput += HandleMovement;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position + transform.forward, 1.0f);
            foreach (var hit in hitColliders)
            {
                IInteractable interactable = hit.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact(GetComponent<PlayerCarry>());
                    break;
                }
            }
        }
    }

    private void HandleMovement(Vector2 direction)
    {
        movable.Move(direction);
    }

    private void OnDestroy()
    {
        if (inputProvider != null)
            inputProvider.OnMoveInput -= HandleMovement;
    }
}


