using System;
using Mathc3Project.Enums;
using UnityEngine;
using Mathc3Project.Interfaces;

namespace Mathc3Project
{
    public class Cell : MonoBehaviour, ICell
    {
        #region Fields
        
        private const float SPEED = 9.81f;
        private const float POSITION_DELTA = 0.01f;
        
        private CellTypes _cellTypes;
        private GameObject _currentGameObject;
        
        private int _targetX;
        private int _targetY;
        
        private bool _isMatched;
        
        private bool _isMoving;
        private bool _isMovingBack;
        private bool _isFall;
        
        private INotifier _notifier;
        
        #endregion
        
        private void Start()
        {
            _targetX = (int) transform.position.x;
            _targetY = (int) transform.position.y;
        }

        private void Update()
        {
            if (_isMoving)
                Move();
        }

        public void Move()
        {
            Vector2 tempPos = new Vector2(_targetX, _targetY);

            if (Mathf.Abs(_targetX - transform.position.x) > POSITION_DELTA ||
                Mathf.Abs(_targetY - transform.position.y) > POSITION_DELTA)
            {
                transform.position = Vector2.Lerp(transform.position, tempPos, SPEED * Time.deltaTime);
            }
            else
            {
                transform.position = tempPos;
                _isMoving = false;

                if (_isFall)
                {
                    _isFall = false;
                    _notifier.Notify(EventTypeEnum.CELL_fall, this);
                }
                else if (_isMovingBack)
                {
                    _isMovingBack = false;
                    _notifier.Notify(EventTypeEnum.CELL_endingMoveBack, this);
                }
                else
                {
                    _notifier.Notify(EventTypeEnum.CELL_endingMove, this);
                }
            }
        }
       
        private void OnDestroy()
        {
            _notifier.Notify(EventTypeEnum.CELL_destroyed, _currentGameObject.tag);
        }
        
        public void AddSubscriber(ISubscriber subscriber)
        {
            _notifier.AddSubscriber(subscriber);
        }
        
        #region Properties
        
        public CellTypes CellTypes
        {
            get { return _cellTypes; }
            set { _cellTypes = value; }
        }

        public string Tag
        {
            get { return gameObject.tag; }
        }
        
        public GameObject CurrentGameObject
        {
            get { return _currentGameObject; }
            set { _currentGameObject = value; }
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

        public bool IsMatched
        {
            get { return _isMatched; }
            set { _isMatched = value; }
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

        public bool IsFall
        {
            get { return _isFall; }
            set { _isFall = value; }
        }
        
        public INotifier Notifier
        {
            get { return _notifier; }
            set { _notifier = value; }
        }
        
        #endregion
    }
}
