using Godot;
using System;
using System.Collections.Generic;
using FinalFantasyRemake.Scripts.Enums;

public class OverworldActor : KinematicBody2D
{
    private const int MoveDistance = 16;
    private const float TweenTime = 0.4f;

    private AnimationPlayer _anim;
    private AnimationTree _animationTree;
    private AnimationNodeStateMachinePlayback _animationStateMachine;
    private Tween _tween;

    private Dictionary<int, Vector2> _inputMapping = new Dictionary<int, Vector2>()
    {
        {(int) KeyList.W, Vector2.Up},
        {(int) KeyList.S, Vector2.Down},
        {(int) KeyList.A, Vector2.Left},
        {(int) KeyList.D, Vector2.Right},
    };

    public OverworldState State = OverworldState.IDLE;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _anim = GetNode<AnimationPlayer>("Anim");
        _animationTree = GetNode<AnimationTree>("AnimationTree");
        _animationStateMachine = _animationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
        _tween = GetNode<Tween>("Tween");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey eventKey)
        {
            if (@eventKey.Pressed)
            {
                foreach (var key in _inputMapping.Keys)
                {
                    if (@eventKey.Scancode == key)
                    {
                        MoveBy(_inputMapping[key]);
                    }
                }
            }
        }
    }

    public bool MoveBy(Vector2 direction)
    {
        if (Game.OverworldState != OverworldState.IDLE) return false;
        
        // collision detection
        if (TestMove(Transform, direction)) return false;

        // move and animate
        _animationTree.Set("parameters/Idle/blend_position", direction);
        _animationTree.Set("parameters/Moving/blend_position", direction);
        _animationStateMachine.Travel("Moving");
        _tween.InterpolateProperty(this, "position", Position, Position + direction * MoveDistance, TweenTime);
        Game.OverworldState = OverworldState.MOVING;
        return _tween.Start();
    }

    private void _on_Tween_tween_completed(Node node, NodePath path)
    {
        if (path == ":position")
        {
            Game.OverworldState = OverworldState.IDLE;
            _animationStateMachine.Travel("Idle");
        }
    }
}
