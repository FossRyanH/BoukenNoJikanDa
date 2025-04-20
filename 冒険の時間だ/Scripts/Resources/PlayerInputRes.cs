using Godot;
using System;

[GlobalClass]
public partial class PlayerInputRes : Resource
{
    public event Action<Vector2> Movement, PanCamera;

    public void HandleMovement(Vector2 movement) => Movement?.Invoke(movement);
    public void HandleCameraRotation(Vector2 cameraPan) => PanCamera?.Invoke(cameraPan);
}
