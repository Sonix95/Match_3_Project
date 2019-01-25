using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mathc3Project
{
    public class Cell : MonoBehaviour, ICell
    {
        #region Fields

        private const float TIME = .05f;

        private float _gravity = 9.81f;
        private bool _canFall = true;
        private Vector3 _endPosition;
        private int _selfColomn;
        private int _selfRow;
        private IBoard _board;

        public float Speed { get => _gravity; set => _gravity = value; }
        public bool CanFall { get => _canFall; set => _canFall = value; }
        public IBoard Board { get => _board; set => _board = value; }
        public Vector3 EndPosition { get => _endPosition; set => _endPosition = value; }
        public int SelfColomn { get => _selfColomn; set => _selfColomn = value; }
        public int SelfRow { get => _selfRow; set => _selfRow = value; }
        public float Gravity { get => _gravity; set => _gravity = value; }

        #endregion

        #region Methods

        private void Start()
        {
            _board = FindObjectOfType<Board>();
            _endPosition = Vector3.zero;
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
            transform.position = new Vector3(transform.position.x, transform.position.y - _gravity * Time.deltaTime, 0f);

            _selfColomn = (int)transform.position.x;
            _selfRow = (int)Mathf.Round(transform.position.y);
        }

        public void CheckDownCell()
        {
            int nextCell_y = (int)Mathf.Round(transform.position.y) - 1;

            if(nextCell_y < _board.Rows && nextCell_y >= 0)
            {
                if (_board.Cells[_selfColomn, nextCell_y] != null)
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
            if (_selfRow < _board.Rows)
                _board.Cells[_selfColomn, _selfRow] = this.gameObject;
            _endPosition = transform.position;
            yield return new WaitForSeconds(TIME);
            _endPosition.y = nextCell + 1;
            transform.position = _endPosition;
            
        }

        #endregion
    }
}
