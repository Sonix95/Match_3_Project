using System.Collections;
using Mathc3Project.Classes.Abstract;
using Mathc3Project.Classes.Observer;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;
using UnityEngine;
using Object = System.Object;

namespace Mathc3Project.Classes.Scenes
{
    public class GameplayLevelScene : BaseScene
    {
        //TODO убрать это в другое место
        private ILevel _level;
        
        private IGameplayLogicManager _gameplayLogicManager;
        private ICellRegistrator _cellRegistrator;
        private IUpdateManager _updateManager;

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
            _NavigationManager.MasterManager.Coroutiner.StartCoroutine(InitialAndSetup(transferObject));
        }

        private IEnumerator InitialAndSetup(Object transferObject)
        {
            yield return new WaitForSeconds(1f);
            Initial(transferObject);
            
            yield return new WaitForSeconds(1f);
            SetUp();
        }
        
        
        private void Initial(Object transferObject)
        {
            _level = (ILevel) transferObject;
            _updateManager = new GameObject("Update MANAGER").AddComponent<UpdateManager>();
            _gameplayLogicManager = new GameObject("Gameplay Logic Manager").AddComponent<GameplayLogicManager>();
            GameObject empty = new GameObject("---------------");

            _gameplayNotifier = _NavigationManager.MasterManager.GameplayNotifier;
            _taskNotifier = new Notifier();

            _inputManager = new InputManager(_gameplayNotifier);
            _spawnManager = _NavigationManager.MasterManager.SpawnManager;
            _checkManager = new CheckManager();
            _taskManager = new TaskManager(_level.LevelTasks);
            
            _cellRegistrator = new CellRegistrator(_gameplayNotifier,_updateManager);
            
            _board = new Board(_level.BoardWidth, _level.BoardHeight, _spawnManager, _checkManager, _cellRegistrator);
        }
        
        public void SetUp()
        {
            _checkManager.Board = _board;
            
            _gameplayLogicManager.Board = _board;
            _gameplayLogicManager.Notifier = _taskNotifier;
            _gameplayLogicManager.CheckManager = _checkManager;
            _gameplayLogicManager.SpawnManager = _spawnManager;
            _gameplayLogicManager.NavigationManager = _NavigationManager;
            
            _taskManager.Notifier = _gameplayNotifier;
            
            _taskManager.AddSubscriber(_gameplayLogicManager);
            _inputManager.AddSubscriber(_gameplayLogicManager);
            _gameplayLogicManager.AddSubscriber(_taskManager);
            _updateManager.AddUpdatable(_inputManager as IUpdatable);

            _updateManager.IsUpdate = true;
        }
        
        public override void OnExit()
        {
            Debug.Log("Exit from Gameplay Level");
       //     _gameplayLogicManager.RemoveSubscriber(_taskManager);
       //     _taskManager.RemoveSubscriber(_gameplayLogicManager);
            
            _NavigationManager.MasterManager.GameplayNotifier = new Notifier();
        }

        
    }
}