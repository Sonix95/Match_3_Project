using System.Collections;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces.Cells;
using Mathc3Project.Interfaces.Observer;
using UnityEngine;

namespace Mathc3Project.Classes.Cells
{
    public class NormalCell : INormalCell
    {
        private const float POSITION_DELTA = 0.1f;
        private const float SPEED = 9.81f;

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

        public void DoUpdate()
        {
            Vector2 tempPos = new Vector2(_x, _y);

            if (Mathf.Abs(_x - _currentGameObject.transform.position.x) > POSITION_DELTA ||
                Mathf.Abs(_y - _currentGameObject.transform.position.y) > POSITION_DELTA)
            {
                _currentGameObject.transform.position = Vector2.Lerp(_currentGameObject.transform.position, tempPos,
                    SPEED * Time.deltaTime);
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
            if (_currentGameObject != null && _currentGameObject.CompareTag("Power"))
            {
                GameObject powerGameObject = _currentGameObject.transform.GetChild(0).transform.gameObject;
                ArrayList typeAndPos = new ArrayList();
                typeAndPos.Add(powerGameObject.tag);
                typeAndPos.Add(_currentGameObject.transform.position);
                
                _notifier.Notify(EventTypes.POWER_Use, typeAndPos);
            }
            
            GameObject.Destroy(_currentGameObject);
            _currentGameObject = null;
        }

        public override string ToString()
        {
            string message = "Ячейка [" + _x + "x" + _y + "]";
            message += " Статус: " + _canUpdate;
            message += " Текущий объект: " + (_currentGameObject != null ? _currentGameObject.tag : "отсутствует");
            if (_currentGameObject != null)
                message += " координаты объекта: " + _currentGameObject.transform.position.x + "x" +
                           _currentGameObject.transform.position.y;
            
            return message;
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