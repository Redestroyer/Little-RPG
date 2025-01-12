using Godot;
using System;
using static Godot.Mathf;

[Tool]
public partial class Enemy : CharacterBody2D, IHasHealth
{
	float health = 100;
	float maxHealth = 100;
    [Export] public virtual float Health
	{
		get => health;
		set => health = Min(value, maxHealth);
	}
    [Export] public virtual float MaxHealth
	{
		get => maxHealth;
		set
		{
			var difference = value - maxHealth;
			health += difference;
			maxHealth = value;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (Engine.IsEditorHint())
			return;
		AI();
	}

	public virtual void AI()
	{
	}

	public virtual void TakeDamage(float damage)
	{
		(this as IHasHealth).TakeDamage(damage);
	}
    public virtual void Die()
    {
        QueueFree();
    }
}
