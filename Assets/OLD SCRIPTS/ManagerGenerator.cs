using UnityEngine;

namespace Mathc3Project.OLD
{
    public class ManagerGenerator : MonoBehaviour, IManagerGenerator
    {
        #region Fields

        [Header("Board")]
        public bool fillFakeElements = true;
        public int borderRows = 9;
        public int borderColumn = 9;
        public float cellSize = .8f;        

        [Header("Spawner")]
        public int minRandomColor = 0;
        public int maxRandomColor = 3;
        public float spawnTime = 1f;
        public float elementSize = .6f;
        public float gravityElement = 10f;

        #endregion

        #region Methods
        void Start()
        {
            CreateBoardGenerator();
            CreateCellSpawner();

            CameraPositionAndCreateEmptyObject();
        }

        public void CreateBoardGenerator()
        {
            GameObject boardGeneratorObject = new GameObject("Board Generator");
            boardGeneratorObject.AddComponent<BoardGenerator>();

            IBorderGenerator borderGenerator = boardGeneratorObject.GetComponent<BoardGenerator>();

            borderGenerator.BoardRowCount = borderRows;
            borderGenerator.BoardColumnCount = borderColumn;
            borderGenerator.FillFakeElements = fillFakeElements;
            borderGenerator.CellSize = cellSize;
        }

        public void CreateCellSpawner()
        {
            GameObject cellSpawnerObject = new GameObject("Cell Spawner");
            cellSpawnerObject.AddComponent<CellSpawner>();

            ICellSpawner cellSpawner = cellSpawnerObject.GetComponent<CellSpawner>();

            cellSpawner.MinRandomColor = minRandomColor;
            cellSpawner.MaxRandomColor = maxRandomColor;
            cellSpawner.SpawnTime = spawnTime;
            cellSpawner.ElementSize = elementSize;
            cellSpawner.GravityElement = gravityElement;
        }

        private void CameraPositionAndCreateEmptyObject()
        {
            Camera.main.transform.position = new Vector3(borderColumn / 2, borderRows / 2, -10f);
            Camera.main.GetComponent<Camera>().orthographicSize = 10;
            GameObject boardGenerator = new GameObject("-----------------------");
        }

        #endregion
    }
}