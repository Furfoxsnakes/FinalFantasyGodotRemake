using Godot;
using System;

public class SceneTrigger : Area2D
{
    [Export] private PackedScene _sceneToTransitionTo;

    private void _on_SceneTrigger_body_entered(OverworldActor actor)
    {
        GetTree().ChangeSceneTo(_sceneToTransitionTo);
    }
}
