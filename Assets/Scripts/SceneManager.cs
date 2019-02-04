using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mathc3Project.Interfaces;

namespace Mathc3Project
{
    public class SceneManager : MonoBehaviour, ISceneManager
    {
        public int boardRows;
        public int boardColumns;

        private IBoard _boardManager;
        private IGameLogicManager _gameManager;
        private ICreateManager _createManager;
        private ISpawner _spawner;

        private void Start()
        {
            _gameManager = new GameObject("Game Logic Manager").AddComponent<GameLogicManager>();
            _createManager = new CreateManager(_gameManager);
            _boardManager = new Board(boardRows, boardColumns);
            _spawner = new Spawner(_boardManager.RowCount);

            _spawner.CreateManager = _createManager;            
            _gameManager.Spawner = _spawner;
            _gameManager.BoardManager = _boardManager;
        }

    }
}
