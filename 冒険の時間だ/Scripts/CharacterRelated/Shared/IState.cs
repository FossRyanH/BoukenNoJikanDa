using Godot;
using System;

public interface IState
{
    public void Enter();
    public void Update(double delta);
    public void PhysicsUpdate(double delta);
    public void Exit();
}
