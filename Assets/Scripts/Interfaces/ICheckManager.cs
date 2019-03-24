using System.Collections.Generic;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces.Cells;
using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface ICheckManager
    {
        IBoard Board { get; set; }

        bool SimpleCheck(ICell cell);
        bool HaveMatch(ICell cell);
        IList<ICell> CheckCell(ICell cell, out AxisTypesEnum majorAxis);
        IList<ICell> PowerCheck(PowerUpTypesEnum powerUpTypeEnum, Vector2 position);

    }
}
