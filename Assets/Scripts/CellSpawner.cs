using System.Collections;
using UnityEngine;

namespace Mathc3Project
{
    public class CellSpawner : MonoBehaviour, ICellSpawner
    {
        #region Fields

        private IBoard _board;        
        private int _spawnPositionsCount;
        private int _spawnStartPositionY;
        private Vector3[] _spawnPositions;
        private int _minRandomColor;
        private int _maxRandomColor;
        private float _spawnTime;
        private float _elementSize;
        private float _gravityElemet;

        public IBoard Board { get { return _board; } set { _board = value; } }
        public int SpawnPositionsCount { get { return _spawnPositionsCount; } set { _spawnPositionsCount = value; } }
        public int SpawnStartPositionY { get { return _spawnStartPositionY; } set { _spawnStartPositionY = value; } }
        public Vector3[] SpawnPositions { get { return _spawnPositions; } set { _spawnPositions = value; } }
        public int MinRandomColor { get { return _minRandomColor; } set { _minRandomColor = value; } }
        public int MaxRandomColor { get { return _maxRandomColor; } set { _maxRandomColor = value; } }
        public float SpawnTime { get { return _spawnTime; } set { _spawnTime = value; } }
        public float ElementSize { get { return _elementSize; } set { _elementSize = value; } }
        public float GravityElement { get { return _gravityElemet; } set { _gravityElemet = value; } }

        #endregion

        #region Methods
        private void Start()
        {            
            Init();
            StartCoroutine(CheckCells());
        }

        public void Init()
        {
            _board = FindObjectOfType<Board>();

            _spawnPositionsCount = _board.Columns;
            _spawnStartPositionY = _board.Rows + 1;

            _spawnPositions = new Vector3[_spawnPositionsCount];

            for(int i=0; i<_spawnPositionsCount; i++)
            {
                _spawnPositions[i] = new Vector3(i, _spawnStartPositionY, 0f);
            }            
        }

        public IEnumerator CheckCells()
        {
            yield return new WaitForSeconds(_spawnTime);
            for (int i = 0; i < _spawnPositionsCount; i++)
            {
                if(_board.Cells[i, _spawnStartPositionY - 2] == null)
                {
                    GenerateElement(i);
                }
            }

            StartCoroutine(CheckCells());
        }

        public void GenerateElement(int position)
        {
            GameObject sphereObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            sphereObj.transform.position = _spawnPositions[position];
            sphereObj.transform.localScale *= _elementSize;
            sphereObj.GetComponent<Renderer>().material.color = new Color(0, Random.Range(_minRandomColor, _maxRandomColor), Random.Range(_minRandomColor, _maxRandomColor));
            sphereObj.name = sphereObj.GetComponent<Renderer>().material.color.ToString("N1");
            sphereObj.AddComponent<Cell>();
            sphereObj.GetComponent<Cell>().Gravity = _gravityElemet;
        }

        #endregion
    }
}
