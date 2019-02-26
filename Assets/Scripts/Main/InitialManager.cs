using UnityEngine;
using Mathc3Project.Interfaces;

namespace Mathc3Project
{
    public class InitialManager : MonoBehaviour
    {
        [SerializeField] private int _boardWidth;
        [SerializeField] private int _boardHeight;

        private IBoard _board;
        private INotifier _notifier;
        private IObjectStorage _objectStorage;
        private IObjectSetter _objectSetter;
        private ILogicManager _logicManager;
        private IInputManager _inputManager;
        private ISpawnManager _spawnManager;
        private ICheckManager _checkManager;
        private ICameraManager _cameraManager;

        private void Start()
        {
            Initial();
            SetUp();
        }

        private void Initial()
        {
            _logicManager = new GameObject("Logic Manager").AddComponent<LogicManager>();
            _inputManager = new GameObject("Input Manager").AddComponent<InputManager>();
            GameObject emptyGO = new GameObject("--------------");
            
            _cameraManager = new CameraManager(_boardWidth,_boardHeight);
            _notifier = new Notifier();
            _objectStorage = new ObjectStorage();
            _objectSetter = new ObjectSetter(_logicManager,_notifier);
            _spawnManager = new SpawnManager(_objectStorage,_objectSetter);
            _checkManager = new CheckManager();
            _board = new Board(_boardWidth, _boardHeight, _spawnManager,_checkManager);
        }

        private void SetUp()
        {
            _inputManager.Notifier = _notifier;
            _inputManager.AddSubscriber(_logicManager);
            _logicManager.Board = _board;
        }

    }
}
