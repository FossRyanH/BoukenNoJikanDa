using System;
using Godot;

public class PlayerLocomotionState : PlayerBaseState
{
    Vector3 _velocity;

    public PlayerLocomotionState(PlayerStatemachine player) : base(player) {}

    public override void Enter()
    {
        base.Enter();
        _blendTreeName = "LocomotionTree";
        UpdateAnimationTree(_movement);
        Travel(_blendTreeName);
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);
        _velocity = _movement * Player.MoveSpeed * Player.InputDir.Length();

        Player.Velocity = _velocity;

        Player.MoveAndSlide();
        FaceDirection(_movement, delta);
    }
}