using UnityEngine;

namespace TESTing
{
    public class BoardTest : MonoBehaviour
    {
        public int height;
        public int width;

        public GameObject[,] allCells;

        void Start()
        {
            allCells = new GameObject[width, height];
            CreateBoardAndFill();
            Camera.main.transform.position = new Vector3(height / 2, width / 2, -10f);
        }

        private void CreateBoardAndFill()
        {
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    GameObject quadObject = GameObject.CreatePrimitive(PrimitiveType.Quad);

                    quadObject.transform.parent = this.transform;
                    Vector3 position = new Vector3(i, j, 0);
                    quadObject.transform.position = position;
                    quadObject.transform.localScale *= 0.8f;
                    quadObject.name = $"( {i}, {j} )";
                    //-----
                    GameObject cell = GameObject.CreatePrimitive(PrimitiveType.Sphere) as GameObject;

                    cell.transform.parent = this.transform;
                    cell.transform.position = position;
                    cell.transform.localScale *= 0.8f;
                    cell.GetComponent<Renderer>().material.color = new Color(0, Random.Range(0, 2), Random.Range(0, 2));
                    cell.name = $"( {i}, {j} )";

                   // cell.AddComponent<BoardCell>();
                    //-----

                    allCells[i, j] = cell;
                }
        }




    }

}