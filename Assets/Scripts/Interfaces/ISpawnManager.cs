using Mathc3Project.Enums;
using Mathc3Project.Interfaces.Cells;
using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface ISpawnManager
    {
        GameObject SpawnPrefab(Vector3 position);
        GameObject SpawnPowerPrefab(PowerUpTypes powerUpType, Vector3 position);
        ICell SpawnNormalCell(Vector3 position);
    }
}
