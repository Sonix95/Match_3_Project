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
            _boardColumnCount = 5;
            _boardRowCount = 9;

            CreateEmptyBoard();
        }

        public void CreateEmptyBoard()
        {
            GameObject board = new GameObject("Board");

            for (int i = 0; i < _boardRowCount; i++)
                for (int j = 0; j < _boardColumnCount; j++)
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
