using Godot;
using System;

public partial class InputManager : Singleton<InputManager>
{
    #region Input Resources
    [Export] PlayerInputRes _playerInputs;
    #endregion

    Vector2 _inputVec;

    public override void _Input(InputEvent @event)
    {
        _inputVec = Input.GetVector("MoveLeft", "MoveRight", "MoveForward", "MoveBackward");

        _playerInputs.HandleMovement(_inputVec != Vector2.Zero ? _inputVec : Vector2.Zero);
    }

}
