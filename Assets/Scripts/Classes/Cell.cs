using System;
using Mathc3Project.Enums;
using UnityEngine;
using Mathc3Project.Interfaces;

namespace Mathc3Project
{
    public class Cell : MonoBehaviour, ICell
    {
        private INotifier _notifier;
        private GameObject _currentGameObject;
        private int _targetX;
        private int _targetY;
        private bool _isMoving;
        private bool _isMovingBack;
        private bool _isMatched;

        private void Start()
        {
            _currentGameObject = gameObject;
            _targetX = (int) transform.position.x;
            _targetY = (int) transform.position.y;
            _isMoving = _isMovingBack = false;
            _isMatched = false;
        }

        private void Update()
        {
            if (_isMoving)
                Move();
        }

        public void Move()
        {
            Vector2 tempPos = new Vector2(_targetX, _targetY);

            if (Mathf.Abs(_targetX - transform.position.x) > .1f || Mathf.Abs(_targetY - transform.position.y) > .1f)
            {
                transform.position = Vector2.Lerp(transform.position, tempPos, .3f);
            }
            else
            {
                transform.position = tempPos;
                _isMoving = false;
                if (!_isMovingBack)
                    _notifier.Notify(EventTypeEnum.CELL_endingMove, this);
                else
                {
                    _isMovingBack = false;
                    _notifier.Notify(EventTypeEnum.CELL_endingMoveBack, this);
                }
            }
        }

        public void AddSubscriber(ISubscriber subscriber)
        {
            _notifier.AddSubscriber(subscriber);
        }

        public INotifier Notifier
        {
            get { return _notifier; }
            set { _notifier = value; }
        }

        public string Name
        {
            get { return gameObject.name; }
            set { gameObject.name = value; }
        }

        public GameObject CurrentGameObject
        {
            get { return _currentGameObject; }
            set { _currentGameObject = value; }
        }

        public Vector2 Self
        {
            get { return transform.position; }
            set { transform.position = value; }
        }

        public int TargetX
        {
            get { return _targetX; }
            set
            {
                if (value < 0) value = 0;
                _targetX = value;
            }
        }

        public int TargetY
        {
            get { return _targetY; }
            set
            {
                if (value < 0) value = 0;
                _targetY = value;
            }
        }

        public bool IsMoving
        {
            get { return _isMoving; }
            set { _isMoving = value; }
        }

        public bool IsMovingBack
        {
            get { return _isMovingBack; }
            set { _isMovingBack = value; }
        }

        public bool IsMatched
        {
            get { return _isMatched; }
            set { _isMatched = value; }
        }
    }
}
