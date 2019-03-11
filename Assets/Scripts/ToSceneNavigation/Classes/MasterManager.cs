using DefaultNamespace;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;
using UnityEngine;

namespace ToSceneNavigation.Classes
{
    public class MasterManager : IMasterManager
    {
        public MasterManager(ICoroutiner coroutiner, IUpdateManager updateManager, INotifier gameplayNotifier,
            INotifier uiNotifier, IObjectStorage objectStorage, IObjectSetter objectSetter, ISpawnManager spawnManager)
        {
            Coroutiner = coroutiner;
            UpdateManager = updateManager;
            GameplayNotifier = gameplayNotifier;
            UINotifier = uiNotifier;
            ObjectStorage = objectStorage;
            ObjectSetter = objectSetter;
            SpawnManager = spawnManager;
        }

        public ICoroutiner Coroutiner { get; private set; }
        public IUpdateManager UpdateManager { get; private set; }
        public INotifier GameplayNotifier { get; set; }
        public INotifier UINotifier { get; set; }
        public IObjectStorage ObjectStorage { get; private set; }
        public IObjectSetter ObjectSetter { get; private set; }
        public ISpawnManager SpawnManager { get; private set; }
    }
}