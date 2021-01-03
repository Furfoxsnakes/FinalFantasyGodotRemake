using Godot;
using System;
using FinalFantasyRemake.Scripts.Actors;
using FinalFantasyRemake.Scripts.Enums;

public class Game : Node
{
    public static OverworldState OverworldState = OverworldState.IDLE;
    public static OverworldPlayerActor OverworldPlayer;

    public override void _Ready()
    {
        
    }
}
