using Mathc3Project.Enums;
using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface IObjectSetter
    {
        ICell SetGameplayObject(CellTypes type, GameObject go);
        void SetNonGameplayObject(GameObject go, GameObject parent, Vector3 position);
    }
}
