namespace Mathc3Project.Classes.StaticClasses
{
    public static class MagicStrings
    {
        // ============= ||||||| ============= \\
        // ============= NUMBERS ============= \\
        // ============= ||||||| ============= \\

        // ------------- FOR LOGIC MANAGER ------------- 
        public const float SWIPE_SENSITIVITY = 0.1f;

        // ------------- FOR CELLS ------------- 
        public const float POSITION_DELTA = 0.1f;
        public const float CELL_SPEED = 9.81f;

        // ------------- FOR BOARD ------------- 
        public const int MAX_LIMIT_CHECK_COUNTER = 10;

        // ------------- FOR LOGIC MANAGER ------------- 
        public const float TIME_AFTER_MARK = 0.1f;
        public const float TIME_AFTER_DESTROY = 0.02f;
        public const float TIME_AFTER_DECREASE = 0.02f;


        // ============= ||||||| ============= \\
        // ============= STRINGS ============= \\
        // ============= ||||||| ============= \\

        // ------------- FOR INITIAL MANAGER ------------- 
        public const string Update_Manager = "Update Manager";
        public const string Logic_Manager = "Logic Manager";

        // ------------- FOR ON EVENT ------------- 
        public const string Event_Not_Found = "EVENT NOT FOUND";

        // ------------- FOR CELLS ------------- 
        public const string Hollow_Cell = "This cell is HOLLOW: ";
        public const string Normal_Cell = "This cell is NORMAL: ";

        // ------------- FOR OBJECT STORAGE ------------- 
        public const string Gameplay_Elements = "Prefabs/Gameplay/Elements";
        public const string Power_Horizontal = "Prefabs/Gameplay/Powers/Horizontal";
        public const string Power_Vertical = "Prefabs/Gameplay/Powers/Vertical";
        public const string Power_Bomb = "Prefabs/Gameplay/Powers/Bomb";
        public const string Power_ColorBomb = "Prefabs/Gameplay/Powers/ColorBomb";

        // ------------- FOR POWERS ------------- 
        public const string Tag_Power = "Power";
        public const string Tag_Horizontal = "Horizontal";
        public const string Tag_Vertical = "Vertical";
        public const string Tag_Bomb = "Bomb";

    }
}
