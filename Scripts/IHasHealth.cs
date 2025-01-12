using System;
using Godot;
using static Godot.GD;
using static Godot.Mathf;

public interface IHasHealth
{
    float Health { get; set; }
    float MaxHealth { get; set; }
    bool IsDead => Health < 0.0;
    void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
            Die();
    }
    void Die();
}
