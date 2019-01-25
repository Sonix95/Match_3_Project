using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mathc3Project
{
    public class Board : MonoBehaviour
    {
        public GameObject[,] cells;
        public int rows;
        public int column;

        private void Start()
        {
            InitBoard();
        }
        
        void InitBoard()
        {
            cells = new GameObject[column, rows];

            //cells[0, 1] = GameObject.Find("Main Camera");
            //cells[2, 0] = GameObject.Find("Main Camera");

            //CreateTempObj(0, 1, 1);
            //CreateTempObj(2, 0, 2);

            //foreach (GameObject g in cells)
            //    Debug.Log(g);
        }

        //void CreateTempObj(float x, float y, int i)
        //{
        //    GameObject Temp = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //    Temp.transform.position = new Vector3(x, y, 0f);
        //    Temp.transform.localScale *= 0.4f;
        //    Temp.GetComponent<Renderer>().material.color = Color.gray;
        //    Temp.name = $"TEMP {i}";
        //}
    }
}
