using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface ICreateManager
    {
        IGameElement CreateGameElement(int column, float yCoord, bool updateObject);
    }
}
