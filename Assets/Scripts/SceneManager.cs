using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mathc3Project
{
    public class SceneManager : MonoBehaviour, ISceneManager
    {
        public int boardRows;
        public int boardColumns;

        private IBoardManager _boardManager;
        private ISpawner _spawner;


        void Start()
        {
            _boardManager = new BoardManager(boardRows, boardColumns);
            _spawner = new Spawner(_boardManager);
            StartCoroutine(ie());
        }

        IEnumerator ie()
        {
            _spawner.Spawn();
            yield return new WaitForSeconds(.5f);
            StartCoroutine(ie());
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _boardManager.SetBoard(boardRows, boardColumns);
                _spawner.SetSpawner(_boardManager);
                _spawner.Spawn();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                for (int i = 0; i < _boardManager.Rows; i++)
                    for (int j = 0; j < _boardManager.Columns; j++)
                    {
                        Debug.Log(_boardManager.Cells[i, j]);
                    }
            }
        }



    }
}
