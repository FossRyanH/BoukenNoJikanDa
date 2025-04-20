using System;
using System.Collections.Generic;
using Godot;

public interface IPlayerInputListener
{
    void Move(Vector2 move);
    void PanCamera(Vector2 move);
}