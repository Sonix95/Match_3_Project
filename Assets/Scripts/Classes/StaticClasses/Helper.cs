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

        public static GameElementTypes StringToGameElementType(string gameElementTypeString)
        {
            GameElementTypes gameElementType = GameElementTypes.OrangeBox;

            switch (gameElementTypeString)
            {
                case Strings.Tag_BlueMultiAngle:
                    gameElementType = GameElementTypes.BlueMultiAngle;
                    break;
                case Strings.Tag_GreenDownTriangle:
                    gameElementType = GameElementTypes.GreenDownTriangle;
                    break;
                case Strings.Tag_OrangeBox:
                    gameElementType = GameElementTypes.OrangeBox;
                    break;
                case Strings.Tag_RedCircle:
                    gameElementType = GameElementTypes.RedCircle;
                    break;
                case Strings.Tag_YellowUpTriangle:
                    gameElementType = GameElementTypes.YellowUpTriangle;
                    break;
                
                default:
                    Debug.LogWarning("Element with this tag no found: " + gameElementTypeString);
                    break;
            }

            return gameElementType;
        }
        
        public static PowerUpTypes StringToPowerType(string powerTypeString)
        {
            PowerUpTypes powerUpType = PowerUpTypes.Vertical;

            switch (powerTypeString)
            {
                case Strings.Tag_Horizontal:
                    powerUpType = PowerUpTypes.Horizontal;
                    break;
                case Strings.Tag_Vertical:
                    powerUpType = PowerUpTypes.Vertical;
                    break;
                case Strings.Tag_Bomb:
                    powerUpType = PowerUpTypes.Bomb;
                    break;
                case Strings.Tag_ColorBomb:
                    powerUpType = PowerUpTypes.ColorBomb;
                    break;
                
                default:
                    Debug.LogWarning("PowerUp with this tag no found: " + powerTypeString);
                    break;
            }

            return powerUpType;
        }

        public static PowerUpTypes DetectPowerUp(int matchCount, AxisTypes axis)
        {
            PowerUpTypes powerUp = PowerUpTypes.Vertical;

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

        public static Vector2 SetUITaskPosition(int tasksCount, int currentTask, SceneTypes sceneType)
        {
            Vector2 position = Vector3.zero;
            int y = sceneType == SceneTypes.Menu ? 0 : -80;

            if (tasksCount == 2)
            {
                int x = sceneType == SceneTypes.Menu ? 80 : 60;
                switch (currentTask)
                {
                    case 0:

                        position = new Vector2(-x, y);
                        break;
                    case 1:
                        position = new Vector2(x, y);
                        break;
                }
            }
            else if (tasksCount == 3)
            {
                int x = sceneType == SceneTypes.Menu ? 140 : 90;
                switch (currentTask)
                {
                    case 0:
                        position = new Vector2(-x, y);
                        break;
                    case 1:
                        position = new Vector2(0, y);
                        break;
                    case 2:
                        position = new Vector2(x, y);
                        break;
                }
            }
            else
                position = new Vector2(0, y);

            return position;
        }

    }
}
