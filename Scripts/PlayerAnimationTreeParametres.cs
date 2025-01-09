using Godot;
using System;

[Tool]
public partial class PlayerAnimationTreeParametres : AnimationTree
{
    Vector2 _direction;
    bool _moving;
    bool _swinging;
    [Export] public Vector2 direction
    {
        get => _direction;
        set
        {
            _direction = value;
            Set("parameters/StateMachine/Idle/blend_position", value);
            Set("parameters/StateMachine/Move/blend_position", value);
            Set("parameters/StateMachine/Swing/blend_position", value);
        }
    }
    [Export] public bool moving
    {
        get => _moving;
        set
        {
            _moving = value;
        }
    }
    [Export] public bool swinging
    {
        get => _swinging;
        set
        {
            _swinging = value;
        }
    }
}
