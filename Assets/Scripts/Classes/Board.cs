using UnityEngine;
using Mathc3Project.Interfaces;

namespace Mathc3Project
{
    public class Board : IBoard
    {
        private int _width;
        private int _height;
        private ICell[,] _cells;

        public Board(int width, int height)
        {
            _width = width;
            _height = height;
            _cells = new Cell[_width, _height];

            Initial();
        }

        private void Initial()
        {
            var backCellsMain = new GameObject("Back");
            
            Object[] objTempArray = Resources.LoadAll("Prefabs/Elements/");
            GameObject[] prefArray = new GameObject[objTempArray.Length];
            for (int i = 0; i < objTempArray.Length; i++)
                prefArray[i] = objTempArray[i] as GameObject;
        
            for (int i = 0; i < _width; i++)
            for (int j = 0; j < _height; j++)
            {
                Vector3 tempPos = new Vector3(i, j, 0f);

                var pref = Resources.Load("Prefabs/Empty") as GameObject;
                GameObject backTile = Object.Instantiate(pref, tempPos + Vector3.forward, Quaternion.identity);
                backTile.name = "(" + tempPos.x + ", " + tempPos.y + ")";
                backTile.transform.parent = backCellsMain.transform;

                int cellIndex = Random.Range(0, prefArray.Length);
                pref = Resources.Load("Prefabs/Elements/" + prefArray[cellIndex].name.ToString()) as GameObject;
                ICell cell = Object.Instantiate(pref, tempPos, Quaternion.identity).AddComponent<Cell>();
                
                _cells[i, j] = cell;
            }
        }

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }
        public ICell[,] Cells
        {
            get { return _cells; }
            set { _cells = value; }
        }


    }
}
