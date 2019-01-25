using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mathc3Project
{
    public class Board : MonoBehaviour
    {
        public GameObject[,] cells;

        private void Start()
        {
            InitBoard();
        }
        IBorderGenerator board;
        void InitBoard()
        {
             board = FindObjectOfType<BoardGenerator>();
            cells = new GameObject[board.BoardColumnCount, board.BoardRowCount];
            
                
        }
        private void Update()
        {
            for (int i = 0; i < board.BoardColumnCount; i++)
                for (int j = 0; j < board.BoardRowCount; j++)
                    
                Debug.Log((cells[i,j] == null) ? "NULL" : cells[i, j].ToString() + " Cell " + i + "x" + "j" );
        }
    }
}
