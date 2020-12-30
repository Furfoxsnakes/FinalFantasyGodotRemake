using Godot;
using System;
using System.Collections.Generic;
using FinalFantasyRemake.Scripts.Enums;

public class OverworldActor : Node2D
{
    private const int MoveDistance = 16;
    private const float TweenTime = 0.4f;

    private AnimationPlayer _anim;
    private AnimationTree _animationTree;
    private AnimationNodeStateMachinePlayback _animationStateMachine;
    private RayCast2D _upCast;
    private RayCast2D _downCast;
    private RayCast2D _leftCast;
    private RayCast2D _rightCast;
    private Tween _tween;

    private Dictionary<int, Vector2> _inputMapping = new Dictionary<int, Vector2>()
    {
        {(int) KeyList.W, Vector2.Up},
        {(int) KeyList.S, Vector2.Down},
        {(int) KeyList.A, Vector2.Left},
        {(int) KeyList.D, Vector2.Right},
    };

    public Dictionary<Vector2, RayCast2D> _raycastDirections;

    public OverworldState State = OverworldState.IDLE;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _anim = GetNode<AnimationPlayer>("Anim");
        _animationTree = GetNode<AnimationTree>("AnimationTree");
        _animationStateMachine = _animationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
        _upCast = GetNode<RayCast2D>("UpCast");
        _downCast = GetNode<RayCast2D>("DownCast");
        _leftCast = GetNode<RayCast2D>("LeftCast");
        _rightCast = GetNode<RayCast2D>("RightCast");
        _tween = GetNode<Tween>("Tween");

        _raycastDirections = new Dictionary<Vector2, RayCast2D>()
        {
            {Vector2.Up, _upCast},
            {Vector2.Down, _downCast},
            {Vector2.Left, _leftCast},
            {Vector2.Right, _rightCast}
        };
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
        // collision detection
        if (_raycastDirections[direction].IsColliding()) return false;
        
        if (State != OverworldState.IDLE) return false;

        // move and animate
        _animationTree.Set("parameters/Idle/blend_position", direction);
        _animationTree.Set("parameters/Moving/blend_position", direction);
        _animationStateMachine.Travel("Moving");
        _tween.InterpolateProperty(this, "position", Position, Position + direction * MoveDistance, TweenTime);
        State = OverworldState.MOVING;
        return _tween.Start();
    }

    private void _on_Tween_tween_completed(Node node, NodePath path)
    {
        if (path == ":position")
        {
            State = OverworldState.IDLE;
            _animationStateMachine.Travel("Idle");
        }
    }
}
