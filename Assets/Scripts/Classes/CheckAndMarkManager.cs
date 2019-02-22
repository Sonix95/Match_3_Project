using System.Collections.Generic;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using UnityEngine;

namespace Mathc3Project
{
    public static class CheckAndMarkManager
    {
        public static void CheckCell(ICell cell, IBoard board)
        {
            CheckLine(LineDirectionType.Vertical, cell, board);
            CheckLine(LineDirectionType.Horizontal, cell, board);
        }

        private static void CheckLine(LineDirectionType lineDirection, ICell cell, IBoard board)
        {
            IList<ICell> sideAList = new List<ICell>();
            IList<ICell> sideBList = new List<ICell>();

            int column = cell.TargetX;
            int row = cell.TargetY;

            int boardLimit;
            int axis;

            ICell sideCell = null;

            if (lineDirection == LineDirectionType.Horizontal)
            {
                boardLimit = board.Width;
                axis = column;
            }
            else
            {
                boardLimit = board.Height;
                axis = row;
            }

            if (axis > 0 && axis < boardLimit)
            {
                for (int i = axis - 1; i >= 0; i--)
                {
                    sideCell = (lineDirection == LineDirectionType.Horizontal)
                        ? board.Cells[i, row]
                        : board.Cells[column, i];

                    if (sideCell == null)
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
                        ? board.Cells[i, row]
                        : board.Cells[column, i];

                    if (sideCell == null)
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

        public static void MarkCell(ICell cell)
        {
            if (cell != null && cell.IsMatched)
            {
                SpriteRenderer render = cell.CurrentGameObject.GetComponent<SpriteRenderer>();
                render.color = new Color(render.color.r, render.color.g, render.color.b, .2f);
            }
        }
        
    }
}
