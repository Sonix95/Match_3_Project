using System.Collections.Generic;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Cells;
using UnityEngine;

namespace Mathc3Project.Classes
{
    public class CheckManager : ICheckManager
    {
        private IBoard _board;
        
        public bool HaveMatch(ICell cell)
        {
            IList<ICell> horizontalCellsList = CheckLine(LineDirectionTypes.Horizontal, cell);
            IList<ICell> verticalCellsList = CheckLine(LineDirectionTypes.Vertical, cell);

            if (horizontalCellsList.Count + verticalCellsList.Count > 2)
                return true;

            return false;
        }

        #region Powers
        
        public IList<ICell> PowerCheck(PowerTypes powerType, Vector2 position)
        {
            IList<ICell> checkedCells = new List<ICell>();

            int posX = (int) position.x;
            int posY = (int) position.y;
            
            switch (powerType)
            {
                case PowerTypes.Horizontal:
                    checkedCells = HorizontalPower(posY);
                    break;
                case PowerTypes.Vertical:
                    checkedCells = VerticalPower(posX);
                    break;
                case PowerTypes.Bomb:
                    checkedCells = BombPower(posX,posY);
                    break;
                
                default:
                    checkedCells.Add(_board.Cells[posX, posY]);
                    break;
            }

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

        #region bool JustCheckCell(...
        
        public bool JustCheckCell(ICell cell)
        {
            IList<ICell> allCellList = CheckCell(cell);

            if (allCellList.Count > 0)
            {
                foreach (var oneCell in allCellList)
                    oneCell.CellState = CellStates.Check;

                return true;
            }
            
            return false;
        }
        
        public bool JustCheckCell(ICell cell, out IList<ICell> allCellList)
        {
            allCellList = CheckCell(cell);

            if (allCellList.Count > 0)
            {
                foreach (var oneCell in allCellList)
                    oneCell.CellState = CellStates.Check;

                return true;
            }
            
            return false;
        }
        
        public bool JustCheckCell(ICell cell, out IList<ICell> allCellList, out LineDirectionTypes highedDirection)
        {
            allCellList = CheckCell(cell, out highedDirection);

            if (allCellList.Count > 0)
            {
                foreach (var oneCell in allCellList)
                    oneCell.CellState = CellStates.Check;

                return true;
            }
            
            return false;
        }
        
        #endregion

        #region IList<ICell> CheckCell(...
        
        public IList<ICell> CheckCell(ICell cell)
        {
            IList<ICell> allCellList = new List<ICell>();
            
            IList<ICell> horizontalCellsList = CheckLine(LineDirectionTypes.Horizontal, cell);
            IList<ICell> verticalCellsList = CheckLine(LineDirectionTypes.Vertical, cell);
            
            if(horizontalCellsList.Count > 1)
                foreach (var horCell in horizontalCellsList)
                {
                    if(allCellList.Contains(horCell) == false)
                        allCellList.Add(horCell);
                }
            
            if(verticalCellsList.Count > 1)
                foreach (var vertCell in verticalCellsList)
                {
                    if(allCellList.Contains(vertCell) == false)
                        allCellList.Add(vertCell);
                }
            
            if(allCellList.Count > 1)
                allCellList.Add(cell);

            return allCellList;
        }
        
        public IList<ICell> CheckCell(ICell cell, out  LineDirectionTypes highedDirection)
        {
            IList<ICell> allCellList = new List<ICell>();
            
            IList<ICell> horizontalCellsList = CheckLine(LineDirectionTypes.Horizontal, cell);
            IList<ICell> verticalCellsList = CheckLine(LineDirectionTypes.Vertical, cell);
            
            if(horizontalCellsList.Count > 1)
                foreach (var horCell in horizontalCellsList)
                {
                    if(allCellList.Contains(horCell) == false)
                        allCellList.Add(horCell);
                }
            
            if(verticalCellsList.Count > 1)
                foreach (var vertCell in verticalCellsList)
                {
                    if(allCellList.Contains(vertCell) == false)
                        allCellList.Add(vertCell);
                }

            if (horizontalCellsList.Count > verticalCellsList.Count)
                highedDirection = LineDirectionTypes.Horizontal;
            else if (horizontalCellsList.Count < verticalCellsList.Count)
                highedDirection = LineDirectionTypes.Vertical;
            else
                highedDirection = LineDirectionTypes.Undefined;
            
            if(allCellList.Count > 1)
                allCellList.Add(cell);

            return allCellList;
        }
        
        #endregion

        private IList<ICell> CheckLine(LineDirectionTypes lineDirection, ICell cell)
        {
            if (cell == null || cell.CellType == CellTypes.Hollow || cell.CurrentGameObject == null)
                return null;

            IList<ICell> sideCells = new List<ICell>();

            int column = cell.TargetX;
            int row = cell.TargetY;

            int boardLimit;
            int axis;

            ICell sideCell = null;

            if (lineDirection == LineDirectionTypes.Horizontal)
            {
                boardLimit = _board.Width;
                axis = column;
            }
            else
            {
                boardLimit = _board.Height;
                axis = row;
            }
            
            if (axis > 0 && axis < boardLimit)
            {
                for (int i = axis - 1; i >= 0; i--)
                {
                    sideCell = (lineDirection == LineDirectionTypes.Horizontal)
                        ? _board.Cells[i, row]
                        : _board.Cells[column, i];

                    if (sideCell == null || sideCell.CellType == CellTypes.Hollow || sideCell.CurrentGameObject == null || sideCell.CurrentGameObject.CompareTag("Power"))
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
                    sideCell = (lineDirection == LineDirectionTypes.Horizontal)
                        ? _board.Cells[i, row]
                        : _board.Cells[column, i];

                    if (sideCell == null || sideCell.CellType == CellTypes.Hollow || sideCell.CurrentGameObject == null || sideCell.CurrentGameObject.CompareTag("Power"))
                        break;
                    if (sideCell.CurrentGameObject.CompareTag(cell.CurrentGameObject.tag))
                        sideCells.Add(sideCell);
                    else
                        break;
                }
            }
            
            return sideCells;
        }

        public bool SimpleCheck(ICell cell)
        {
            if (cell.CellType == CellTypes.Hollow)
                return false;
            
            int column = cell.TargetX;
            int row = cell.TargetY;
            
            if (row > 1 && column > 1)
            {
                if(!CellIsEmpty(_board.Cells[column, row - 1]) && 
                   !CellIsEmpty(_board.Cells[column, row - 2]) && 
                   !CellIsEmpty(_board.Cells[column - 1, row]) && 
                   !CellIsEmpty(_board.Cells[column - 2, row]))
                {
                    ICell[] sideCells =
                    {
                        _board.Cells[column, row - 1], _board.Cells[column, row - 2],
                        _board.Cells[column - 1, row], _board.Cells[column - 2, row]
                    };

                    foreach (var sideCell in sideCells)
                    {
                        if (sideCell.CurrentGameObject.CompareTag(cell.CurrentGameObject.tag))
                            return true;
                    }
                }
            }
            else if (row <= 1 || column <= 1)
            {
                if (row > 1)
                {
                    if (!CellIsEmpty(_board.Cells[column, row - 1]) && 
                        !CellIsEmpty(_board.Cells[column, row - 2]))
                    {
                        ICell[] sideHorizontalCells =
                            {_board.Cells[column, row - 1], _board.Cells[column, row - 2]};

                        foreach (var sideCell in sideHorizontalCells)
                        {
                            if (sideCell.CurrentGameObject.CompareTag(cell.CurrentGameObject.tag))
                                return true;
                        }
                    }
                }

                if (column > 1)
                {
                    if (!CellIsEmpty(_board.Cells[column - 1, row]) && 
                        !CellIsEmpty(_board.Cells[column - 2, row]))
                    {
                        ICell[] sideVerticalCells =
                            {_board.Cells[column - 1, row], _board.Cells[column - 2, row]};

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

        public bool CellIsEmpty(ICell cell)
        {
            if (cell == null || cell.CellType == CellTypes.Hollow || cell.CurrentGameObject == null)
                return true;

            return false;
        }

        public IBoard Board
        {
            get { return _board; }
            set { _board = value; }
        }
    }
}