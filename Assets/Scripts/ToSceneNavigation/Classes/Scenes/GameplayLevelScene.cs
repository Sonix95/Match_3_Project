using Mathc3Project.Classes;
using Mathc3Project.Classes.Observer;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;
using ToSceneNavigation.Classes.Abstract;
using UnityEngine;
using Object = System.Object;

namespace ToSceneNavigation.Classes
{
    public class GameplayLevelScene : BaseScene
    {
        //TODO убрать это в другое место
        private ILevel _level;
        

        private IUpdateManager _updateManager;
        private IGameplayLogicManager _gameplayLogicManager;

        private INotifier _gameplayNotifier;
        private INotifier _taskNotifier;
        
        private IInputManager _inputManager;
        private ISpawnManager _spawnManager;
        private ICheckManager _checkManager;
        private ITaskManager _taskManager;
        
        private IBoard _board;
        
        public override void OnEnter(Object transferObject)
        {
            Debug.Log("Now you will really play");
            Initial(transferObject);
            SetUp();
        }
        
        private void Initial(Object transferObject)
        {
            _level = (ILevel) transferObject;

            _updateManager = _NavigationManager.MasterManager.UpdateManager;
            _gameplayLogicManager = new GameObject(Strings.Logic_Manager).AddComponent<GameplayLogicManager>();
            GameObject empty = new GameObject("---------------");

            _gameplayNotifier = _NavigationManager.MasterManager.GameplayNotifier;
            _taskNotifier = new Notifier();

            _inputManager = new InputManager(_gameplayNotifier);
            _spawnManager = _NavigationManager.MasterManager.SpawnManager;
            _checkManager = new CheckManager();
            _taskManager = new TaskManager(_level.LevelTasks);
            
            _board = new Board(_level.BoardWidth, _level.BoardHeight, _spawnManager, _checkManager);
        }
        
        public void SetUp()
        {
            _checkManager.Board = _board;

            _gameplayLogicManager.Board = _board;
            _taskManager.Notifier = _gameplayNotifier;
            _gameplayLogicManager.Notifier = _taskNotifier;
            _gameplayLogicManager.CheckManager = _checkManager;
            _gameplayLogicManager.SpawnManager = _spawnManager;
            _gameplayLogicManager.NavigationManager = _NavigationManager;
            
            _taskManager.AddSubscriber(_gameplayLogicManager);
            _inputManager.AddSubscriber(_gameplayLogicManager);
            _gameplayLogicManager.AddSubscriber(_taskManager);
            _updateManager.AddUpdatable(_inputManager as IUpdatable);

            _updateManager.IsUpdate = true;
        }
        
        public override void OnExit()
        {
            Debug.Log("Exit from Gameplay Level");
        }

        
    }
}