using Godot;
using System;

public partial class CameraBoom : SpringArm3D
{
    [Export] Node3D _target;
    [Export] float _decay = 15f;

    public override void _PhysicsProcess(double delta)
    {
        GlobalTransform = GlobalTransform.InterpolateWith(_target.GlobalTransform, 1f - Mathf.Exp(-_decay * (float)delta));
    }

}
