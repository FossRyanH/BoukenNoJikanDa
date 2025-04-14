using Godot;
using System;

[GlobalClass]
public partial class PlayerInputRes : Resource
{
    public event Action<Vector2> Movement;

    public void HandleMovement(Vector2 movement) => Movement?.Invoke(movement);
}
