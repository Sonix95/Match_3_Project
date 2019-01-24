using UnityEngine;

public class BoardGenerator : MonoBehaviour, IBorderGenerator
{
    public int BoardColumnCount { get; set; }
    public int BoardRowCount { get; set; }

    private void Start()
    {
        BoardColumnCount = 5;
        BoardRowCount = 4;

        CreateEmptyBoard();
    }

    public void CreateEmptyBoard()
    {
        GameObject board = new GameObject("Board");
        board.AddComponent<Board>();

        board.GetComponent<Board>().BoardRowCount = BoardColumnCount;
        board.GetComponent<Board>().BoardRowCount = BoardRowCount;

        for (int i = 0; i < BoardRowCount; i++)
            for (int j = 0; j < BoardColumnCount; j++)
            {
                GameObject quadObject = GameObject.CreatePrimitive(PrimitiveType.Quad);

                quadObject.transform.parent = board.transform;
                quadObject.transform.position = new Vector3(i, j, 0f);
                quadObject.transform.localScale *= 0.8f;
                quadObject.name = $"(Cell: {i}, {j} )";

            }
    }
}
