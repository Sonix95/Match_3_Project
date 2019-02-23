using System;
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
        
        public Board(int width, int height, ISpawnManager spawnManager)
        {
            _width = width;
            _height = height;
            _cells = new Cell[_width, _height];
            _spawnManager = spawnManager;
            
             Initial();
        }

        
        private void Initial()
        {
            GameObject boardParent = new GameObject("Board");

            for (int i = 0; i < _width; i++)
            for (int j = 0; j < _height; j++)
            {
                Vector3 tempPos = new Vector3(i, j, 0f);

                _spawnManager.GenerateBackTile(tempPos, boardParent);
                
                ICell newCell =  _spawnManager.GenerateGameElement(tempPos);

                int maxLimit = 0;
                while (CheckAndMarkManager.SimpleCheck(newCell,this) && maxLimit < 100)
                {
                    GameObject.Destroy(newCell.CurrentGameObject);
                    newCell =  _spawnManager.GenerateGameElement(tempPos);
                    
                    maxLimit++;
                }
                maxLimit = 0;
                
                _cells[i, j] = newCell;
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
    }
}
