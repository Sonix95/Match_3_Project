using System.Collections;
using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface ISpawner
    {
        ICreateManager CreateManager { get; set; }

        void SpawnGameobject(int column);
        IGameElement SpawnGameobject(int column, int row);
    }
}
