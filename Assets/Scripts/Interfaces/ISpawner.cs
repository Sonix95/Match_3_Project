using System.Collections;
using UnityEngine;

namespace Mathc3Project
{
    public interface ISpawner
    {
        IBoardManager BoardManager { get; set; }
        Vector3[] SpawningPositions { get; set; }
        int SpawnPositionsCount { get; set; }
        int SpawnStartPositionY { get; set; }

        void SetSpawner(IBoardManager boardManager);
        void Spawn();
    }
}
