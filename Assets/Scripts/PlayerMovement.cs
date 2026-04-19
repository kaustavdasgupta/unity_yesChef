using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovable
{
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float rotationSpeed = 10f;

    CharacterController controller;
    Vector2 currentInput;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Move(Vector2 direction)
    {
        currentInput = direction;
    }

    void Update()
    {
        Vector3 move = new Vector3(currentInput.x, 0, currentInput.y);
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}



