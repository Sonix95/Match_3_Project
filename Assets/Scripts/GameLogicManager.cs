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
        private IBoard _boardManager;

        public ISpawner Spawner { get { return _spawner; } set { _spawner = value; StartCoroutine(StartSpawn(4f)); } }
        public IBoard BoardManager { get { return _boardManager; } set { _boardManager = value;  } }

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
                    CustomUpdate(gameElement);
                }
            }
        }

        IEnumerator StartSpawn(float time)
        {
            yield return new WaitForSeconds(time);
            _spawner.SpawnGameobject(Random.Range(0, _boardManager.ColumnCount));
            StartCoroutine(StartSpawn(.5f));
        }

        IEnumerator InitGameBoard()
        {
            yield return new WaitForSeconds(.5f);
            for(int i=0; i<_boardManager.RowCount; i++)
                for (int j = 0; j < _boardManager.ColumnCount; j++)
                {
                    _boardManager.Cells[i,j] = _spawner.SpawnGameobject(j, i);
                }
        }

        void CustomUpdate(IGameElement go)
        {
            go.CurrentPosition = new Vector3(go.CurrentPosition.x, go.CurrentPosition.y - 10f * Time.deltaTime);
            go.SetPosition();
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

        private void OnDestroy()
        {
            Debug.Log("AAA");
        }

    }
}
*/
