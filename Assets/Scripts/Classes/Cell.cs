using System;
using UnityEngine;
using Mathc3Project.Interfaces;

namespace Mathc3Project
{
    public class Cell : MonoBehaviour, ICell
    {
        private Vector2 _self;
        private int _targetX;
        private int _targetY;
        private int _prevTargetX;
        private int _prevTargetY;
        private bool _isMatched;
        private bool _isMoving;
        private GameObject _currentGameObject;

        private void Start()
        {
            _self = transform.position;
            _prevTargetX = _targetX = (int) _self.x;
            _prevTargetY = _targetY = (int) _self.y;
            _isMatched = false;
            _isMoving = false;
            _currentGameObject = gameObject;
        }

        private void Update()
        {
            if (_isMoving)
                Move();
        }

        public void Move()
        {
            Vector2 tempPos = new Vector2(_targetX , _targetY);
            if (Mathf.Abs(_targetX - transform.position.x) > .01f || Mathf.Abs(_targetY - transform.position.y) > .01f)
            {
                transform.position = Vector2.Lerp(transform.position, tempPos, .3f);
            }
            else
            {
                transform.position = tempPos;
                _isMoving = false;
            }
        }

        public bool IsMoving
        {
            get { return _isMoving; }
            set { _isMoving = value; }
        }

        public Vector2 Self
        {
            get { return _self; }
            set
            {
                _self = value;
                transform.position = _self;
            }
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

        public int PrevTargetX
        {
            get { return _prevTargetX; }
            set { _prevTargetX = value; }
        }

        public int PrevTargetY
        {
            get { return _prevTargetY; }
            set { _prevTargetY = value; }
        } 
        
        public string Name
        {
            get { return gameObject.name; }
            set { gameObject.name = value; }
        } 
        
        public bool IsMatched
        {
            get { return _isMatched; }
            set { _isMatched = value; }
        } 
        
        public GameObject CurrentGameObject
        {
            get { return _currentGameObject; }
            set { _currentGameObject = value; }
        }
       
    }
}
