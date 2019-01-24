using UnityEngine;

namespace Mathc3Project
{
    public class ManagerGenerator : MonoBehaviour, IManagerGenerator
    {
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