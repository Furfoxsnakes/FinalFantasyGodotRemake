using Godot;
using FinalFantasyRemake.Scripts.Enums;

public class SceneChanger : CanvasLayer
{
    private static AnimationPlayer _anim;
    private static ColorRect _curtain;
    private static string _scenePath;

    public override void _Ready()
    {
        _anim = GetNode<AnimationPlayer>("Anim");
        _curtain = GetNode<ColorRect>("Curtain");
    }

    public static void ChangeScene(string scenePath)
    {
        _scenePath = scenePath;
        _anim.Play("Fade");
    }

    private void _on_animation_finished(string animName)
    {
        if (animName == "Fade")
            Game.OverworldState = OverworldState.IDLE;
    }

    private void UpdateScene()
    {
        GetTree().ChangeScene(_scenePath);
    }
}
