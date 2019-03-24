using Mathc3Project.Interfaces;

namespace Mathc3Project.Classes
{
    public class Level : ILevel
    {
        private int _locationID;
        private int _levelID;
        private ILevelTask[] _levelTasks;
        private int _boardWidth;
        private int _boardHeight;

        public Level(int levelID, int boardWidth, int boardHeight,ILevelTask[] levelTasks)
        {
            _locationID = 1; 
            _levelID = levelID;
            _levelTasks = levelTasks;
            _boardWidth = boardWidth;
            _boardHeight = boardHeight;
        }

        public override string ToString()
        {
            string message = "Level " + _locationID + "-" + _levelID + "\n";
            message += "Board = " + _boardWidth + "x" + _boardHeight + "\n";
            
            return message;
        }
        
        public int LocationId
        {
            get { return _locationID; }
            set { _locationID = value; }
        }

        public int LevelId
        {
            get { return _levelID; }
            set { _levelID = value; }
        }

        public ILevelTask[] LevelTasks
        {
            get { return _levelTasks; }
            set { _levelTasks = value; }
        }
        
        public int BoardWidth
        {
            get { return _boardWidth; }
            set { _boardWidth = value; }
        }

        public int BoardHeight
        {
            get { return _boardHeight; }
            set { _boardHeight = value; }
        }
        
    }
}
