using Godot;
using System;
using FinalFantasyRemake.Scripts.Actors;

public class SubArea : TileMap
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Game.OverworldPlayer = GetNode<OverworldPlayerActor>("OverworldPlayerActor");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
