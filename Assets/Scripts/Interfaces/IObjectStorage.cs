using Mathc3Project.Enums;
using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface IObjectStorage
    {
        GameObject GetRandomGameElement();
        GameObject GetGameElement(GameElementTypesEnum gameElementTypeEnum);
        GameObject GetPowerElement(PowerUpTypesEnum powerUpTypeEnum);
    }
}
