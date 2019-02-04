using System;
using System.Collections;
using UnityEngine;
using Mathc3Project.Interfaces;

namespace Mathc3Project
{
    public class Board : IBoard
    {
        private int _rowCount;
        private int _columnCount;
        private IGameElement[,] _cells;

        public int RowCount { get { return _rowCount; } set { _rowCount = value; } }
        public int ColumnCount { get { return _columnCount; } set { _columnCount = value; } }
        public IGameElement[,] Cells { get { return _cells; } set { _cells = value; } }

        public Board(int rows, int columns)
        {
            _rowCount = rows;
            _columnCount = columns;
            _cells = new IGameElement[_rowCount, _columnCount];
        }


    }
}
