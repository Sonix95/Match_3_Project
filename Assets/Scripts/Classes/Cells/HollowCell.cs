using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces.Cells;
using UnityEngine;

namespace Mathc3Project.Classes.Cells
{
    public class HollowCell : ICell
    {
        private int _x;
        private int _y;
        private CellTypes _cellType;
        private CellStates _cellStates;
        private GameObject _currentGameObject;

        public HollowCell(int x, int y)
        {
            _x = x;
            _y = y;
            _cellType = CellTypes.Hollow;
            _cellStates = CellStates.Lock;
        }

        public void Move()
        {
            // -- no implementation --
        }

        public void DoAfterMatch()
        {
            // -- no implementation --
        }

        public override string ToString()
        {
            string message = MagicStrings.Hollow_Cell + _x + "x" + _y;
            return message;
        }

        public int TargetX
        {
            get { return _x; }
            set { _x = value; }
        }

        public int TargetY
        {
            get { return _y; }
            set { _y = value; }
        }

        public CellTypes CellType
        {
            get { return _cellType; }
            set { _cellType = value; }
        }

        public CellStates CellState
        {
            get { return _cellStates; }
            set { _cellStates = value; }
        }

        public GameObject CurrentGameObject
        {
            get { return _currentGameObject; }
            set { _currentGameObject = value; }
        }
    }
}
