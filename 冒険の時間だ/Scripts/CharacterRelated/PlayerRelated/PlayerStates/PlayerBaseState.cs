using System;
using Godot;

public class PlayerBaseState : IState, IPlayerInputListener
{
    protected PlayerStatemachine Player;
    
    #region State Required Variables
    protected Vector3 _movement;
    protected string _blendTreeName;
    float _animationSpeed = 8.5f;
    #endregion

    public PlayerBaseState(PlayerStatemachine player)
    {
        Player = player;
    }

    public virtual void Enter() 
    {
        RegisterListeners();
    }

    public virtual void Exit() 
    {
        UnregisterListeners();
    }

    public virtual void PhysicsUpdate(double delta)  
    {
        RotateCamera();
        _movement = ProcessMovement();
        ApplyGravity();
    }

    public virtual void Update(double delta) 
    {
        SetAnimWeight(_blendTreeName, delta);
    }

    void RegisterListeners() 
    {
        Player.PlayerInputs.Movement += Move;
        Player.PlayerInputs.PanCamera += PanCamera;
    }

    void UnregisterListeners() 
    {
        Player.PlayerInputs.Movement -= Move;
        Player.PlayerInputs.PanCamera -= PanCamera;
    }

    void RotateCamera()
    {
        Player.HorizontalPivot.RotateY(Player.LookDir.X);
        Player.VerticalPivot.RotateX(Player.LookDir.Y);

        var verticalRotClamp = Player.VerticalPivot.Rotation.X;
        verticalRotClamp = (float)Mathf.Clamp(Player.VerticalPivot.Rotation.X, Mathf.DegToRad(Player.MinCamBoundary),
        Mathf.DegToRad(Player.MaxCamBoundary));
        Player.VerticalPivot.Rotation = new(verticalRotClamp, 0f, 0f);

        Player.LookDir = Vector2.Zero;
    }

    protected Vector3 ProcessMovement()
    {
        Vector3 inputVec = new(Player.InputDir.X, 0f, Player.InputDir.Y);
        Vector3 direction = (Player.HorizontalPivot.GlobalTransform.Basis * inputVec).Normalized();

        UpdateAnimationTree(direction);

        return direction;
    }

    protected void FaceDirection(Vector3 direction, double delta)
    {
        if (direction.Length() == 0f) return;
        direction = direction.Normalized();

        var targetTransform = Player.PlayerRig.GlobalTransform.LookingAt(Player.PlayerRig.GlobalPosition + direction,
        Vector3.Up, true);
        var rigBasis = Player.PlayerRig.GlobalTransform.Basis;
        rigBasis = rigBasis.Slerp(targetTransform.Basis, (float)delta * 5f);

        var globalTransform = Player.PlayerRig.GlobalTransform;
        globalTransform.Basis = rigBasis;

        Player.PlayerRig.GlobalTransform = globalTransform;
    }

    void SetAnimWeight(string blendTree, double delta)
    {
        float currentWeight = (float)Player.AnimTree.Get($"parameters/{blendTree}/blend_position");
        Player.AnimTree.Set($"parameters/{blendTree}/blend_position", Mathf.MoveToward(currentWeight, Player.TargetRunWeight, _animationSpeed * (float)delta));
    }

    protected void Travel(string animName)
    {
        Player.StateMachinePlayback.Travel(animName);
    }

    protected void UpdateAnimationTree(Vector3 direction)
    {
        if (direction.IsZeroApprox())
        {
            Player.TargetRunWeight = 0f;
        }
        else
        {
            Player.TargetRunWeight = Player.InputDir.Length();
        }
    }

    public void Move(Vector2 move)
    {
        Player.InputDir = move;
        Player.InputDir.Normalized();
    }

    public void PanCamera(Vector2 move)
    {
        Player.LookDir = -move * Player.ControllerLookSensitivity;
        Player.LookDir.Normalized();
    }

    void ApplyGravity()
    {
        if (!Player.IsOnFloor())
        {
            Vector3 velo = new Vector3(Player.Velocity.X, -Player.Gravity, Player.Velocity.Z);
            Player.Velocity += velo;
            Player.MoveAndSlide();
        }
        else
        {
            return;
        }
    }
}