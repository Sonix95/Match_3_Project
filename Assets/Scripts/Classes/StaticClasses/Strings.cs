using System.Collections.Generic;
using DefaultNamespace;
using Mathc3Project.Enums;

namespace Mathc3Project.Classes.StaticClasses
{
    public static class Strings
    {
        private static IDictionary<SceneTypes, string> ScenesPath;

        static Strings()
        {
            ScenesPath = new Dictionary<SceneTypes, string>();
            ScenesPathInit();
        }

        private static void ScenesPathInit()
        {
            ScenesPath.Add(SceneTypes.BootstrapperScene, "Scenes/Bootstrapper Scene");
            ScenesPath.Add(SceneTypes.StartScreenScene, "Scenes/Start Screen Scene");
            ScenesPath.Add(SceneTypes.GameplayLevel, "Scenes/Gameplay Level");
            ScenesPath.Add(SceneTypes.Menu, "Scenes/Menu");
        }

        public static string GetScenePath(SceneTypes sceneType)
        {
            return ScenesPath[sceneType];
        }
        

        // ===========================================================================================
        
        
        // ============= ||||||| ============= \\
        // ============= NUMBERS ============= \\
        // ============= ||||||| ============= \\

        // ------------- FOR LOGIC MANAGER ------------- 
        public const float SWIPE_SENSITIVITY = 0.1f;

        // ------------- FOR CELLS ------------- 
        public const float POSITION_DELTA = 0.1f;
        public const float CELL_SPEED = 9.81f;

        // ------------- FOR BOARD ------------- 
        public const int MAX_LIMIT_CHECK_COUNTER = 5;

        // ------------- FOR LOGIC MANAGER ------------- 
        public const float TIME_AFTER_MARK = 0.1f;
        public const float TIME_AFTER_DESTROY = 0.02f;
        public const float TIME_AFTER_DECREASE = 0.02f;
        public const float TIME_BETWEEN_SPAWN = 0.1f;


        // ===========================================================================================
        
        
        // ============= ||||||| ============= \\
        // ============= STRINGS ============= \\
        // ============= ||||||| ============= \\

        // ------------- FOR INITIAL MANAGER ------------- 
        public const string Update_Manager = "Update Manager";
        public const string Logic_Manager = "Logic Manager";
        public const string Coroutiner = "Coroutiner";

        // ------------- FOR ON EVENT ------------- 
        public const string Event_Not_Found = "EVENT NOT FOUND";

        // ------------- FOR CELLS ------------- 
        public const string Hollow_Cell = "This cell is HOLLOW: ";
        public const string Normal_Cell = "This cell is NORMAL: ";

        // ------------- FOR OBJECT STORAGE ------------- 
        public const string Gameplay_Elements = "Prefabs/Gameplay/Elements/";
        public const string Power_Element = "Prefabs/Gameplay/Powers/";
        public const string Sprites_Elements = "Sprites/Gameplay/Elements/";

        // ------------- FOR TAG ELEMENTS ------------- 
        public const string Tag_BlueMultiAngle = "Blue MultiAngle";
        public const string Tag_GreenDownTriangle = "Green DownTriangle";
        public const string Tag_OrangeBox = "Orange Box";
        public const string Tag_RedCircle = "Red Circle";
        public const string Tag_YellowUpTriangle = "Yellow UpTriangle";
        
        // ------------- FOR POWERS ------------- 
        public const string Tag_Power = "Power";
        public const string Tag_Horizontal = "Horizontal";
        public const string Tag_Vertical = "Vertical";
        public const string Tag_Bomb = "Bomb";
        public const string Tag_ColorBomb = "ColorBomb";

    }
}
