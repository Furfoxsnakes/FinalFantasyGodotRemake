using Godot;
using System;
using FinalFantasyRemake.Scripts.Enums;

public class DialogManager : CanvasLayer
{
    private DialogPanel _dialogPanel;

    public static bool IsShown = false;

    public override void _Ready()
    {
        _dialogPanel = GetNode<DialogPanel>("DialogPanel");
    }

    public void Show(string dialog)
    {
        _dialogPanel.Show(dialog);
        Game.OverworldState = OverworldState.DIALOG;
    }

    public void Hide()
    {
        _dialogPanel.Hide();
    }
}
