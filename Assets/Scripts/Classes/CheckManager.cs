using System.Collections.Generic;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using UnityEngine;

namespace Mathc3Project
{
    public class CheckManager : ICheckManager
    {
        private IBoard _board;
        
        public void CheckCell(ICell cell)
        {
            CheckLine(LineDirectionType.Vertical, cell);
            CheckLine(LineDirectionType.Horizontal, cell);
        }

        private void CheckLine(LineDirectionType lineDirection, ICell cell)
        {
            if(cell == null || cell.CellTypes == CellTypes.Hollow)
                return;
            
            IList<ICell> sideAList = new List<ICell>();
            IList<ICell> sideBList = new List<ICell>();

            int column = cell.TargetX;
            int row = cell.TargetY;

            int boardLimit;
            int axis;

            ICell sideCell = null;

            if (lineDirection == LineDirectionType.Horizontal)
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
                    sideCell = (lineDirection == LineDirectionType.Horizontal)
                        ? _board.Cells[i, row]
                        : _board.Cells[column, i];

                    if (sideCell == null || sideCell.CellTypes == CellTypes.Hollow)
                        break;
                    else if (sideCell.CurrentGameObject.CompareTag(cell.CurrentGameObject.tag))
                        sideAList.Add(sideCell);
                    else
                        break;
                }
            }

            if (axis >= 0 && axis < boardLimit)
            {
                for (int i = axis + 1; i < boardLimit; i++)
                {
                    sideCell = (lineDirection == LineDirectionType.Horizontal)
                        ? _board.Cells[i, row]
                        : _board.Cells[column, i];

                    if (sideCell == null || sideCell.CellTypes == CellTypes.Hollow)
                        break;
                    else if (sideCell.CurrentGameObject.CompareTag(cell.CurrentGameObject.tag))
                        sideBList.Add(sideCell);
                    else
                        break;
                }
            }

            if (sideAList.Count + sideBList.Count > 1)
            {
                foreach (var cellInListA in sideAList)
                    cellInListA.IsMatched = true;
                foreach (var cellInListB in sideBList)
                    cellInListB.IsMatched = true;

                cell.IsMatched = true;
            }
        }

        public bool SimpleCheck(ICell cell)
        {
            int column = (int) cell.CurrentGameObject.transform.position.x;
            int row = (int) cell.CurrentGameObject.transform.position.y;

            if (row > 1 && column > 1)
            {
                if (_board.Cells[column, row - 1] != null &&
                    _board.Cells[column, row - 2] != null &&
                    _board.Cells[column - 1, row] != null &&
                    _board.Cells[column - 2, row] != null)
                {
                    ICell[] sideCells =
                    {
                        _board.Cells[column, row - 1], _board.Cells[column, row - 2],
                        _board.Cells[column - 1, row], _board.Cells[column - 2, row]
                    };

                    foreach (var sideCell in sideCells)
                    {
                        if (sideCell.Tag == cell.Tag)
                            return true;
                    }
                }
            }
            else if (row <= 1 || column <= 1)
            {
                if (row > 1)
                {
                    if (_board.Cells[column, row - 1] != null &&
                        _board.Cells[column, row - 2] != null)
                    {
                        ICell[] sideHorizontalCells =
                            {_board.Cells[column, row - 1], _board.Cells[column, row - 2]};

                        foreach (var sideCell in sideHorizontalCells)
                        {
                            if (sideCell.Tag == cell.Tag)
                                return true;
                        }
                    }
                }

                if (column > 1)
                {
                    if (_board.Cells[column - 1, row] != null &&
                        _board.Cells[column - 2, row] != null)
                    {
                        ICell[] sideVerticalCells =
                            {_board.Cells[column - 1, row], _board.Cells[column - 2, row]};

                        foreach (var sideCell in sideVerticalCells)
                        {
                            if (sideCell.Tag == cell.Tag)
                                return true;
                        }
                    }
                }
            }

            return false;
        }

        public IBoard Board
        {
            get { return _board; }
            set { _board = value; }
        }
    }
}