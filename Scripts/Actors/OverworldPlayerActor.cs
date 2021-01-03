using System.Collections.Generic;
using FinalFantasyRemake.Scripts.Enums;
using Godot;

namespace FinalFantasyRemake.Scripts.Actors
{
    public class OverworldPlayerActor : OverworldActor
    {
        private Dictionary<int, Vector2> _inputMapping = new Dictionary<int, Vector2>()
        {
            {(int) KeyList.W, Vector2.Up},
            {(int) KeyList.S, Vector2.Down},
            {(int) KeyList.A, Vector2.Left},
            {(int) KeyList.D, Vector2.Right},
        };
        
        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventKey eventKey)
            {
                if (@eventKey.Pressed)
                {
                    // movement
                    foreach (var key in _inputMapping.Keys)
                    {
                        if (@eventKey.Scancode == key)
                        {
                            MoveBy(_inputMapping[key]);
                        }
                    }
                    
                    // interaction
                    if (@eventKey.Scancode == (int) KeyList.Space)
                        if (State == OverworldState.IDLE)
                            DialogManager.Show("Hello World");
                        else if (State == OverworldState.DIALOG)
                            DialogManager.Hide();
                }
            }
        }
    }
}