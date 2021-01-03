using Godot;
using System;
using FinalFantasyRemake.Scripts.Enums;

public class DialogManager : CanvasLayer
{
    [Export] private AudioStreamSample _textUpAudio;
    [Export] private AudioStreamSample _textDownAudio;

    private static AnimationPlayer _anim;
    private static RichTextLabel _text;

    public static bool IsShown = false;

    public override void _Ready()
    {
        _anim = GetNode<AnimationPlayer>("Anim");
        _text = GetNode<RichTextLabel>("Container/MarginContainer/Text");
        GetNode<Control>("Container").RectSize = new Vector2(192, 0);
    }

    public static void Show(string dialog)
    {
        Game.OverworldPlayer.State = OverworldState.DIALOG;
        _text.Text = dialog;
        _anim.Play("Show");
        IsShown = true;
    }

    public static void Hide()
    {
        _anim.Play("Hide");
        IsShown = false;
    }

    private void _on_animation_finished(string animName)
    {
        if (animName != "Hide") return;
        Game.OverworldPlayer.State = OverworldState.IDLE;
    }
}
