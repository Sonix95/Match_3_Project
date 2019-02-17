using Mathc3Project.Enums;
using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface IObjectStorage
    {
        GameObject GetObjectByType(GameElementsType gameElementType);
    }
}
