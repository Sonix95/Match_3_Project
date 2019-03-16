using Mathc3Project.Enums;
using Mathc3Project.Interfaces.Cells;
using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface ISpawnManager
    {
        GameObject SpawnRandomPrefab(Vector3 position);
        
        GameObject SpawnPrefab(GameElementTypes elementType, Vector3 position);
        GameObject SpawnPowerPrefab(PowerUpTypes powerUpType, Vector3 position);
        ICell SpawnRandomNormalCell(Vector3 position);
        ICell SpawnNormalCell(GameElementTypes gameElement, Vector3 position);
    }
}
