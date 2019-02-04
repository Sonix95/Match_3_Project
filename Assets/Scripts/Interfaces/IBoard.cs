using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface IBoard
    {
        int RowCount { get; set; }
        int ColumnCount { get; set; }
        IGameElement[,] Cells { get; set; }        
    }
}
