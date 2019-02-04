using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mathc3Project.Interfaces;

namespace Mathc3Project
{
    public class Spawner : ISpawner
    {
        #region Fields

        private float _spawnYCoordinate;
        private ICreateManager _createManager;

        public ICreateManager CreateManager { get { return _createManager; } set { _createManager = value; } }

        #endregion

        #region Constructor

        public Spawner(int yCoordinate)
        {
            _spawnYCoordinate = yCoordinate + 1;
        }

        #endregion

        #region Methods

        public void SpawnGameobject(int column)
        {
            _createManager.CreateGameElement(column, _spawnYCoordinate, true);
        }
        public IGameElement SpawnGameobject(int column, int row)
        {
            return _createManager.CreateGameElement(column, row, false);
        }

        #endregion
    }
}
