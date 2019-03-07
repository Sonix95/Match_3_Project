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
        public int width;
        public int height;

        private IUpdateManager _updateManager;
        private ILogicManager _logicManager;

        private INotifier _notifier;

        private IObjectStorage _objectStorage;
        private IObjectSetter _objectSetter;

        private IInputManager _inputManager;
        private ISpawnManager _spawnManager;
        private ICheckManager _checkManager;

        private IBoard _board;

        private void Start()
        {
            Create();
            Setting();
        }

        public void Create()
        {
            _updateManager = new GameObject(MagicStrings.Update_Manager).AddComponent<UpdateManager>();
            _logicManager = new GameObject(MagicStrings.Logic_Manager).AddComponent<LogicManager>();
            GameObject empty = new GameObject("---------------");

            _notifier = new Notifier();

            _objectStorage = new ObjectStorage();
            _objectSetter = new ObjectSetter(_updateManager, _notifier);

            _inputManager = new InputManager(_notifier);
            _spawnManager = new SpawnManager(_objectStorage, _objectSetter);
            _checkManager = new CheckManager();

            _board = new Board(width, height, _spawnManager, _checkManager);
        }

        public void Setting()
        {
            _checkManager.Board = _board;

            _logicManager.Board = _board;
            _logicManager.CheckManager = _checkManager;
            _logicManager.SpawnManager = _spawnManager;

            _inputManager.AddSubscriber(_logicManager);
            _updateManager.AddUpdatable(_inputManager as IUpdatable);

            _updateManager.IsUpdate = true;
        }

    }
}
