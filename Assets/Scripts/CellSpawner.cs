﻿using UnityEngine;

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

        void InitSpawner()
        {
            IBorderGenerator board = FindObjectOfType<BoardGenerator>();

            spawnYPosition = board.BoardColumnCount + 1;

            spawnPositionCount = board.BoardRowCount;
            spawnPosition = new Vector3[spawnPositionCount];

            for (int i = 0; i < spawnPositionCount; i++)
            {
                spawnPosition[i] = new Vector3(i, spawnYPosition, 0f);
            }
        }

        void FirstGenerate()
        {
            for (int i = 0; i < spawnPositionCount; i++)
            {
                GameObject sphereObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                sphereObj.transform.position = spawnPosition[i];
                sphereObj.transform.localScale *= 0.6f;
                sphereObj.GetComponent<Renderer>().material.color = GenerateColor();
                sphereObj.name = $"{i}";
            }
        }

        Color GenerateColor()
        {
            int g = Random.Range(0, 3);

            int b = Random.Range(0, 3);

            Color color = new Color(0, g, b);

            return color;


        }

    }
}