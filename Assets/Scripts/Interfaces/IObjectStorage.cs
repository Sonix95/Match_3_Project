using Mathc3Project.Enums;
using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface IObjectStorage
    {
        GameObject GetRandomGameElement();
        GameObject GetPowerElement(PowerUpTypes powerUpType);
    }
}
