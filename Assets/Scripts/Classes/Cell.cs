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
        private GameObject _currentGameObject;

        private void Start()
        {
            _self = transform.position;
            _prevTargetX = _targetX = (int) _self.x;
            _prevTargetY = _targetY = (int) _self.y;
            _isMatched = false;
            _currentGameObject = gameObject;
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
            set { _targetX = value; }
        }

        public int TargetY
        {
            get { return _targetY; }
            set { _targetY = value; }
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
