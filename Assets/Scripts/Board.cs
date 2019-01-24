using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour, IBoard
{
    public GameObject[,] allCells;

    public int BoardRowCount { get; set; }
    public int BoardColumnCount { get; set; }

    private void Start()
    {
        BoardRowCount = 0;
        BoardColumnCount = 0;

        InitilalBoard();
    }

    public void InitilalBoard()
    {
        for (int i = 0; i < BoardRowCount; i++)
            for (int j = 0; j < BoardColumnCount; j++)
            {
                GameObject cell = GameObject.CreatePrimitive(PrimitiveType.Sphere) as GameObject;

                cell.transform.parent = this.transform;
                transform.position = new Vector3(i, j, 0f);
                cell.transform.localScale *= 0.8f;
                cell.GetComponent<Renderer>().material.color = new Color(0, Random.Range(0, 2), Random.Range(0, 2));
                cell.name = $"( {i}, {j} )";

                

                allCells[i, j] = cell;

            }
        Debug.Log(BoardRowCount);
        Debug.Log(BoardColumnCount);
    }
}
