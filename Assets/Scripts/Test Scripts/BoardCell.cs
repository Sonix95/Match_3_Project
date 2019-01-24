using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
namespace TESTing
{
    public class BoardCell : MonoBehaviour
    {
        public int column;
        public int row;
        public int previousColumn;
        public int previousRow;
        public int targetX;
        public int targetY;
        public bool isMatched = false;

        private GameObject otherCell;
        private Board board;
        private Vector3 firstPosition = Vector3.zero;
        private Vector3 secondPosition = Vector3.zero;
        private Vector3 tempPosition = Vector3.zero;
        private float angle = 0;

        private void Start()
        {
            board = FindObjectOfType<Board>();
            targetX = (int)transform.position.x;
            targetY = (int)transform.position.y;
            column = targetX;
            row = targetY;
            previousColumn = row;
            previousColumn = column;
        }

        private void Update()
        {
            FindMatches();
            if (isMatched)
            {
                this.gameObject.GetComponent<Renderer>().material.color = Color.white;
            }

            targetX = column;
            targetY = row;

            Swap();
        }

        private void Swap()
        {
            if (Mathf.Abs(targetX - transform.position.x) > .1)
            {
                tempPosition = new Vector3(targetX, transform.position.y, 0f);
                transform.position = Vector3.Lerp(transform.position, tempPosition, 0.7f);
                if (board.allCells[column, row] != this.gameObject)
                {
                    board.allCells[column, row] = this.gameObject;
                }
            }
            else
            {
                tempPosition = new Vector3(targetX, transform.position.y, 0f);
                transform.position = tempPosition;
            }

            if (Mathf.Abs(targetY - transform.position.y) > .1)
            {
                tempPosition = new Vector3(transform.position.x, targetY, 0f);
                transform.position = Vector3.Lerp(transform.position, tempPosition, 0.7f);
                if (board.allCells[column, row] != this.gameObject)
                {
                    board.allCells[column, row] = this.gameObject;
                }
            }
            else
            {
                tempPosition = new Vector3(transform.position.x, targetY, 0f);
                transform.position = tempPosition;
            }
        }

        public IEnumerator CheckMove()
        {
            yield return new WaitForSeconds(0.5f);
            if (otherCell != null)
            {
                if (!isMatched && !otherCell.GetComponent<BoardCell>().isMatched)
                {
                    otherCell.GetComponent<BoardCell>().column = column;
                    otherCell.GetComponent<BoardCell>().row = row;
                    column = previousColumn;
                    row = previousRow;
                }
                otherCell = null;
            }

        }

        private void OnMouseDown()
        {
            firstPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        private void OnMouseUp()
        {
            secondPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
        }

        void CalculateAngle()
        {
            angle = Mathf.Atan2(secondPosition.y - firstPosition.y, secondPosition.x - firstPosition.x) * 180 / Mathf.PI;
            MovePieces();
        }

        void MovePieces()
        {
            if (angle > -45 && angle <= 45 && column < board.width - 1)
            {
                //Right Swipe
                otherCell = board.allCells[column + 1, row];
                previousRow = row;
                previousColumn = column;
                otherCell.GetComponent<BoardCell>().column -= 1;
                column += 1;

            }
            else if (angle > 45 && angle <= 135 && row < board.height - 1)
            {
                //Up Swipe
                otherCell = board.allCells[column, row + 1];
                previousRow = row;
                previousColumn = column;
                otherCell.GetComponent<BoardCell>().row -= 1;
                row += 1;

            }
            else if ((angle > 135 || angle <= -135) && column > 0)
            {
                //Left Swipe
                otherCell = board.allCells[column - 1, row];
                previousRow = row;
                previousColumn = column;
                otherCell.GetComponent<BoardCell>().column += 1;
                column -= 1;
            }
            else if (angle < -45 && angle >= -135 && row > 0)
            {
                //Down Swipe
                otherCell = board.allCells[column, row - 1];
                previousRow = row;
                previousColumn = column;
                otherCell.GetComponent<BoardCell>().row += 1;
                row -= 1;
            }

            StartCoroutine(CheckMove());
        }

        void FindMatches()
        {
            if (column > 0 && column < board.width - 1)
            {
                GameObject leftCell = board.allCells[column - 1, row];
                GameObject rightCell = board.allCells[column + 1, row];
                if (leftCell.GetComponent<Renderer>().material.color ==
                    this.gameObject.GetComponent<Renderer>().material.color &&
                    rightCell.GetComponent<Renderer>().material.color ==
                    this.gameObject.GetComponent<Renderer>().material.color)
                {
                    leftCell.GetComponent<BoardCell>().isMatched = true;
                    rightCell.GetComponent<BoardCell>().isMatched = true;
                    isMatched = true;
                }
            }
            if (row > 0 && row < board.height - 1)
            {
                GameObject upCell = board.allCells[column, row + 1];
                GameObject downCell = board.allCells[column, row - 1];
                if (upCell.GetComponent<Renderer>().material.color ==
                    this.gameObject.GetComponent<Renderer>().material.color &&
                    downCell.GetComponent<Renderer>().material.color ==
                    this.gameObject.GetComponent<Renderer>().material.color)
                {
                    upCell.GetComponent<BoardCell>().isMatched = true;
                    downCell.GetComponent<BoardCell>().isMatched = true;
                    isMatched = true;
                }
            }



        }

    }
}
*/