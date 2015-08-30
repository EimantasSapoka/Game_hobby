using System;
using Assets.Scripts.Game.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class SmartEnemy : Enemy
    {

       
        public override void MoveEnemy()
        {
            var move = new MovementCoordinates(transform, Target);
            if (AttemptMove(move.ShouldMoveX, move.ShouldMoveY))
            {
                return;
            }
            // if movement was unsuccessful, switch which axis to move on and attempt that. 
            move.SwitchShouldMoveAxis();
            AttemptMove(move.ShouldMoveX, move.ShouldMoveY);
        }

       
    }
}
