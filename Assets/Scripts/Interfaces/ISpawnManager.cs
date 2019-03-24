using Mathc3Project.Enums;
using Mathc3Project.Interfaces.Cells;
using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface ISpawnManager
    {
        GameObject SpawnRandomPrefab(Vector3 position);
        
        GameObject SpawnPrefab(GameElementTypesEnum elementTypeEnum, Vector3 position);
        GameObject SpawnPowerPrefab(PowerUpTypesEnum powerUpTypeEnum, Vector3 position);
        ICell SpawnRandomNormalCell(Vector3 position);
        ICell SpawnNormalCell(GameElementTypesEnum gameElement, Vector3 position);
    }
}
