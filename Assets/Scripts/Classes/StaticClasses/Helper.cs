using Mathc3Project.Enums;
using UnityEngine;

namespace Mathc3Project.Classes.StaticClasses
{
    public class Helper
    {
        public static MoveDirectionTypes FindMoveDirection(Vector3 a, Vector3 b)
        {
            MoveDirectionTypes moveDirection = MoveDirectionTypes.none;

            float angle = Mathf.Atan2(b.y - a.y, b.x - a.x) * 180 / Mathf.PI;
            
            if (angle > -45 && angle <= 45)
            {
                moveDirection = MoveDirectionTypes.Right;
            }
            else if (angle > 45 && angle <= 135)
            {
                moveDirection = MoveDirectionTypes.Up;
            }
            else if (angle > 135 || angle <= -135)
            {
                moveDirection = MoveDirectionTypes.Left;
            }
            else if (angle >= -135 && angle < -45)
            {
                moveDirection = MoveDirectionTypes.Down;
            }
            
            return moveDirection;
        }
    }
}
