using UnityEngine;

namespace Mathc3Project
{
    public interface IBoardManager
    {
        int Rows { get; set; }
        int Columns { get; set; }
        GameObject[,] Cells { get; set; }

        void SetBoard(int rows, int columns);
    }
}