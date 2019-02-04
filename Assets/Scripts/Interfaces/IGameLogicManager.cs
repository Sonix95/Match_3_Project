using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface IGameLogicManager
    {
        ISpawner Spawner { get; set; }
        IBoard BoardManager { get; set; }

        void AddGameElementInList(IGameElement gameElement);
    }
}