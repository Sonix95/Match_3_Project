using Mathc3Project.Classes;
using Mathc3Project.Classes.Observer;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;
using UnityEngine;

namespace Editor.Tests
{
    public static class ObjectsCreator
    {
        public static ICoroutiner CreateCoroutiner()
        {
            GameObject coroutinerGO = new GameObject("Coroutiner");
            coroutinerGO.AddComponent<Coroutiner>();
            ICoroutiner coroutiner = coroutinerGO.GetComponent<Coroutiner>();

            return coroutiner;
        }

        public static IGameplayLogicManager CreateGameplayLogicManager()
        {
            GameObject gameplayLogicManagerGO = new GameObject("Gameplay Logic Manager");

            gameplayLogicManagerGO.AddComponent<GameplayLogicManager>();
            IGameplayLogicManager gameplayLogicManager = gameplayLogicManagerGO.GetComponent<GameplayLogicManager>();

            return gameplayLogicManager;
        }

        public static IMasterManager CreateMasterManager()
        {
            ICoroutiner coroutiner = CreateCoroutiner();
            IUpdateManager updateManager = CreateUpdateManager();
            INotifier gameplayNotifier = new Notifier();
            INotifier uiNotifier = new Notifier();
            IObjectStorage objectStorage = new ObjectStorage();
            ISpawnManager spawnManager = new SpawnManager(objectStorage);

            IMasterManager masterManager = new MasterManager(coroutiner, updateManager, gameplayNotifier,
                uiNotifier, objectStorage, spawnManager);

            return masterManager;
        }

        public static IUpdateManager CreateUpdateManager()
        {
            GameObject updateManagerGO = new GameObject("Update Manager");
            updateManagerGO.AddComponent<UpdateManager>();
            IUpdateManager updateManager = updateManagerGO.GetComponent<UpdateManager>();

            return updateManager;
        }

        public static ITaskManager CreateTaskManager()
        {
            ILevelTask simpleTaskA = new LevelTask(Strings.Tag_RedCircle, 1);
            ILevelTask simpleTaskB = new LevelTask(Strings.Tag_BlueMultiAngle, 1);

            ILevelTask[] levelTasks = new ILevelTask[] {simpleTaskA, simpleTaskB};

            ITaskManager taskManager = new TaskManager(levelTasks);

            return taskManager;
        }

        public static ICellRegistrator CreateCellRegistrator()
        {
            INotifier notifier = new Notifier();
            IUpdateManager updateManager = CreateUpdateManager();
            ICellRegistrator cellRegistrator = new CellRegistrator(notifier, updateManager);

            return cellRegistrator;
        }

    }
}
