using Mathc3Project.Enums;
using UnityEngine;

namespace Mathc3Project.Interfaces.Cells
{
    public interface ICell
    {
        int TargetX { get; set; }
        int TargetY { get; set; }
        CellTypes CellType { get; set; }
        CellStates CellState { get; set; }
        GameObject CurrentGameObject { get; set; }

        void Move();
        void DoAfterMatch();
    }
}
