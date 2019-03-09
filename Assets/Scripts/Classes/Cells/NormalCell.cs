using System.Collections;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces.Cells;
using Mathc3Project.Interfaces.Observer;
using UnityEngine;

namespace Mathc3Project.Classes.Cells
{
    public class NormalCell : INormalCell
    {
        private int _x;
        private int _y;
        private CellTypes _cellType;
        private CellStates _cellStates;
        private GameObject _currentGameObject;
        private INotifier _notifier;
        private bool _canUpdate;

        public NormalCell(int x, int y)
        {
            _x = x;
            _y = y;
            _cellType = CellTypes.Normal;
            _cellStates = CellStates.Wait;
        }

        public void Move()
        {
            _canUpdate = true;
        }

        public void CustomUpdate()
        {
            Vector2 tempPos = new Vector2(_x, _y);

            if (Mathf.Abs(_x - _currentGameObject.transform.position.x) > Strings.POSITION_DELTA ||
                Mathf.Abs(_y - _currentGameObject.transform.position.y) > Strings.POSITION_DELTA)
            {
                _currentGameObject.transform.position = Vector2.Lerp(_currentGameObject.transform.position, tempPos,
                    Strings.CELL_SPEED * Time.deltaTime);
            }
            else
            {
                _currentGameObject.transform.position = tempPos;
                _canUpdate = false;

                switch (CellState)
                {
                    case CellStates.Swipe:
                        _notifier.Notify(EventTypes.CELL_EndMove, this);
                        break;
                    case CellStates.Back:
                        _notifier.Notify(EventTypes.CELL_EndMoveBack, this);
                        break;
                    case CellStates.Fall:
                        _notifier.Notify(EventTypes.CELL_Fall, this);
                        break;
                }
            }
        }

        public void DoAfterMatch()
        {
            if (_currentGameObject != null)
            {
                if (_currentGameObject.CompareTag(Strings.Tag_Power))
                {
                    GameObject powerGameObject = _currentGameObject.transform.GetChild(0).transform.gameObject;
                
                    ArrayList typeAndPos = new ArrayList();
                    typeAndPos.Add(powerGameObject.tag);
                    typeAndPos.Add(_currentGameObject.transform.position);

                    _notifier.Notify(EventTypes.POWER_Use, typeAndPos);
                }
                else
                    _notifier.Notify(EventTypes.CELL_Destroy, _currentGameObject.transform.tag);
            }

            
            GameObject.Destroy(_currentGameObject);
            _currentGameObject = null;
        }

        public override string ToString()
        {
            string message = Strings.Normal_Cell + _x + "x" + _y;
            message += ", State: " + _cellStates;
            message += ", Update?: " + _canUpdate;
            message += ", Current GO: " + (_currentGameObject != null ? _currentGameObject.tag : "missing");

            return message;
        }
        
        public void Notify(EventTypes eventType, Object messageData)
        {
            _notifier.Notify(eventType, messageData);
        }

        public int TargetX
        {
            get { return _x; }
            set
            {
                if (value < 0) value = 0;
                _x = value;
            }
        }

        public int TargetY
        {
            get { return _y; }
            set
            {
                if (value < 0) value = 0;
                _y = value;
            }
        }

        public CellTypes CellType
        {
            get { return _cellType; }
            set { _cellType = value; }
        }

        public CellStates CellState
        {
            get { return _cellStates; }
            set { _cellStates = value; }
        }

        public GameObject CurrentGameObject
        {
            get { return _currentGameObject; }
            set { _currentGameObject = value; }
        }

        public bool canUpdate
        {
            get { return _canUpdate; }
            set { _canUpdate = value; }
        }

        public INotifier Notifier
        {
            get { return _notifier; }
            set { _notifier = value; }
        }
    }
}