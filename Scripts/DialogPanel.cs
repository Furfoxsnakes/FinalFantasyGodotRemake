using Godot;
using System;
using FinalFantasyRemake.Scripts.Enums;

public class DialogPanel : SlidingPanel
{
    [Export] private AudioStream _textUpAudio;
    [Export] private AudioStream _textDownAudio;

    public void Show(string dialog)
    {
        Game.OverworldPlayer.State = OverworldState.DIALOG;
        Text.Text = dialog;
        Audio.Stream = _textUpAudio;
        Audio.Play();
        GetTree().Paused = true;
        Show();
    }

    public override void Hide()
    {
        Audio.Stream = _textDownAudio;
        Audio.Play();
        base.Hide();
    }

    protected override void OnAnimationFinished(string animName)
    {
        if (animName != "Hide") return;

        GetTree().Paused = false;
        Game.OverworldPlayer.State = OverworldState.IDLE;
    }
}
