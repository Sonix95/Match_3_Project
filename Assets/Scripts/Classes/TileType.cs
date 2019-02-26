using Mathc3Project.Enums;

namespace Mathc3Project
{
    public class TileType
    {
        private int _x;
        private int _y;
        private CellTypes _cellTypes;

        public TileType(int x, int y, CellTypes cellTypes)
        {
            _x = x;
            _y = y;
            _cellTypes = cellTypes;
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
        
        public CellTypes CellTypes
        {
            get { return _cellTypes; }
            set { _cellTypes = value; }
        }
        
    }
}