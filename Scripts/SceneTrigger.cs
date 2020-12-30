using Godot;
using System;

public class SceneTrigger : Area2D
{
    [Export] private string _scenePath;

    private void _on_SceneTrigger_body_entered(Node body)
    {
        if (body is OverworldActor)
            SceneChanger.ChangeScene(_scenePath);
    }
}
