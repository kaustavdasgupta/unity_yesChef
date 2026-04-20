using UnityEngine;

public interface IInputProvider
{
    event System.Action<Vector2> OnMoveInput;
}

public interface IMovable
{
    void Move(Vector2 direction);
}

public interface IInteractable
{
    bool Interact(PlayerCarry player);
}

