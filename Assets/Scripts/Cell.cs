using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mathc3Project
{
    public class Cell : MonoBehaviour, ICell
    {
        #region Fields

        public float speed = 3f;
        public bool canFall = true;

        public Board board;
        public Vector3 endPosition;

        private int selfColomn;
        private int selfRow;

        #endregion

        #region Methods

        private void Start()
        {
            board = FindObjectOfType<Board>();
            endPosition = Vector3.zero;
        }

        private void Update()
        {
            if (canFall)
            {                
                MoveDown();
            }            
        }
        public void MoveDown()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, 0f);

            selfColomn = (int)transform.position.x;
            selfRow = (int)Mathf.Round(transform.position.y);

            CheckDownCell();
        }

        public void CheckDownCell()
        {
            int nextCell_y = (int)Mathf.Round(transform.position.y) - 1;

            if(nextCell_y < board.rows && nextCell_y >= 0)
            {
                if (board.cells[selfColomn, nextCell_y] != null)
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
            canFall = false;
            endPosition = transform.position;
            endPosition.y = nextCell + 1;
            yield return new WaitForSeconds(.1f);
            transform.position = endPosition;
            if (selfRow < board.rows)
                board.cells[selfColomn, selfRow] = this.gameObject;
        }

        #endregion
    }
}
