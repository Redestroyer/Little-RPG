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
}
