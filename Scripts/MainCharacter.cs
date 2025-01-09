using System;
using Godot;
using static Godot.GD;

public partial class MainCharacter : OctodirectionalMovement
{
	[ExportGroup("Nodes")]
	[Export] public AnimationPlayer AnimationPlayer;
	[Export] public PlayerAnimationTreeParametres AnimationTree;
	[Export] public Timer LookUpTimer;
	[Export] public Node2D SwingHitboxHandle;
	[ExportGroup("States")]
	[Export] public bool attacking;

	bool acting;

	public override void _Ready()
	{
		LookUpTimer.Timeout += () => setDirectionInAnimationTree(Vector2.Zero);
	}
	public override void _PhysicsProcess(double delta)
	{
		AnimationTree.swinging = false;
		// `Node2D.GetLocalMousePosition` returns the mouse position relative to the node.
		// Pretty much the same thing as `GetGlobalMousePosition() - GlobalPosition`.
		var aimDirection = GetLocalMousePosition();
		//Print(attacking);
		if (!attacking)
		{
			var cardinalAngle = Mathf.Snapped(aimDirection.Angle(), Mathf.Pi * .5f);
			SwingHitboxHandle.Rotation = cardinalAngle;
		}
		var direction = InputVector;
		Move(direction);
		
		bool isMoving = direction != Vector2.Zero;
		AnimationTree.moving = isMoving;
		if (IsActing && !attacking)
		{
			attacking = true;
			setDirectionInAnimationTree(aimDirection);
			AnimationTree.swinging = true;
		}
		else if (isMoving)
		{
			if (!attacking)
				setDirectionInAnimationTree(direction);
			//LookUpTimer.Reset();
		}

		resetInputs();
	}
	public override void _UnhandledInput(InputEvent @event)
	{
		acting = acting || @event.IsActionPressed("ActionPrimary");
	}

	public bool IsActing => acting;
	void setDirectionInAnimationTree(Vector2 direction)
	{
		AnimationTree.direction = direction.Normalized();
	}
	void resetInputs()
	{
		acting = false;
	}
}
