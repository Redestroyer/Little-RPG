using System;
using Godot;
using static Godot.GD;
using static Godot.Mathf;

public interface IHasHealth
{
    float Health { get; set; }
    float MaxHealth { get; set; }
    bool IsDead { get; }
    void TakeDamage(float damage);
    void Die();
}
