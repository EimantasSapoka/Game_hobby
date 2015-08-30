using Assets.Scripts.Game.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class DumbEnemy : Enemy, IWallDamage
    {

        public int WallDamage = 2;

        protected override bool AttemptMove(int xDir, int yDir)
        {
            if (skipMove)
            {
                skipMove = false;
                return false;
            }
            skipMove = true;

            return base.AttemptMove(xDir, yDir); 
        }

        public override void MoveEnemy()
        {
            var move = new MovementCoordinates(transform, Target.transform);
            AttemptMove(move.ShouldMoveX, move.ShouldMoveY);

        }

        private bool skipMove;

        public int GetWallDamage()
        {
            TriggerHitAnimation();
            return WallDamage;
        }
    }
}
