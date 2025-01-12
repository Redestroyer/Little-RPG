using Godot;
using System;

using static Godot.Mathf;

using RangeControl = Godot.Range;

[Tool]
public partial class Enemy : CharacterBody2D, IHasHealth
{
	float health = 100;
	float maxHealth = 100;
    [Export] public virtual float Health
	{
		get => health;
		set
		{
			health = Min(value, maxHealth);
			if (HealthBar is not null) HealthBar.Value = health;
		}
	}
    [Export] public virtual float MaxHealth
	{
		get => maxHealth;
		set
		{
			var difference = value - maxHealth;
			health += difference;
			maxHealth = value;
			if (HealthBar is not null) HealthBar.MaxValue = maxHealth;
		}
	}
	RangeControl _healthBar;
	[Export] public RangeControl HealthBar
	{
		get => _healthBar;
		set
		{
			_healthBar = value;
			_healthBar.MinValue = 0;
			_healthBar.MaxValue = maxHealth;
			_healthBar.Value = health;
		}
	}
	public bool IsDead => health < 0.0;

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
