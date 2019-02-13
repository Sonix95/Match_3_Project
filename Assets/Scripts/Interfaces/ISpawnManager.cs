using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface ISpawnManager
    {
        void GenerateBackTile(Vector3 position, GameObject parent);
        ICell GenerateGameElement(Vector3 position);
    }
}