using System.Collections;
using UnityEngine;

namespace Mathc3Project.OLD
{
    public interface ICellSpawner
    {
        IBoard Board { get; set; }
        int SpawnPositionsCount { get; set; }
        int SpawnStartPositionY { get; set; }
        Vector3[] SpawnPositions { get; set; }
        int MinRandomColor { get; set; }
        int MaxRandomColor { get; set; }
        float SpawnTime { get; set; }
        float ElementSize { get; set; }
        float GravityElement { get; set; }

        void Init();
        IEnumerator CheckCells();
        void GenerateElement(int position);
    }
}
