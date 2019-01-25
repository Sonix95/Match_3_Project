using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mathc3Project
{
    public class Board : MonoBehaviour, IBoard
    {
        #region Fields

        private int _rows;
        private int _columns;
        private GameObject[,] _cells;
        private bool _fillFakeElements;

        public int Rows { get => _rows; set => _rows = value; }
        public int Columns { get => _columns; set => _columns = value; }
        public GameObject[,] Cells { get => _cells; set => _cells = value; }
        public bool FillFakeElements { get => _fillFakeElements; set => _fillFakeElements = value; }

        #endregion

        #region Methods
        private void Start()
        {
            InitBoard();
            if(_fillFakeElements)
            {
                TEST_FAKE_DATA();
            }
        }
        
        public void InitBoard()
        {
            _cells = new GameObject[_columns, _rows];   
        }
        
        #endregion

        #region Mathods to Fake Data -- DELETE

        public void TEST_FAKE_DATA()
        {
            for (int i = 0; i < _columns-1; i++)
                for (int j = 0; j < _rows; j++)
                {
                    int RANDOM = Random.Range(0, 10);
                    if(RANDOM > 8) 
                        _cells[i, j] = GenerateElement(i,j);
                }            
        }

        public GameObject GenerateElement(int i, int j)
        {
            GameObject testOBJ = GameObject.CreatePrimitive(PrimitiveType.Capsule);

            testOBJ.transform.position = new Vector3(i, j, 0f);
            testOBJ.transform.localScale *= .5f;
            testOBJ.GetComponent<Renderer>().material.color = Color.gray;
            testOBJ.name = $"TEST OBJECT";

            return testOBJ;
        }

        #endregion
    }
}
