using UnityEngine;
using Mathc3Project.Interfaces;

namespace Mathc3Project
{
    public class Cell : MonoBehaviour, ICell
    {
        private Vector2 _self;
        private int _targetX;
        private int _targetY;

        private void Start()
        {
            _self = transform.position;
            _targetX = (int) _self.x;
            _targetY = (int) _self.y;
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

        public bool isUpdated { get; set; }

        public void UpdateSelf()
        {
            //TODO Add update
        }
    }
}
