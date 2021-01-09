using Godot;
using System;
using System.Collections.Generic;
using FinalFantasyRemake.Scripts.Enums;

public class OverworldActor : KinematicBody2D
{
    protected const int MoveDistance = 16;
    private const float TweenTime = 0.4f;

    [Export] private float MoveTime = 1f;
    
    private AnimationPlayer _anim;
    private AnimationTree _animationTree;
    private AnimationNodeStateMachinePlayback _animationStateMachine;
    private AnimationNodeTimeScale _animationNodeTimeScale;
    private Tween _tween;

    public OverworldState State = OverworldState.IDLE;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _anim = GetNode<AnimationPlayer>("Anim");
        _animationTree = GetNode<AnimationTree>("AnimationTree");
        _animationStateMachine = _animationTree.Get("parameters/StateMachine/playback") as AnimationNodeStateMachinePlayback;
        // _animationNodeTimeScale = _animationTree.Get("parameters/TimeScale/scale") as AnimationNodeTimeScale;
        _animationTree.Set("parameters/TimeScale/scale", 1 / MoveTime);
        _tween = GetNode<Tween>("Tween");
    }

    public bool MoveBy(Vector2 direction)
    {
        if (State != OverworldState.IDLE) return false;
        
        LookAt(direction);

        // collision detection
        if (TestMove(Transform, direction * MoveDistance)) return false;

        // move and animate
        _animationStateMachine.Travel("Moving");
        _tween.InterpolateProperty(this, "position", Position, Position + direction * MoveDistance, MoveTime);
        State = OverworldState.MOVING;
        return _tween.Start();
    }

    protected virtual void LookAt(Vector2 direction)
    {
        _animationTree.Set("parameters/StateMachine/Idle/blend_position", direction);
        _animationTree.Set("parameters/StateMachine/Moving/blend_position", direction);
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
