using System;
using System.Collections;
using UnityEngine;

namespace Mathc3Project
{
    public class Element :  MonoBehaviour, IElement
    {

        public float speed;

        private IBoardManager _boardManager;
        private Vector3 _currentPosition;
        private float _localSize;
        private Vector3 _endPosition;
        private int selfX;
        private int selfY;
        private bool _canFall = true;
        private string _name;

        public IBoardManager BoardManager { get { return _boardManager; } set { _boardManager = value; } }
        public Vector3 CurrentPosition { get { return _currentPosition; } set { _currentPosition = value; } }
        public float localSize { get { return _localSize; } set { _localSize = value; } }
        public string Name { get { return _name; } set { _name = value; } }

        private void Start()
        {
            transform.position = _currentPosition;
            transform.localScale *= _localSize;
            speed = 9.81f;
            transform.name = _name;
            _endPosition = Vector3.zero;
            Debug.Log(_boardManager.Rows);
        }

        private void Update()
        {
            if (_canFall)
            {
                CheckDownCell();
                MoveDown();
            }
        }

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
                if (_boardManager.Cells[selfX, nextCell_y] != null)
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
                _boardManager.Cells[selfX, selfY] = this.gameObject;
            _endPosition = transform.position;
            yield return new WaitForSeconds(0.5f);
            _endPosition.y = nextCell + 1;
            transform.position = _endPosition;
        }


    }
}