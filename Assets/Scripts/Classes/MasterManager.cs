using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;
using UnityEngine;

namespace Mathc3Project.Classes
{
    public class MasterManager : IMasterManager
    {
        public MasterManager(ICoroutiner coroutiner , INotifier gameplayNotifier,
            INotifier uiNotifier, IObjectStorage objectStorage, ISpawnManager spawnManager)
        {
            Coroutiner = coroutiner;
            GameplayNotifier = gameplayNotifier;
            UINotifier = uiNotifier;
            ObjectStorage = objectStorage;
            SpawnManager = spawnManager;
        }

        public ICoroutiner Coroutiner { get; private set; }
        public INotifier GameplayNotifier { get; set; }
        public INotifier UINotifier { get; set; }
        public IObjectStorage ObjectStorage { get; private set; }
        public ISpawnManager SpawnManager { get; private set; }
    }
}