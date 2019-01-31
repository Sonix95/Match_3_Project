using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mathc3Project
{
    public class Spawner : ISpawner
    {
        private IBoardManager _boardManager;
        private Vector3[] _spawningPositions;
        private int _spawnPositionsCount;
        private int _spawnStartPositionY;

        public IBoardManager BoardManager { get { return _boardManager; }  set { _boardManager = value; }  }
        public Vector3[] SpawningPositions { get { return _spawningPositions; } set { _spawningPositions = value; } }
        public int SpawnPositionsCount { get { return _spawnPositionsCount; } set { _spawnPositionsCount = value; } }
        public int SpawnStartPositionY { get { return _spawnStartPositionY; } set { _spawnStartPositionY = value; } }


        public Spawner(IBoardManager boardManagery)
        {
            SetSpawner(boardManagery);
        }

        public void SetSpawner(IBoardManager boardManagery)
        {
            _boardManager = boardManagery;

            _spawnPositionsCount = _boardManager.Columns;
            _spawnStartPositionY = _boardManager.Rows + 1;

            _spawningPositions = new Vector3[_spawnPositionsCount];
            for (int i = 0; i < _spawnPositionsCount; i++)
            {
                _spawningPositions[i] = new Vector3(i, _spawnStartPositionY, 0f);
                Debug.Log(_spawningPositions[i]);
            }
        }

        public void Spawn()
        {
            for (int i = 0; i < _spawnPositionsCount; i++)
            {
                if (_boardManager.Cells[i, _spawnStartPositionY - 2] == null)
                {
                    GenerateElement(_spawningPositions[i], _boardManager);
                }
            }
        }

        private void GenerateElement(Vector3 position, IBoardManager boardManagery)
        {
            IElement element = GameObject.CreatePrimitive(PrimitiveType.Sphere).AddComponent<Element>();

            element.BoardManager = boardManagery;
            element.CurrentPosition = position;
            element.localSize = 0.8f;
            element.Name = position.x + "x" + position.y;
        }

    }
}
