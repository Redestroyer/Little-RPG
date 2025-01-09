using Godot;
using System;

public partial class OctodirectionalMovement : CharacterBody2D
{
	[ExportGroup("Movement")]
	[Export] public float Speed = 32f; // In pixels per second.
	
	public override void _PhysicsProcess(double delta)
	{
		Move(InputVector);
	}
	// `Input.GetVector`'s return value is already normalised by default.
	public virtual Vector2 InputVector => Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
	public virtual void Move(Vector2 direction)
	{
		Velocity = direction * Speed * 2;
		MoveAndSlide();
	}
}
