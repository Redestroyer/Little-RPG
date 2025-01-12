using System;
using Godot;
using static Godot.GD;

[Tool]
public partial class MainCharacter : OctodirectionalMovement
{
	[ExportGroup("Nodes")]
	[Export] public AnimationPlayer AnimationPlayer;
	[Export] public PlayerAnimationTreeParametres AnimationTree;
    Timer _lookUpTimer;
    [Export] public Timer LookUpTimer
    {
        get => _lookUpTimer;
        set
        {
            _lookUpTimer = value;
			_lookUpTimer.Timeout += () => setDirectionInAnimationTree(Vector2.Zero);
        }
    }
    Node2D _swingHitboxHandle;
	Area2D _swingHitbox;
    [Export] public Node2D SwingHitboxHandle
    {
        get => _swingHitboxHandle;
		set
        {
			if (_swingHitboxHandle != null)
			{
				_swingHitbox.BodyEntered -= damageEnemy;
			}
            _swingHitboxHandle = value;
			_swingHitbox = _swingHitboxHandle.GetChild<Area2D>(0);
			_swingHitbox.BodyEntered += damageEnemy;
        }
    }

    [ExportGroup("States")]
	[Export] public bool attacking;

	bool acting;

	/*public override void _Ready()
	{
		LookUpTimer.Timeout += () => setDirectionInAnimationTree(Vector2.Zero);
	}*/
	public override void _PhysicsProcess(double delta)
	{
		if (Engine.IsEditorHint())
			return;

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
			LookUpTimer.Reset();
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
    private void damageEnemy(Node2D body)
    {
        if (Engine.IsEditorHint()) return;

		if (body is Enemy enemy)
		{
			enemy.TakeDamage(30);
			enemy.Position += SwingHitboxHandle.Transform.X * 16;
		}
	}
}
