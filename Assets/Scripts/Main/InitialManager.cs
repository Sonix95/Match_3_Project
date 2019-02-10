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
        private ILogicManager _logicManager;
        private IInputManager _inputManager;

        private void Start()
        {
            _notifier = new Notifier();
            _logicManager = new GameObject("Logic Manager").AddComponent<LogicManager>();
            _inputManager = new GameObject("Input Manager").AddComponent<InputManager>();
            _inputManager.Notifier = _notifier;
            _inputManager.AddSubscriber(_logicManager);

            GameObject emptyGO = new GameObject("--------------");

            _board = new Board(_boardWidth, _boardHeight);
            _logicManager.Board = _board;
        }

    }
}
