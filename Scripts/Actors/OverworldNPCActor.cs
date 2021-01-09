using Godot;
using System;
using System.Runtime.InteropServices;

public class OverworldNPCActor : OverworldActor
{
    private Timer _patrolTimer;

    private Vector2[] _directions = {
        Vector2.Up,
        Vector2.Right,
        Vector2.Down,
        Vector2.Left
    };

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        _patrolTimer = GetNode<Timer>("PatrolTimer");
    }

    private void _on_PatrolTimer_timeout()
    {
        GD.Randomize();
        var randNum = (int) GD.RandRange(0, _directions.Length - 1);
        var direction = _directions[randNum];
        MoveBy(direction);
    }
}
