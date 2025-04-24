using Godot;
using System;
using System.Collections.Generic;

public partial class InputManager : Singleton<InputManager>
{
    #region Input Resources
    [Export] PlayerInputRes _playerInputs;
    #endregion

    Vector2 _inputVec;
    Vector2 _cameraInput;

    protected override void Initialize()
    {
        GameStateManager.GameStateChanged += GameStateChange;
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _ExitTree()
    {
        GameStateManager.GameStateChanged -= GameStateChange;
    }



    public override void _Input(InputEvent @event)
    {
        if (GameStateManager.GameState == GameState.GamePlay)
        {
            _inputVec = Input.GetVector("MoveLeft", "MoveRight", "MoveForward", "MoveBackward");
            _playerInputs.HandleMovement(_inputVec != Vector2.Zero ? _inputVec : Vector2.Zero);
        }
    }

    public override void _Process(double delta)
    {
        _cameraInput = Input.GetVector("CameraLeft", "CameraRight", "CameraUp", "CameraDown");
        _playerInputs.HandleCameraRotation(_cameraInput != Vector2.Zero ? _cameraInput : Vector2.Zero);
    }

    void GameStateChange(GameState state)
    {
        if (state != GameState.GamePlay)
        {
            Input.MouseMode = Input.MouseModeEnum.Confined;
        }
        else
        {
            Input.MouseMode = Input.MouseModeEnum.Captured;
        }
    }
}
