using UnityEngine;

public interface IInputProvider
{
    event System.Action<Vector2> OnMoveInput;
}

public interface IMovable
{
    void Move(Vector2 direction);
}



