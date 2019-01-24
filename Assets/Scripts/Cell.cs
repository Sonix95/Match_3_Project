using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mathc3Project
{
    public class Cell : MonoBehaviour, ICell
    {
        public float gravity = 10f;
        public bool canFall = true;

        public Vector3 currentPosition;

        public GameObject board;

        private void Start()
        {
            board = GameObject.Find("Board");
        }

        public void CheckDownCell()
        {
        }

        public void MoveDown()
        {
        }
    }
}
