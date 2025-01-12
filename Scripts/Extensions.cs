using System;
using Godot;
using static Godot.GD;

public static class Extensions
{
    /// <summary>
    /// Resets the timer by stopping and starting it again.
    /// </summary>
    /// <param name="timer"></param>
    public static void Reset(this Timer timer)
    {
        timer.Stop();
        timer.Start();
    }

    public static void ForcePlay(this AnimationPlayer animationPlayer, StringName name = default, double customBlend = -1.0, float customSpeed = 1f, bool fromEnd = false)
    {
        animationPlayer.Advance(double.PositiveInfinity);
        animationPlayer.Play(name, customBlend, customSpeed, fromEnd);
    }
}
