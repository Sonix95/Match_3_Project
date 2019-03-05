using System.Collections.Generic;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces.Cells;
using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface ICheckManager
    {
        IBoard Board { get; set; }IList<ICell> CheckCell(ICell cell);
        IList<ICell> PowerCheck(PowerTypes powerType, Vector2 position);
        IList<ICell> CheckCell(ICell cell, out LineDirectionTypes highedDirection);
        bool JustCheckCell(ICell cell);
        bool JustCheckCell(ICell cell, out IList<ICell> allCellList);
        bool JustCheckCell(ICell cell, out IList<ICell> allCellList, out LineDirectionTypes highedDirection);
        bool HaveMatch(ICell cell);
        bool SimpleCheck(ICell cell);
        bool CellIsEmpty(ICell cell);
    }
}
