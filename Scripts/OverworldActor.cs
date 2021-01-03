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

    public OverworldState State = OverworldState.IDLE;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _anim = GetNode<AnimationPlayer>("Anim");
        _animationTree = GetNode<AnimationTree>("AnimationTree");
        _animationStateMachine = _animationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
        _tween = GetNode<Tween>("Tween");
    }

    public bool MoveBy(Vector2 direction)
    {
        if (State != OverworldState.IDLE) return false;
        
        // collision detection
        if (TestMove(Transform, direction * MoveDistance)) return false;

        // move and animate
        _animationTree.Set("parameters/Idle/blend_position", direction);
        _animationTree.Set("parameters/Moving/blend_position", direction);
        _animationStateMachine.Travel("Moving");
        _tween.InterpolateProperty(this, "position", Position, Position + direction * MoveDistance, TweenTime);
        State = OverworldState.MOVING;
        return _tween.Start();
    }

    public virtual void _on_Tween_tween_completed(Node node, NodePath path)
    {
        // set actor to idle if it's position tween has finished
        if (path == ":position")
        {
            State = OverworldState.IDLE;
            _animationStateMachine.Travel("Idle");
        }
    }
}
