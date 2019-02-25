using Mathc3Project.Enums;

namespace Mathc3Project
{
    public class TileType
    {
        private int _x;
        private int _y;
        private CellType _cellType;

        public TileType(int x, int y, CellType cellType)
        {
            _x = x;
            _y = y;
            _cellType = cellType;
        }
        
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }
        
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
        
        public CellType CellType
        {
            get { return _cellType; }
            set { _cellType = value; }
        }
        
    }
}