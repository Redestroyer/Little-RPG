using Godot;
using System;
using static Godot.Mathf;

[Tool]
public partial class CrawlerEnemy : Enemy
{
	[ExportGroup("Nodes")]
	[Export] public Node2D PlayerCharacter;
	AnimationPlayer _animationPlayer;
	[Export] public AnimationPlayer AnimationPlayer
	{
		get => _animationPlayer;
		set
		{
			if (_animationPlayer != null)
			{
				_animationPlayer.AnimationFinished -= disappear;
			}
			_animationPlayer = value;
			_animationPlayer.AnimationFinished += disappear;
		}
	}
	[ExportGroup("Movement")]
	[Export] public float Speed = 32f; // In pixels per second.
    [ExportGroup("Detection")]
    float detectionRadius = 32f; // In pixels.
	float detectionRadiusSquared;
    [Export] public float DetectionRadius
    {
        get => detectionRadius;
        set
        {
            detectionRadius = value;
			detectionRadiusSquared = detectionRadius * detectionRadius;
			if (undetectionRadius < value) UndetectionRadius = value;
			QueueRedraw();
        }
    }
    float undetectionRadius = 48f; // In pixels.
	float undetectionRadiusSquared;
    [Export] public float UndetectionRadius
    {
        get => undetectionRadius;
        set
        {
            undetectionRadius = Max(detectionRadius, value);
			undetectionRadiusSquared = undetectionRadius * undetectionRadius;
			QueueRedraw();
        }
    }
    public bool detected { get; protected set; }

    public override void _Draw()
    {
        if (!Engine.IsEditorHint()) return;
		// Draw two circles around the enemy to represent the detection (blue) and undetection (red) radii.
		DrawCircle(Vector2.Zero, detectionRadius, new Color(0, 0, 1, .5f));
		DrawCircle(Vector2.Zero, undetectionRadius, new Color(1, 0, 0, .5f));
    }

    public override void AI()
    {
        if (PlayerCharacter == null) return;

		var pathToPlayer = PlayerCharacter.GlobalPosition - GlobalPosition;
		var distanceSquared = pathToPlayer.LengthSquared();
		detected = (detected && distanceSquared <= undetectionRadiusSquared) || distanceSquared <= detectionRadiusSquared;
		if (detected)
		{
			Velocity = pathToPlayer.Normalized() * Speed;
			MoveAndSlide();
		}
    }

    public override void TakeDamage(float damage)
    {
        Health -= damage;
		if (Health <= 0) _animationPlayer.ForcePlay("Death");
		else _animationPlayer.ForcePlay("Hurt");
    }

	void disappear(StringName animationName)
	{
		if (Engine.IsEditorHint()) return;
		if (animationName == "Death") QueueFree();
	}
}
