using Mathc3Project.Enums;
using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface IObjectStorage
    {
        GameObject GetBackTile();
        GameObject GetBreackableCell();
        GameObject GetGameElement(int cellIndex);
        GameObject GetRandomGameElement();
    }
}
