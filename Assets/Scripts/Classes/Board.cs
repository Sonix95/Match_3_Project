using Mathc3Project.Classes.Cells;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Cells;
using UnityEngine;

namespace Mathc3Project.Classes
{
    public class Board : IBoard
    {
        private int _width;
        private int _height;
        
        private ISpawnManager _spawnManager;
        private ICheckManager _checkManager;
        
        private ICell[,] _cells;
        
        public Board(int width, int height, ISpawnManager spawnManager,ICheckManager checkManager)
        {
            _width = width;
            _height = height;
            _spawnManager = spawnManager;
            _checkManager = checkManager;
            _cells = new ICell[_width, _height];

            _checkManager.Board = this;

            PreSettings();
            Initial();
        }
        
        private void PreSettings()
        {
            GenerateHollowCell(2, 2);
            GenerateHollowCell(2, 3);
        }

        private void GenerateHollowCell(int x, int y)
        {
            _cells[x,y] = new HollowCell(x,y);
        }

        private void Initial()
        {
            for (int i = 0; i < _width; i++)
            for (int j = 0; j < _height; j++)
            {
                if (_cells[i, j] == null)
                {
                    Vector2 tempPos = new Vector2(i, j);

                    ICell newCell = _spawnManager.SpawnNormalCell(tempPos);
                    _checkManager.Board = this;

                    int maxLimit = 0;

                    while (_checkManager.SimpleCheck(newCell) && maxLimit < 10)
                    {
                        GameObject.Destroy(newCell.CurrentGameObject);
                        
                        newCell = _spawnManager.SpawnNormalCell(tempPos);
                        _checkManager.Board = this;

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
        
    }
}
