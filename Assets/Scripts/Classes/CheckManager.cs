using System.Collections.Generic;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Cells;
using UnityEngine;

namespace Mathc3Project.Classes
{
    public class CheckManager : ICheckManager
    {
        private IBoard _board;

        public bool SimpleCheck(ICell cell)
        {
            if (cell.CellType == CellTypes.Hollow)
                return false;

            int x = cell.TargetX;
            int y = cell.TargetY;

            if (y > 1 && x > 1)
            {
                if (!Helper.CellIsEmpty(_board.Cells[x, y - 1]) &&
                    !Helper.CellIsEmpty(_board.Cells[x, y - 2]) &&
                    !Helper.CellIsEmpty(_board.Cells[x - 1, y]) &&
                    !Helper.CellIsEmpty(_board.Cells[x - 2, y]))
                {
                    ICell[] sideCells =
                    {
                        _board.Cells[x, y - 1],
                        _board.Cells[x, y - 2],
                        _board.Cells[x - 1, y],
                        _board.Cells[x - 2, y]
                    };

                    foreach (var sideCell in sideCells)
                    {
                        if (sideCell.CurrentGameObject.CompareTag(cell.CurrentGameObject.tag))
                            return true;
                    }
                }
            }
            else if (y <= 1 || x <= 1)
            {
                if (y > 1)
                {
                    if (!Helper.CellIsEmpty(_board.Cells[x, y - 1]) &&
                        !Helper.CellIsEmpty(_board.Cells[x, y - 2]))
                    {
                        ICell[] sideHorizontalCells =
                        {
                            _board.Cells[x, y - 1],
                            _board.Cells[x, y - 2]
                        };

                        foreach (var sideCell in sideHorizontalCells)
                        {
                            if (sideCell.CurrentGameObject.CompareTag(cell.CurrentGameObject.tag))
                                return true;
                        }
                    }
                }

                if (x > 1)
                {
                    if (!Helper.CellIsEmpty(_board.Cells[x - 1, y]) &&
                        !Helper.CellIsEmpty(_board.Cells[x - 2, y]))
                    {
                        ICell[] sideVerticalCells =
                        {
                            _board.Cells[x - 1, y],
                            _board.Cells[x - 2, y]
                        };

                        foreach (var sideCell in sideVerticalCells)
                        {
                            if (sideCell.CurrentGameObject.CompareTag(cell.CurrentGameObject.tag))
                                return true;
                        }
                    }
                }
            }

            return false;
        }

        private IList<ICell> CheckLine(AxisTypes axisType, ICell cell)
        {
            if (Helper.CellIsEmpty(cell))
                return null;

            IList<ICell> sideCells = new List<ICell>();

            int x = cell.TargetX;
            int y = cell.TargetY;

            int boardLimit;
            int axis;

            ICell sideCell = null;

            if (axisType == AxisTypes.Horizontal)
            {
                boardLimit = _board.Width;
                axis = x;
            }
            else
            {
                boardLimit = _board.Height;
                axis = y;
            }

            if (axis > 0 && axis < boardLimit)
            {
                for (int i = axis - 1; i >= 0; i--)
                {
                    sideCell = (axisType == AxisTypes.Horizontal)
                        ? _board.Cells[i, y]
                        : _board.Cells[x, i];

                    if (Helper.CellIsEmpty(sideCell) || sideCell.CurrentGameObject.CompareTag(Strings.Tag_Power))
                        break;
                    if (sideCell.CurrentGameObject.CompareTag(cell.CurrentGameObject.tag))
                        sideCells.Add(sideCell);
                    else
                        break;
                }
            }

            if (axis >= 0 && axis < boardLimit)
            {
                for (int i = axis + 1; i < boardLimit; i++)
                {
                    sideCell = (axisType == AxisTypes.Horizontal)
                        ? _board.Cells[i, y]
                        : _board.Cells[x, i];

                    if (Helper.CellIsEmpty(sideCell) || sideCell.CurrentGameObject.CompareTag(Strings.Tag_Power))
                        break;
                    if (sideCell.CurrentGameObject.CompareTag(cell.CurrentGameObject.tag))
                        sideCells.Add(sideCell);
                    else
                        break;
                }
            }

            return sideCells;
        }

        public bool HaveMatch(ICell cell)
        {
            IList<ICell> horizontalCellsList = CheckLine(AxisTypes.Horizontal, cell);
            IList<ICell> verticalCellsList = CheckLine(AxisTypes.Vertical, cell);

            if (horizontalCellsList.Count > 1 || verticalCellsList.Count > 1)
                return true;

            return false;
        }

