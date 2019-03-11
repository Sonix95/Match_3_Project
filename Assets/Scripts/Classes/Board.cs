using Mathc3Project.Classes.Cells;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Cells;
using Mathc3Project.Interfaces.Observer;
using UnityEngine;

namespace Mathc3Project.Classes
{
    public class Board : IBoard
    {
        private int _width;
        private int _height;

        private ISpawnManager _spawnManager;
        private ICheckManager _checkManager;
        private ICellRegistrator _cellRegistrator;
        
        private ICell[,] _cells;

        public Board(int width, int height, ISpawnManager spawnManager, ICheckManager checkManager, ICellRegistrator cellRegistrator)
        {
            _width = width;
            _height = height;
            _spawnManager = spawnManager;
            _checkManager = checkManager;
            _cellRegistrator = cellRegistrator;
            _cells = new ICell[_width, _height];

            _checkManager.Board = this;
            
            Initial();
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
                    while (_checkManager.SimpleCheck(newCell) && maxLimit < Strings.MAX_LIMIT_CHECK_COUNTER)
                    {
                        GameObject.Destroy(newCell.CurrentGameObject);

                        newCell = _spawnManager.SpawnNormalCell(tempPos);
                        _checkManager.Board = this;

                        maxLimit++;
                    }
                    maxLimit = 0;

                    _cellRegistrator.RegistrateNormalCell(newCell as NormalCell);

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
        
        public ICellRegistrator CellRegistrator
        {
            get { return _cellRegistrator; }
            set { _cellRegistrator = value; }
        }
    }
}
