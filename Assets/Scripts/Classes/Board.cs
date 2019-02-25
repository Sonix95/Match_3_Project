using System;
using Mathc3Project.Enums;
using UnityEngine;
using Mathc3Project.Interfaces;

namespace Mathc3Project
{
    public class Board : IBoard
    {
        private int _width;
        private int _height;
        private ICell[,] _cells;
        private ISpawnManager _spawnManager;
       
        private TileType[] _boardLayout;
        private bool[,] _hollowCells;
        
        public Board(int width, int height, ISpawnManager spawnManager)
        {
            _width = width;
            _height = height;
            _cells = new Cell[_width, _height];
            _hollowCells = new bool[_width, _height];
            _spawnManager = spawnManager;

            SetCells();
            Initial();
        }
        
        //TODO create method to place a some diffirent cell
        public void SetCells()
        {
            _boardLayout = new TileType[6];
            
            _boardLayout[0] = new TileType(1, 3, CellType.Hollow);
            _boardLayout[1] = new TileType(10, 3, CellType.Hollow);

            _boardLayout[2] = new TileType(5, 4, CellType.Hollow);
            _boardLayout[3] = new TileType(6, 4, CellType.Hollow);
            _boardLayout[4] = new TileType(5, 5, CellType.Hollow);
            _boardLayout[5] = new TileType(6, 5, CellType.Hollow);
        }

        private void GenerateBlankSpaces()
        {
            for (int i = 0; i < _boardLayout.Length; i++)
                if (_boardLayout[i].CellType == CellType.Hollow)
                    _hollowCells[_boardLayout[i].X, _boardLayout[i].Y] = true;
        }

        private void Initial()
        {
            GenerateBlankSpaces();
            GameObject boardParent = new GameObject("Board");

            for (int i = 0; i < _width; i++)
            for (int j = 0; j < _height; j++)
            {
                if (!_hollowCells[i, j])
                {
                    Vector3 tempPos = new Vector3(i, j, 0f);

                    _spawnManager.GenerateBackTile(tempPos, boardParent);

                    if (_cells[i, j] != null)
                    {
                        continue;
                    }

                    ICell newCell = _spawnManager.GenerateRandomGameElement(tempPos);

                    int maxLimit = 0;
                    while (CheckAndMarkManager.SimpleCheck(newCell, this) && maxLimit < 3)
                    {
                        GameObject.Destroy(newCell.CurrentGameObject);
                        newCell = _spawnManager.GenerateRandomGameElement(tempPos);

                        maxLimit++;
                    }

                    maxLimit = 0;

                    _cells[i, j] = newCell;
                }
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
        
        public ISpawnManager SpawnManager
        {
            get { return _spawnManager; }
            set { _spawnManager = value; }
        }
        
        public bool[,] HollowCells
        {
            get { return _hollowCells; }
            set { _hollowCells = value; }
        }
    }
}
