using Mathc3Project.Enums;
using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface IObjectStorage
    {
        GameObject GetRandomGameElement();
        GameObject GetGameElement(GameElementTypes gameElementType);
        GameObject GetPowerElement(PowerUpTypes powerUpType);
    }
}
