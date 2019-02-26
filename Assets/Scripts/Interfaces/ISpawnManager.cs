using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface ISpawnManager
    {
        void GenerateBackTile(Vector3 position, GameObject parent);
        ICell GenerateNormalCell(Vector3 position);
        ICell GenerateHollowCell(Vector3 position);
        ICell GenerateBreakableCell(Vector3 position);
    }
}
