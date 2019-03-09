using Mathc3Project.Enums;
using Mathc3Project.Interfaces.Cells;
using UnityEngine;

namespace Mathc3Project.Classes.StaticClasses
{
    public static class Helper
    {
        public static MoveDirectionTypes FindMoveDirection(Vector3 a, Vector3 b)
        {
            MoveDirectionTypes moveDirection = MoveDirectionTypes.None;

            float angle = Mathf.Atan2(b.y - a.y, b.x - a.x) * 180 / Mathf.PI;
            
            if (angle > -45 && angle <= 45)
                moveDirection = MoveDirectionTypes.Right;
            else if (angle > 45 && angle <= 135)
                moveDirection = MoveDirectionTypes.Up;
            else if (angle > 135 || angle <= -135)
                moveDirection = MoveDirectionTypes.Left;
            else if (angle >= -135 && angle < -45)
                moveDirection = MoveDirectionTypes.Down;
            
            return moveDirection;
        }

        public static PowerUpTypes StringToPowerType(string powerTypeString)
        {
            PowerUpTypes powerUpType = PowerUpTypes.None;

            switch (powerTypeString)
            {
                case MagicStrings.Tag_Horizontal:
                    powerUpType = PowerUpTypes.Horizontal;
                    break;
                case MagicStrings.Tag_Vertical:
                    powerUpType = PowerUpTypes.Vertical;
                    break;
                case MagicStrings.Tag_Bomb:
                    powerUpType = PowerUpTypes.Bomb;
                    break;
                case MagicStrings.Tag_ColorBomb:
                    powerUpType = PowerUpTypes.ColorBomb;
                    break;
            }

            return powerUpType;
        }

        public static PowerUpTypes DetectPowerUp(int matchCount, AxisTypes axis)
        {
            PowerUpTypes powerUp = PowerUpTypes.None;

            if (matchCount == 4)
            {
                switch (axis)
                {
                    case AxisTypes.Vertical:
                        powerUp = PowerUpTypes.Vertical;
                        break;
                    case AxisTypes.Horizontal:
                        powerUp = PowerUpTypes.Horizontal;
                        break;
                }
            }
            else if (matchCount == 5)
            {
                powerUp = PowerUpTypes.Bomb;
            }
            else
                powerUp = PowerUpTypes.ColorBomb;

            return powerUp;
        }

        public static bool CellIsEmpty(ICell cell)
        {
            if (cell == null || cell.CellType == CellTypes.Hollow || cell.CurrentGameObject == null)
                return true;

            return false;
        }
        
        public static void MarkCell(ICell cell)
        {
            SpriteRenderer render = cell.CurrentGameObject.GetComponent<SpriteRenderer>();
            render.color = new Color(render.color.r, render.color.g, render.color.b, .2f);
        }
        
    }
}
