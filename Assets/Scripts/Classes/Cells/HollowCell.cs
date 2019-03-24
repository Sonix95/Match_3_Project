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
        private CellTypesEnum _cellTypeEnum;
        private CellStatesEnum _cellStatesEnum;

        public HollowCell(int x, int y)
        {
            _x = x;
            _y = y;
            _cellTypeEnum = CellTypesEnum.Hollow;
            _cellStatesEnum = CellStatesEnum.Lock;
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
            return Strings.HOLLOW_CELL + _x + "x" + _y;
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

        public CellTypesEnum CellTypeEnum
        {
            get { return _cellTypeEnum; }
            set { _cellTypeEnum = value; }
        }

        public CellStatesEnum CellStateEnum
        {
            get { return _cellStatesEnum; }
            set { _cellStatesEnum = value; }
        }

        public GameObject CurrentGameObject
        {
            get { return null; }
            set { CurrentGameObject = null; }
        }
    }
}
