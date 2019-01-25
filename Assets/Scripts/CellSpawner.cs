using System.Collections;
using UnityEngine;

namespace Mathc3Project
{
    public class CellSpawner : MonoBehaviour, ICellSpawner
    {
        public Vector3[] spawnPosition;
        public int spawnPositionCount;
        public int spawnYPosition;

        private void Start()
        {
            InitSpawner();

            FirstGenerate();
        }

        public void InitSpawner()
        {
            IBorderGenerator board = FindObjectOfType<BoardGenerator>();

            spawnYPosition = board.BoardRowCount + 1;

            spawnPositionCount = board.BoardColumnCount;
            spawnPosition = new Vector3[spawnPositionCount];

            for (int i = 0; i < spawnPositionCount; i++)
            {
                spawnPosition[i] = new Vector3(i, spawnYPosition, 0f);
            }
        }

        public void FirstGenerate()
        {
            for (int i = 0; i < spawnPositionCount; i++)
            {
                CreateNewElement(i);
            }
        }
        void CreateNewElement(int position)
        {
            GameObject sphereObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            sphereObj.transform.position = spawnPosition[position];
            sphereObj.transform.localScale *= 0.6f;
            sphereObj.GetComponent<Renderer>().material.color = new Color(0, Random.Range(0, 3), Random.Range(0, 3));
            sphereObj.name = $"{sphereObj.GetComponent<Renderer>().material.color.ToString("N1")}";
            sphereObj.AddComponent<Cell>();
        }

        public IEnumerator NewElement()
        {
            yield return new WaitForSeconds(3f);

            //----
        }

    }
}