using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mathc3Project.Interfaces;

namespace Mathc3Project
{
    class GameLogicManager : MonoBehaviour, IGameLogicManager
    {
        #region Fields

        private ISpawner _spawner;

        private IList<IGameElement> gameElementsList;
        private IBoard _board;

        public ISpawner Spawner { get { return _spawner; } set { _spawner = value; StartCoroutine(StartSpawn(2f)); } }
        public IBoard BoardManager { get { return _board; } set { _board = value;  } }

        #endregion

        #region Methods

        private void Start()
        {
            gameElementsList = new List<IGameElement>();
            StartCoroutine(InitGameBoard());
        }





        private void Update()
        {
            foreach (var gameElement in gameElementsList)
            {
                if (gameElement.IsUpdate)
                {
                    gameElement.CustomUpdate();
                }
            }


            ////////// MOVE TO INPUT MANAGER  ////////// 
            if (Input.GetKeyDown(KeyCode.R))
            {
                int x = Random.Range(0, _board.ColumnCount);
                int y = Random.Range(0, _board.RowCount);

                if (_board.Cells[y, x] != null)
                {
                    _board.Cells[y, x].DestroyElement();
                    _board.Cells[y, x] = null;
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                foreach (var el in _board.Cells)
                    Debug.Log(el);
            }

            if (Input.GetMouseButtonDown(0))
            {
                int x = (int)Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
                int y = (int)Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

                if (x < _board.ColumnCount && x>=0 && y < _board.RowCount && y >=0)
                {
                    if (_board.Cells[y, x] != null)
                    {
                        _board.Cells[y, x].DestroyElement();
                        _board.Cells[y, x] = null;

                        for(int i=y+1; i<_board.RowCount; i++)
                        {
                            if(_board.Cells[i, x] != null)
                                _board.Cells[i, x].IsUpdate = true;
                            _board.Cells[i, x] = null;
                        }

                    }
                }
            }
            ////////// ----------------------  ////////// 
        }



        IEnumerator StartSpawn(float time)
        {
            yield return new WaitForSeconds(time);

            int Rand = Random.Range(0, _board.ColumnCount);

            if (_board.Cells[BoardManager.RowCount - 1, Rand] == null)
                _spawner.SpawnGameobject(Rand);

            StartCoroutine(StartSpawn(.075f));
        }

        IEnumerator InitGameBoard()
        {
            yield return new WaitForSeconds(.5f);
            for(int i=0; i<_board.RowCount; i++)
                for (int j = 0; j < _board.ColumnCount; j++)
                {
                    _board.Cells[i,j] = _spawner.SpawnGameobject(j, i);
                }
        }

        public void AddGameElementInList(IGameElement gameElement)
        {
            gameElementsList.Add(gameElement);
        }


        #endregion
    }
}


/////////////////////////////////////////////////////////////////////////////////////////////////////


/*
       
    
    public void MoveDown()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, 0f);

            selfX = (int)Mathf.Round(transform.position.x);
            selfY = (int)Mathf.Round(transform.position.y);
        }

        public void CheckDownCell()
        {
            int nextCell_y = (int)Mathf.Round(transform.position.y) - 1;

            if (nextCell_y < _boardManager.Rows && nextCell_y >= 0)
            {
                if (_boardManager.Cells[nextCell_y, selfX] != null)
                {
                    StartCoroutine(BubbleBeforeStop(nextCell_y));
                }
            }
            if (nextCell_y < 0)
            {
                StartCoroutine(BubbleBeforeStop(nextCell_y));
            }
        }

        IEnumerator BubbleBeforeStop(int nextCell)
        {
            _canFall = false;
            if (selfY < _boardManager.Rows)
                _boardManager.Cells[selfY, selfX] = this.gameObject;
            _endPosition = transform.position;
            yield return new WaitForSeconds(.05f);
            _endPosition.y = nextCell + 1;
            transform.position = _endPosition;
        }

    }
}
*/
