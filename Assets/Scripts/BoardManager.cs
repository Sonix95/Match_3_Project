using System;
using System.Collections;
using UnityEngine;

namespace Mathc3Project
{
    public class BoardManager : MonoBehaviour, IBoardManager
    {
        public int _rows;
        public int _columns;
        public GameObject[,] _cells;

        public int Rows { get { return _rows; } set { _rows = value; } }
        public int Columns { get { return _columns; } set { _columns = value; } }
        public GameObject[,] Cells { get { return _cells; } set { _cells = value; } }

        public BoardManager(int rows, int columns)
        {
            SetBoard(rows, columns);
        }

        public void SetBoard(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            _cells = new GameObject[rows, columns];
        }

        public void ClearBoard()
        {
            SetBoard(0, 0);
        }



    }
}