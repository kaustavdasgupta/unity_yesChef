using UnityEngine;
using System;

public class InputManager : MonoBehaviour, IInputProvider
{
    public event Action<Vector2> OnMoveInput;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(horizontal, vertical).normalized;
        OnMoveInput?.Invoke(inputVector);
    }
}

