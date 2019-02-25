using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface ISpawnManager
    {
        void GenerateBackTile(Vector3 position, GameObject parent);
        ICell GenerateHollowCell(Vector3 position);
        ICell GenerateRandomGameElement(Vector3 position);
    }
}
