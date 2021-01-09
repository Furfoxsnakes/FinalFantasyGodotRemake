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

        private RayCast2D _rayCast;
        private Vector2 _movement;

        public override void _Ready()
        {
            base._Ready();
            _rayCast = GetNode<RayCast2D>("RayCast");
        }

        public override void _Process(float delta)
        {
            base._Process(delta);
            _movement = new Vector2(Input.GetActionStrength("MoveRight") - Input.GetActionStrength("MoveLeft"),
                Input.GetActionStrength("MoveDown") - Input.GetActionStrength("MoveUp"));

            if (_movement != Vector2.Zero)
                MoveBy(_movement);

            if (Input.IsActionJustPressed("Interact"))
            {
                if (State == OverworldState.IDLE)
                {
                    var interactable = _rayCast.GetCollider() as Interactable;
                    var textToDisplay = "Nothing here.";

                    if (interactable != null)
                        textToDisplay = interactable.DialogText;
                            
                    Game.DialogManager.Show(textToDisplay);
                }
                else if (State == OverworldState.DIALOG)
                {
                    Game.DialogManager.Hide();
                }
            }
        }

        public override void _Input(InputEvent @event)
        {
            return;
            
            if (@event is InputEventKey eventKey)
            {
                if (@eventKey.Pressed)
                {
                    // movement
                    foreach (var key in _inputMapping.Keys)
                    {
                        if (@eventKey.Scancode == key)
                        {
                            // LookAt(_inputMapping[key]);
                            if (MoveBy(_inputMapping[key]))
                                _rayCast.CastTo = _inputMapping[key] * MoveDistance;
                        }
                    }
                    
                    // interaction
                    if (@eventKey.Scancode == (int) KeyList.Space)
                    {
                        if (State == OverworldState.IDLE)
                        {
                            var interactable = _rayCast.GetCollider() as Interactable;
                            var textToDisplay = "Nothing here.";

                            if (interactable != null)
                                textToDisplay = interactable.DialogText;
                            
                            Game.DialogManager.Show(textToDisplay);
                        }
                        else if (State == OverworldState.DIALOG)
                        {
                            Game.DialogManager.Hide();
                        }
                    }
                }
            }
        }

        protected override void LookAt(Vector2 direction)
        {
            base.LookAt(direction);
            _rayCast.CastTo = direction * MoveDistance;
        }
    }
}