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
        private CellTypesEnum _cellTypeEnum;
        private CellStatesEnum _cellStatesEnum;
        private GameObject _currentGameObject;
        private INotifier _notifier;
        private bool _canUpdate;

        public NormalCell(int x, int y)
        {
            _x = x;
            _y = y;
            _cellTypeEnum = CellTypesEnum.Normal;
            _cellStatesEnum = CellStatesEnum.Wait;
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

                switch (CellStateEnum)
                {
                    case CellStatesEnum.Swipe:
                        _notifier.Notify(EventTypesEnum.CELL_EndMove, this);
                        break;
                    case CellStatesEnum.Back:
                        _notifier.Notify(EventTypesEnum.CELL_EndMoveBack, this);
                        break;
                    case CellStatesEnum.Fall:
                        _notifier.Notify(EventTypesEnum.CELL_Fall, this);
                        break;
                }
            }
        }

        public void DoAfterMatch()
        {
            if (_currentGameObject != null)
            {
                if (_currentGameObject.CompareTag(Strings.TAG_POWER))
                {
                    GameObject powerGameObject = _currentGameObject.transform.GetChild(0).transform.gameObject;
                
                    ArrayList typeAndPos = new ArrayList();
                    typeAndPos.Add(powerGameObject.tag);
                    typeAndPos.Add(_currentGameObject.transform.position);

                    _notifier.Notify(EventTypesEnum.POWER_Use, typeAndPos);
                }
                else
                    _notifier.Notify(EventTypesEnum.CELL_Destroy, _currentGameObject.transform.tag);
            }

            
            GameObject.Destroy(_currentGameObject);
            _currentGameObject = null;
        }

        public override string ToString()
        {
            string message = Strings.NORMAL_CELL + _x + "x" + _y;
            message += ", State: " + _cellStatesEnum;
            message += ", Update?: " + _canUpdate;
            message += ", Current GO: " + (_currentGameObject != null ? _currentGameObject.tag : "missing");

            return message;
        }
        
        public void Notify(EventTypesEnum eventTypeEnum, Object messageData)
        {
            _notifier.Notify(eventTypeEnum, messageData);
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

        public CellTypesEnum CellTypeEnum
        {
            get { return _cellTypeEnum; }
            set { _cellTypeEnum = value; }
        }

        public CellStatesEnum CellStateEnum
        {
            get { return _cellStatesEnum; }
            set { _cellStatesEnum = value; }
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