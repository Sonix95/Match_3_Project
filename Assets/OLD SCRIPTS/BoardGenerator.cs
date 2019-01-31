using UnityEngine;

namespace Mathc3Project.OLD
{
    public class BoardGenerator : MonoBehaviour, IBorderGenerator
    {
        #region Fields

        private int _boardColumnCount;
        private int _boardRowCount;
        private bool _fillFakeElements;
        private float _cellSize;        

        public int BoardColumnCount
        {
            get { return _boardColumnCount; }
            set { _boardColumnCount = value; }
        }

        public int BoardRowCount
        {
            get { return _boardRowCount; }
            set { _boardRowCount = value; }
        }

        public bool FillFakeElements
        {
            get { return _fillFakeElements; }
            set { _fillFakeElements = value; }
        }
        public float CellSize
        {
            get { return _cellSize; }
            set { _cellSize = value; }
        }
                
        #endregion

        #region Methods
        private void Start()
        {
            CreateEmptyBoard();
        }

        public void CreateEmptyBoard()
        {
            GameObject boardObject = new GameObject("Board");
            boardObject.AddComponent<Board>();

            IBoard board = boardObject.GetComponent<Board>();

            board.Rows = _boardRowCount;
            board.Columns = _boardColumnCount;
            board.FillFakeElements = _fillFakeElements;

            for (int i = 0; i < _boardColumnCount; i++)
                for (int j = 0; j < _boardRowCount; j++)
                {
                    GameObject quadObject = GameObject.CreatePrimitive(PrimitiveType.Quad);

                    quadObject.transform.parent = boardObject.transform;
                    quadObject.transform.position = new Vector3(i, j, 0f);
                    quadObject.transform.localScale *= _cellSize;
                    quadObject.name = "R" + i + " C" + j;
                }
        }

        #endregion
    }
}
