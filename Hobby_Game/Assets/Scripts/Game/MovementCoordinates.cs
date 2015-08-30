using System;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class MovementCoordinates
    {

        public readonly float DistanceFromTargetX;
        public readonly float DistanceFromTargetY;
        public readonly int MoveDirectionX;
        public readonly int MoveDirectionY;
        public int ShouldMoveX { get; private set; }
        public int ShouldMoveY { get; private set; }


        public MovementCoordinates(Transform movingObject, Transform target)
        {
            MoveDirectionX = 0;
            MoveDirectionY = 0;

            DistanceFromTargetY = Math.Abs(target.position.y -  movingObject.position.y);
            DistanceFromTargetX = Math.Abs(target.position.x - movingObject.position.x);

            if (DistanceFromTargetY > float.Epsilon)
                MoveDirectionY = target.position.y >  movingObject.position.y ? 1 : -1;

            if (DistanceFromTargetX > float.Epsilon)
                MoveDirectionX = target.position.x > movingObject.position.x ? 1 : -1;

            ShouldMoveX = 0;
            ShouldMoveY = 0;

            // if the enemy is further away from player on X direction, move on X then
            if (DistanceFromTargetX > DistanceFromTargetY)
            {
                ShouldMoveX = MoveDirectionX;
            }
            // if it's further away on Y direction, attempt to move Y then
            else if (DistanceFromTargetX < DistanceFromTargetY)
            {
                ShouldMoveY = MoveDirectionY;
            }
            // if enemy is equal distance on y and X, attempt to move X first
            else
            {
                ShouldMoveX = MoveDirectionX;
            }

        }

        public void SwitchShouldMoveAxis()
        {
            ShouldMoveX = ShouldMoveX == 0 ? MoveDirectionX : 0;
            ShouldMoveY = ShouldMoveY == 0 ? MoveDirectionY : 0;
        }

    }
}
