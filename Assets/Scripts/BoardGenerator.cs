using UnityEngine;

namespace Mathc3Project
{
    public class BoardGenerator : MonoBehaviour, IBorderGenerator
    {
        public int boardColumnCount;
        public int boardRowCount;

        private void Start()
        {
            CreateEmptyBoard();
        }

        public void CreateEmptyBoard()
        {
            GameObject board = new GameObject("Board");

            for (int i = 0; i < boardRowCount; i++)
                for (int j = 0; j < boardColumnCount; j++)
                {
                    GameObject quadObject = GameObject.CreatePrimitive(PrimitiveType.Quad);

                    quadObject.transform.parent = board.transform;
                    quadObject.transform.position = new Vector3(i, j, 0f);
                    quadObject.transform.localScale *= 0.8f;
                    quadObject.name = $"{i}x{j}";
                }
        }
    }
}
