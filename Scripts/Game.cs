using Godot;
using System;
using FinalFantasyRemake.Scripts.Actors;
using FinalFantasyRemake.Scripts.Enums;
using FinalFantasyRemake.Scripts.Models;

public class Game : Node
{
    public static OverworldState OverworldState = OverworldState.IDLE;
    public static OverworldPlayerActor OverworldPlayer;
    public static DialogManager DialogManager;
    public static GameData GameData;
    
    public override void _Ready()
    {
        DialogManager = GetNode<DialogManager>("DialogManager");
        NewGame();
    }

    public void NewGame()
    {
        GameData = new GameData();
    }
}
