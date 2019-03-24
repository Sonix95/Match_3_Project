using Mathc3Project.Enums;
using Mathc3Project.Interfaces.Cells;
using UnityEngine;

namespace Mathc3Project.Classes.StaticClasses
{
    public static class Helper
    {
        public static MoveDirectionTypesEnum FindMoveDirection(Vector3 a, Vector3 b)
        {
            MoveDirectionTypesEnum moveDirection = MoveDirectionTypesEnum.None;

            float angle = Mathf.Atan2(b.y - a.y, b.x - a.x) * 180 / Mathf.PI;
            
            if (angle > -45 && angle <= 45)
                moveDirection = MoveDirectionTypesEnum.Right;
            else if (angle > 45 && angle <= 135)
                moveDirection = MoveDirectionTypesEnum.Up;
            else if (angle > 135 || angle <= -135)
                moveDirection = MoveDirectionTypesEnum.Left;
            else if (angle >= -135 && angle < -45)
                moveDirection = MoveDirectionTypesEnum.Down;
            
            return moveDirection;
        }

        public static GameElementTypesEnum StringToGameElementType(string gameElementTypeString)
        {
            GameElementTypesEnum gameElementTypeEnum = GameElementTypesEnum.OrangeBox;

            switch (gameElementTypeString)
            {
                case Strings.TAG_BLUEMULTIANGLE:
                    gameElementTypeEnum = GameElementTypesEnum.BlueMultiAngle;
                    break;
                case Strings.TAG_GREENDOWNTIRANGLE:
                    gameElementTypeEnum = GameElementTypesEnum.GreenDownTriangle;
                    break;
                case Strings.TAG_ORANGEBOX:
                    gameElementTypeEnum = GameElementTypesEnum.OrangeBox;
                    break;
                case Strings.TAG_REDCIRCLE:
                    gameElementTypeEnum = GameElementTypesEnum.RedCircle;
                    break;
                case Strings.TAG_YELLOWUPTRIANGLE:
                    gameElementTypeEnum = GameElementTypesEnum.YellowUpTriangle;
                    break;
                
                default:
                    Debug.LogWarning("Element with this tag no found: " + gameElementTypeString);
                    break;
            }

            return gameElementTypeEnum;
        }
        
        public static PowerUpTypesEnum StringToPowerType(string powerTypeString)
        {
            PowerUpTypesEnum powerUpTypeEnum = PowerUpTypesEnum.Vertical;

            switch (powerTypeString)
            {
                case Strings.TAG_HORIZONTAL:
                    powerUpTypeEnum = PowerUpTypesEnum.Horizontal;
                    break;
                case Strings.TAG_VERICAL:
                    powerUpTypeEnum = PowerUpTypesEnum.Vertical;
                    break;
                case Strings.TAG_BOMB:
                    powerUpTypeEnum = PowerUpTypesEnum.Bomb;
                    break;
                case Strings.TAG_COLORBOMB:
                    powerUpTypeEnum = PowerUpTypesEnum.ColorBomb;
                    break;
                
                default:
                    Debug.LogWarning("PowerUp with this tag no found: " + powerTypeString);
                    break;
            }

            return powerUpTypeEnum;
        }

        public static PowerUpTypesEnum DetectPowerUp(int matchCount, AxisTypesEnum axis)
        {
            PowerUpTypesEnum powerUp = PowerUpTypesEnum.Vertical;

            if (matchCount == 4)
            {
                switch (axis)
                {
                    case AxisTypesEnum.Vertical:
                        powerUp = PowerUpTypesEnum.Vertical;
                        break;
                    case AxisTypesEnum.Horizontal:
                        powerUp = PowerUpTypesEnum.Horizontal;
                        break;
                }
            }
            else if (matchCount == 5)
            {
                powerUp = PowerUpTypesEnum.Bomb;
            }
            else
                powerUp = PowerUpTypesEnum.ColorBomb;

            return powerUp;
        }

        public static bool CellIsEmpty(ICell cell)
        {
            if (cell == null || cell.CellTypeEnum == CellTypesEnum.Hollow || cell.CurrentGameObject == null)
                return true;

            return false;
        }
        
        public static void MarkCell(ICell cell)
        {
            SpriteRenderer render = cell.CurrentGameObject.GetComponent<SpriteRenderer>();
            render.color = new Color(render.color.r, render.color.g, render.color.b, .2f);
        }

        public static Vector2 SetUITaskPosition(int tasksCount, int currentTask, SceneTypesEnum sceneTypeEnum)
        {
            Vector2 position = Vector3.zero;
            int y = sceneTypeEnum == SceneTypesEnum.Menu ? 0 : -80;

            if (tasksCount == 2)
            {
                int x = sceneTypeEnum == SceneTypesEnum.Menu ? 80 : 60;
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
                int x = sceneTypeEnum == SceneTypesEnum.Menu ? 140 : 90;
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
