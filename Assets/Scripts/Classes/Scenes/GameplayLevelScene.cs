using System.Collections;
using Mathc3Project.Classes.Abstract;
using Mathc3Project.Classes.ButtonsManager;
using Mathc3Project.Classes.Observer;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;
using UnityEngine;
using Object = System.Object;

namespace Mathc3Project.Classes.Scenes
{
    public class GameplayLevelScene : BaseScene
    {
        private IGameplayLogicManager _gameplayLogicManager;
        private IUpdateManager _updateManager;

        private INotifier _gameplayNotifier;
        private INotifier _uiNotifier;
        private INotifier _taskNotifier;

        private IInputManager _inputManager;
        private ISpawnManager _spawnManager;
        private ICheckManager _checkManager;
        private ITaskManager _taskManager;

        private IUIManager _uiManager;
        private IButtonsManager _buttonsManager;

        private ICellRegistrator _cellRegistrator;

        private ILevel _level;
        private IBoard _board;

        public override void OnEnter(Object transferObject)
        {
            Debug.Log("Now you will really play");
            _level = (ILevel) transferObject;

            Initial();
            SetUp();
        }

        private void Initial()
        {
            _updateManager = _NavigationManager.MasterManager.UpdateManager;
            _gameplayLogicManager = new GameObject("Gameplay Logic Manager").AddComponent<GameplayLogicManager>();
            GameObject empty = new GameObject("---------------");

            _gameplayNotifier = _NavigationManager.MasterManager.GameplayNotifier;
            _uiNotifier = _NavigationManager.MasterManager.UINotifier;
            _taskNotifier = new Notifier();

            _inputManager = new InputManager(_gameplayNotifier);
            _spawnManager = _NavigationManager.MasterManager.SpawnManager;
            _checkManager = new CheckManager();
            _taskManager = new TaskManager(_level.LevelTasks);

            _uiManager = new UIGameplayLevelManager(_level);
            _buttonsManager = new ButtonsGameplaySceneManager(_uiNotifier);

            _cellRegistrator = new CellRegistrator(_gameplayNotifier, _updateManager);

            _board = new Board(_level.BoardWidth, _level.BoardHeight, _spawnManager, _checkManager, _cellRegistrator);
        }

        public void SetUp()
        {
            _board.Initial();
            
            _checkManager.Board = _board;

            _uiManager.NavigationManager = _NavigationManager;

            _gameplayLogicManager.Board = _board;
            _gameplayLogicManager.Notifier = _taskNotifier;
            _gameplayLogicManager.CheckManager = _checkManager;
            _gameplayLogicManager.SpawnManager = _spawnManager;
            _gameplayLogicManager.NavigationManager = _NavigationManager;

            _taskManager.Notifier = _gameplayNotifier;

            _buttonsManager.AddSubscriber(_uiManager);
            _taskManager.AddSubscriber(_gameplayLogicManager);
            _inputManager.AddSubscriber(_gameplayLogicManager);
            _gameplayLogicManager.AddSubscriber(_taskManager);
            _gameplayLogicManager.AddSubscriber(_uiManager);
            _updateManager.AddUpdatable(_inputManager as IUpdatable);

            _updateManager.IsUpdate = true;
            _buttonsManager.Notify(EventTypesEnum.UI_SceneLoaded, null);
        }

        public override void OnExit()
        {
            Debug.Log("Exit from Gameplay Level");
           _updateManager.RemoveUpdatable(_inputManager as IUpdatable);
           foreach (var cell in _board.Cells)
           {
               _updateManager.RemoveUpdatable(cell as IUpdatable);
           }
           
           _updateManager.IsUpdate = false;
           
            _NavigationManager.MasterManager.GameplayNotifier = new Notifier();
            _NavigationManager.MasterManager.UINotifier = new Notifier();
        }

    }
}
