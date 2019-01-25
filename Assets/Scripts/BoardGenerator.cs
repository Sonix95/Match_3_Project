using UnityEngine;

namespace Mathc3Project
{
    public class BoardGenerator : MonoBehaviour, IBorderGenerator
    {
        #region Fields

        private int _boardColumnCount;
        private int _boardRowCount;

        public int BoardColumnCount { get => _boardColumnCount; set => _boardColumnCount = value; }
        public int BoardRowCount { get => _boardRowCount; set => _boardRowCount = value; }

        #endregion
        private void Start()
        {
            CreateEmptyBoard();
        }

        public void CreateEmptyBoard()
        {
            GameObject boardObject = new GameObject("Board");
            boardObject.AddComponent<Board>();

            Board board = boardObject.GetComponent<Board>();
            board.rows = _boardRowCount;
            board.column = _boardColumnCount;

            for (int i = 0; i < _boardColumnCount; i++)
                for (int j = 0; j < _boardRowCount; j++)
                {
                    GameObject quadObject = GameObject.CreatePrimitive(PrimitiveType.Quad);

                    quadObject.transform.parent = boardObject.transform;
                    quadObject.transform.position = new Vector3(i, j, 0f);
                    quadObject.transform.localScale *= 0.8f;
                    quadObject.name = $"R{i} C{j}";
                }
        }
    }
}