        public IList<ICell> CheckCell(ICell cell, out AxisTypes majorAxis)
        {
            IList<ICell> allCellList = new List<ICell>();

            IList<ICell> horizontalCellsList = CheckLine(AxisTypes.Horizontal, cell);
            IList<ICell> verticalCellsList = CheckLine(AxisTypes.Vertical, cell);

            if (horizontalCellsList.Count > 1)
                foreach (var horCell in horizontalCellsList)
                        allCellList.Add(horCell);

            if (verticalCellsList.Count > 1)
                foreach (var vertCell in verticalCellsList)
                        allCellList.Add(vertCell);

            if (allCellList.Count > 0)
                allCellList.Add(cell);

            if (horizontalCellsList.Count > verticalCellsList.Count)
                majorAxis = AxisTypes.Horizontal;
            else if (horizontalCellsList.Count < verticalCellsList.Count)
                majorAxis = AxisTypes.Vertical;
            else
                majorAxis = AxisTypes.Undefined;

            foreach (var oneCell in allCellList)
                if (oneCell.CellState != CellStates.Lock)
                    oneCell.CellState = CellStates.Check;
            
            return allCellList;
        }

        #region Powers

        public IList<ICell> PowerCheck(PowerUpTypes powerUpType, Vector2 position)
        {
            IList<ICell> checkedCells = new List<ICell>();

            int posX = (int) position.x;
            int posY = (int) position.y;

            switch (powerUpType)
            {
                case PowerUpTypes.Horizontal:
                    checkedCells = HorizontalPower(posY);
                    break;
                case PowerUpTypes.Vertical:
                    checkedCells = VerticalPower(posX);
                    break;
                case PowerUpTypes.Bomb:
                    checkedCells = BombPower(posX, posY);
                    break;
                case PowerUpTypes.ColorBomb:
                    checkedCells = RandomColorBombPower();
                    break;

                default:
                    checkedCells.Add(_board.Cells[posX, posY]);
                    break;
            }

            return checkedCells;
        }

        private IList<ICell> RandomColorBombPower()
        {
            IList<ICell> checkedCells = new List<ICell>();

            int randomX = Random.Range(0, _board.Width-1);
            int randomY = Random.Range(0, _board.Height-1);

            while (Helper.CellIsEmpty(_board.Cells[randomX, randomY]))
            {
                randomX = Random.Range(0, _board.Width);
                randomY = Random.Range(0, _board.Height);
            }

            string cellTag = _board.Cells[randomX, randomY].CurrentGameObject.tag;

            foreach (var cell in _board.Cells)
                if (Helper.CellIsEmpty(cell) == false && cell.CurrentGameObject.CompareTag(cellTag))
                    checkedCells.Add(cell);

            return checkedCells;
        }

        private IList<ICell> HorizontalPower(int positionY)
        {
            IList<ICell> checkedCells = new List<ICell>();

            foreach (var cell in _board.Cells)
                if (cell.CellType != CellTypes.Hollow && cell.TargetY == positionY)
                    checkedCells.Add(cell);

            return checkedCells;
        }

        private IList<ICell> VerticalPower(int positionX)
        {
            IList<ICell> checkedCells = new List<ICell>();

            foreach (var cell in _board.Cells)
                if (cell.CellType != CellTypes.Hollow && cell.TargetX == positionX)
                    checkedCells.Add(cell);

            return checkedCells;
        }

        private IList<ICell> BombPower(int posX, int posY)
        {
            IList<ICell> checkedCells = new List<ICell>();

            foreach (var cell in _board.Cells)
                if (cell.CellType != CellTypes.Hollow)
                {
                    int x = Mathf.Abs(cell.TargetX - posX);
                    int y = Mathf.Abs(cell.TargetY - posY);

                    if (x < 3 && x > 0 && y < 2)
                        checkedCells.Add(cell);
                    else if (y < 3 && y > 0 && x < 2)
                        checkedCells.Add(cell);
                    else if (x == 1 && y == 1 || x == 1 && y == 0 || x == 0 && y == 1)
                        checkedCells.Add(cell);
                    else if (x == 0 && y == 0)
                        checkedCells.Add(cell);
                }

            return checkedCells;
        }

        #endregion

        public IBoard Board
        {
            get { return _board; }
            set { _board = value; }
        }

    }
}
