using Godot;
using System;

public partial class PositionLabel : Label
{
	string formatting_text;
	public override void _Ready()
	{
		formatting_text = Text;
	}
	public override void _Process(double delta)
	{
		Text = string.Format(formatting_text, (GetViewport().GetCamera2D().GlobalPosition / 16).Round());
	}
}
