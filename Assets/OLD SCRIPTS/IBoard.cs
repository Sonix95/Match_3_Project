using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mathc3Project.OLD
{
    public interface IBoard
    {
        GameObject[,] Cells { get; set; }
        int Rows { get; set; }
        int Columns { get; set; }
        bool FillFakeElements { get; set; }

        void InitBoard();
    }
}
