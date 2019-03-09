using System.Collections;
using Mathc3Project.Classes;
using Mathc3Project.Classes.Observer;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;
using UnityEngine;

namespace Mathc3Project.Main
{
    public class InitialManager : MonoBehaviour
    {
        //TODO убрать это в другое место
        public int width;
        public int height;
        private ILevelTask[] _levelTasks;
        

        private IUpdateManager _updateManager;
        private ILogicManager _logicManager;

        private INotifier _notifier;

        private IInputManager _inputManager;
        private ISpawnManager _spawnManager;
        private ICheckManager _checkManager;
        
        private IObjectStorage _objectStorage;
        private IObjectSetter _objectSetter;

        private ITaskManager _taskManager;
        private INotifier _taskNotifier;
        
        private IBoard _board;

        private void Start()
        {
            _levelTasks = new ILevelTask[]
            {
                new LevelTask(Strings.Tag_RedCircle, 5), 
                new LevelTask(Strings.Tag_GreenDownTriangle, 8), 
                new LevelTask(Strings.Tag_BlueMultiAngle, 12)
            };
            
            StartCoroutine(InitialAndSet());
        }

        private IEnumerator InitialAndSet()
        {
            Create();
            yield return new WaitForSeconds(0.1f);
            Setting();

        }

        public void Create()
        {
            _updateManager = new GameObject(Strings.Update_Manager).AddComponent<UpdateManager>();
            _logicManager = new GameObject(Strings.Logic_Manager).AddComponent<LogicManager>();
            GameObject empty = new GameObject("---------------");

            _notifier = new Notifier();
            _taskNotifier = new Notifier();

            _objectStorage = new ObjectStorage();
            _objectSetter = new ObjectSetter(_updateManager, _notifier);
            _inputManager = new InputManager(_notifier);
            
            _spawnManager = new SpawnManager(_objectStorage, _objectSetter);
            _checkManager = new CheckManager();
            _taskManager = new TaskManager(_levelTasks);
            
            _board = new Board(width, height, _spawnManager, _checkManager);
        }

        public void Setting()
        {
            _checkManager.Board = _board;

            _logicManager.Board = _board;
            _taskManager.Notifier = _notifier;
            _logicManager.Notifier = _taskNotifier;
            _logicManager.CheckManager = _checkManager;
            _logicManager.SpawnManager = _spawnManager;
            
            _taskManager.AddSubscriber(_logicManager);
            _inputManager.AddSubscriber(_logicManager);
            _logicManager.AddSubscriber(_taskManager);
            _updateManager.AddUpdatable(_inputManager as IUpdatable);

            _updateManager.IsUpdate = true;
        }

    }
}
