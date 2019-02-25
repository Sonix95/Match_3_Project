using Mathc3Project.Enums;
using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface IObjectStorage
    {
        GameObject GetBackTile();
        GameObject GetHollowCell();
        GameObject GetGameElement(int cellIndex);
        GameObject GetRandomGameElement();
    }
}
