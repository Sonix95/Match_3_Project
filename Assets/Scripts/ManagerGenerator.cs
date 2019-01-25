using UnityEngine;

namespace Mathc3Project
{
    public class ManagerGenerator : MonoBehaviour, IManagerGenerator
    {
        public int borderRows;
        public int borderColumn;

        void Start()
        {
            CreateBoardGenerator();
            CreateCellSpawner();

            GenerateEmptyGO();
        }      

        public void CreateBoardGenerator()
        {
            GameObject boardGenerator = new GameObject("Board Generator");
            boardGenerator.AddComponent<BoardGenerator>();

            boardGenerator.GetComponent<BoardGenerator>().BoardRowCount = borderRows;
            boardGenerator.GetComponent<BoardGenerator>().BoardColumnCount = borderColumn;
        }

        public void CreateCellSpawner()
        {
            GameObject cellGenerator = new GameObject("Cell Spawner");
            cellGenerator.AddComponent<CellSpawner>();
        }
        private void GenerateEmptyGO()
        {
            GameObject boardGenerator = new GameObject("-----------------------");
        }

    }
}