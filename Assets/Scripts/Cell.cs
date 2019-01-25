using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mathc3Project
{
    public class Cell : MonoBehaviour, ICell
    {
        public float gravity = 10f;
        public bool canFall = true;        

        public Board board;
        public Vector3 endPosition;
        IBorderGenerator boardInfo;

        private void Start()
        {
            board = FindObjectOfType<Board>();
            boardInfo = FindObjectOfType<BoardGenerator>();
        }

        private void Update()
        {
            if(canFall)
            {
                MoveDown();
                CheckDownCell();
            }
        }       

        public void CheckDownCell()
        {            
            
        }

        IEnumerator BubleBeforeStop()
        {
             
            yield return new WaitForSeconds(.2f);
            transform.position = endPosition;
        }

        public void MoveDown()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - gravity * Time.deltaTime, 0f);
            int i = (int)transform.position.x;
            int j = (int)Mathf.Round(transform.position.y);
            float offset = transform.position.y - j;

            if (transform.position.y < boardInfo.BoardColumnCount && transform.position.y > 0)
            {
                Debug.Log($"offset {offset}");
                if (offset < 0.4)
                    board.cells[i, j] = this.gameObject;
                else board.cells[i, j-1] = null;
                Debug.Log(board.cells[i, j] == null ? "NULL" : board.cells[i, j].ToString());

            }
        }
    }
}
