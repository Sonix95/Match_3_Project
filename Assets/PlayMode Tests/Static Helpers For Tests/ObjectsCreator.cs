using Mathc3Project.Classes;
using Mathc3Project.Classes.Observer;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;
using UnityEngine;

namespace Tests.Static
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

        public static IBoard CreateBoard(int width, int height)
        {
            IMasterManager masterManager = CreateMasterManager(); 

            ISpawnManager spawnManager = masterManager.SpawnManager; 
            INotifier notifier = masterManager.GameplayNotifier; 
            IUpdateManager updateManager = masterManager.UpdateManager; 
            ICheckManager checkManager = new CheckManager(); 
            ICellRegistrator cellRegistrator = new CellRegistrator(notifier, updateManager); 

            IBoard board = new Board(width, height, spawnManager, checkManager, cellRegistrator);
            return board;
        }

        public static IBoard CreateBoard(int width, int height, out IMasterManager masterManager, out ICellRegistrator cellRegistrator)
        {
            
            masterManager = CreateMasterManager();

            ISpawnManager spawnManager = masterManager.SpawnManager;
            INotifier notifier = masterManager.GameplayNotifier;
            IUpdateManager updateManager = masterManager.UpdateManager;
            ICheckManager checkManager = new CheckManager();
            cellRegistrator = new CellRegistrator(notifier, updateManager);

            IBoard board = new Board(width, height, spawnManager, checkManager, cellRegistrator);
            return board;
        }

        public static ILevelTask CreateLevelTask(string elementTag, int count)
        {
            ILevelTask levelTask = new LevelTask(elementTag,count);

            return levelTask;
        }
        
        public static ILevel CreateLevel(int levelID, int boardWidth, int boardHeight, ILevelTask[] levelTasks)
        {
            ILevel level = new Level(levelID, boardWidth, boardHeight, levelTasks);

            return level;
        }
        
    }
}