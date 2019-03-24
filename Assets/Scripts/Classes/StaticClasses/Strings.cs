using System.Collections.Generic;
using Mathc3Project.Enums;

namespace Mathc3Project.Classes.StaticClasses
{
    public static class Strings
    {
        private static IDictionary<SceneTypesEnum, string> ScenesPath;

        static Strings()
        {
            ScenesPath = new Dictionary<SceneTypesEnum, string>();
            ScenesPathInit();
        }

        private static void ScenesPathInit()
        {
            ScenesPath.Add(SceneTypesEnum.BootstrapperScene, "Scenes/Bootstrapper Scene");
            ScenesPath.Add(SceneTypesEnum.StartScreenScene, "Scenes/Start Screen Scene");
            ScenesPath.Add(SceneTypesEnum.GameplayLevel, "Scenes/Gameplay Level");
            ScenesPath.Add(SceneTypesEnum.Menu, "Scenes/Menu");
        }

        public static string GetScenePath(SceneTypesEnum sceneTypeEnum)
        {
            return ScenesPath[sceneTypeEnum];
        }
        
        /// <summary>
        /// Numbers
        /// </summary>
        public const float SWIPE_SENSITIVITY = 0.1f;

        public const float POSITION_DELTA = 0.1f;
        public const float CELL_SPEED = 9.81f;

        public const int MAX_LIMIT_CHECK_COUNTER = 5;

        public const float TIME_AFTER_MARK = 0.1f;
        public const float TIME_AFTER_DESTROY = 0.02f;
        public const float TIME_AFTER_DECREASE = 0.02f;
        public const float TIME_BETWEEN_SPAWN = 0.1f;


        /// <summary>
        /// Strings
        /// </summary>
        public const string UPDATE_MANAGER = "Update Manager";
        public const string LOGIC_MANAGER = "Logic Manager";
        public const string COROUTINER = "Coroutiner";

        public const string EVENT_NOT_FOUND = "EVENT NOT FOUND";

        public const string BASE_SCENE_OBJECT = "Base Scene Object";

        public const string HOLLOW_CELL = "This cell is HOLLOW: ";
        public const string NORMAL_CELL = "This cell is NORMAL: ";

        public const string GAMEPLAY_ELEMENTS = "Prefabs/Gameplay/Elements/";
        public const string POWERUP_ELEMENTS = "Prefabs/Gameplay/Powers/";
        public const string SPRITE_ELEMENTS = "Sprites/Gameplay/Elements/";

        public const string TAG_BLUEMULTIANGLE = "Blue MultiAngle";
        public const string TAG_GREENDOWNTIRANGLE = "Green DownTriangle";
        public const string TAG_ORANGEBOX = "Orange Box";
        public const string TAG_REDCIRCLE = "Red Circle";
        public const string TAG_YELLOWUPTRIANGLE = "Yellow UpTriangle";
        
        public const string TAG_POWER = "Power";
        public const string TAG_HORIZONTAL = "Horizontal";
        public const string TAG_VERICAL = "Vertical";
        public const string TAG_BOMB = "Bomb";
        public const string TAG_COLORBOMB = "ColorBomb";

    }
}
