using Godot;
using System;
using FinalFantasyRemake.Scripts.Enums;

public class SlidingPanel : Panel
{
    [Export] private float _slideTime = 1.0f;
    protected AnimationPlayer Anim;
    protected AudioStreamPlayer Audio;
    protected RichTextLabel Text;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Anim = GetNode<AnimationPlayer>("Anim");
        Audio = GetNode<AudioStreamPlayer>("Audio");
        Text = GetNode<RichTextLabel>("MarginContainer/Text");
        Anim.PlaybackSpeed = 1 / _slideTime;
    }

    protected virtual void OnAnimationFinished(string animName)
    {
        
    }

    public virtual void Show()
    {
        Anim.Play("Show");
    }

    public virtual void Hide()
    {
        Anim.Play("Hide");
    }
}
