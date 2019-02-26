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
        private ISpawnManager _spawnManager;
        private ICheckManager _checkManager;
        private ICell[,] _cells;
       
        private TileType[] _boardLayout;
        
        private bool[,] _hollowCells;
        private BackgroundTile[,] _breakableCells;
        
        public Board(int width, int height, ISpawnManager spawnManager,ICheckManager checkManager)
        {
            _width = width;
            _height = height;
            _spawnManager = spawnManager;
            _checkManager = checkManager;
            _cells = new Cell[_width, _height];
            
            _hollowCells = new bool[_width, _height];
            _breakableCells = new BackgroundTile[_width, _height];

            _checkManager.Board = this;

            SetCells();
            Initial();
        }
        
        //TODO create method to place a some diffirent cell
        public void SetCells()
        {
            _boardLayout = new TileType[5];
            _boardLayout[4] = new TileType(2, 2, CellTypes.Breakable);

            GenerateHollowCellAt(4, 3);
            GenerateHollowCellAt(5, 3);
            GenerateHollowCellAt(4, 4);
            GenerateHollowCellAt(5, 4);
        }

        private void GenerateHollowCellAt(int x, int y)
        {
            _cells[x,y] =  _spawnManager.GenerateHollowCell(new Vector3(x,y,0));
        }

        private void Initial()
        {
            GameObject boardParent = new GameObject("Board");

            for (int i = 0; i < _width; i++)
            for (int j = 0; j < _height; j++)
            {
                if (_cells[i, j] == null)
                {
                    Vector3 tempPos = new Vector3(i, j, 0f);

                    _spawnManager.GenerateBackTile(tempPos, boardParent);
                    
                    if (_cells[i, j] != null)
                    {
                        continue;
                    }

                    ICell newCell = _spawnManager.GenerateNormalCell(tempPos);

                    int maxLimit = 0;
                    while (_checkManager.SimpleCheck(newCell) && maxLimit < 2)
                    {
                        GameObject.Destroy(newCell.CurrentGameObject);
                        newCell = _spawnManager.GenerateNormalCell(tempPos);

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
        
        public ICheckManager CheckManager
        {
            get { return _checkManager; }
            set { _checkManager = value; }
        }

        public bool[,] HollowCells
        {
            get { return _hollowCells; }
            set { _hollowCells = value; }
        }
        
        public BackgroundTile[,] BreakableCells
        {
            get { return _breakableCells; }
            set { _breakableCells = value; }
        }
    }
}
