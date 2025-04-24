using Godot;
using System;

public partial class PlayerStatemachine : StateMachine
{
    #region Required Nodes
    [Export] public Node3D PlayerRigPivot { get; private set; }
    [Export] public Node3D PlayerRig { get; set; }
    [Export] public SpringArm3D CameraBoom { get; set; }
    [Export] public Node3D HorizontalPivot { get; set; }
    [Export] public Node3D VerticalPivot { get; set; }
    [Export] public AnimationTree AnimTree { get; private set; }
    #endregion

    #region Inputs
    [Export] public PlayerInputRes PlayerInputs { get; private set; }
    #endregion

    #region Misc & Movement
    public Vector2 InputDir { get; set; }
    public Vector2 LookDir { get; set; }
    [Export] public float MouseSensitivity { get; private set; } = 0.005f;
    [Export] public float ControllerLookSensitivity { get; private set; } = 0.025f;
    [Export] public float MinCamBoundary { get; private set; } = -60f;
    [Export] public float MaxCamBoundary { get; private set; } = 10f;
    public float TargetRunWeight = 0f;
    public AnimationNodeStateMachinePlayback StateMachinePlayback { get; set; }
    public float MoveSpeed = 3.5f;
    public float Gravity { get; set; } = 9.82f;
    #endregion

    public override void _Ready()
    {
        AnimTree.Active = true;
        StateMachinePlayback = (AnimationNodeStateMachinePlayback)AnimTree.Get("parameters/playback");

        InitState(new PlayerLocomotionState(this));
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion eventMouseMotion)
        {
            LookDir = -eventMouseMotion.Relative * MouseSensitivity;
        }
    }
}
