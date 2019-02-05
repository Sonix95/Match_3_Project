using System;
using System.Collections;
using UnityEngine;
using Mathc3Project.Interfaces;

namespace Mathc3Project
{
    public class GameElement : MonoBehaviour, IGameElement
    {
        #region Fields

        private float _size;
        private Color _color;
        private string _name;
        private bool _isUpdate;
        private Vector3 _currentPosition;
        private IBoard _board;


        public int selfX;
        public int selfY;
        public bool _canFall = true;
        public Vector3 _endPosition = Vector3.zero;

        public float Size { get { return _size; } set { _size = value; } }
        public Color Color { get { return _color; } set { _color = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public bool IsUpdate { get { return _isUpdate; } set { _isUpdate = value; } }
        public Vector3 CurrentPosition { get { return _currentPosition; } set { _currentPosition = value; } }
        public IBoard Board { get { return _board; } set { _board = value; } }

        #endregion

        #region Methods

        private void Start()
        {
            transform.localScale *= _size;
            GetComponent<Renderer>().material.color = _color;
            name = _name;
            transform.position = _currentPosition;
        }

        public void SetPosition()
        {
            transform.position = CurrentPosition;
        }

        public void CustomUpdate()
        {
            MoveDown();
            CheckDownCell();
        }

       

        public void MoveDown()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 10f * Time.deltaTime, 0f);

            selfX = (int)Mathf.Round(transform.position.x);
            selfY = (int)Mathf.Round(transform.position.y);
        }
        public void CheckDownCell()
        {
            int nextCell_y = (int)Mathf.Round(transform.position.y) - 1;
            if (nextCell_y < _board.RowCount && nextCell_y >= 0)
            {
                if (_board.Cells[nextCell_y, selfX] != null)
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
            _isUpdate = false;
            if (selfY < _board.RowCount)
                _board.Cells[selfY, selfX] = this;
            _endPosition = transform.position;
            yield return new WaitForSeconds(.025f);
            _endPosition.y = nextCell + 1;
            transform.position = _endPosition;

            if (transform.position.y >= _board.RowCount)
                DestroyElement();


        }

        public void DestroyElement()
        {   
            Destroy(gameObject);
        }

       

        #endregion
    }
}
