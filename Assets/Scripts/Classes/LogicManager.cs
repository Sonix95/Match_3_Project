using UnityEngine;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;

namespace Mathc3Project
{
    class LogicManager : MonoBehaviour, ILogicManager
    {
        private IBoard _board;

        private Vector3 _firstClick;
        private Vector3 _lastClick;

        private ICell _cellA;
        private ICell _cellB;

        private Vector2 _tempPosA;
        private Vector2 _tempPosB;

        public LogicManager(IBoard board)
        {
            _board = board;
        }

        private void Update()
        {
            MovePiece();
        }

        void MovePiece()
        {
            if (_cellA != null && _cellB != null)
            {
                if (Mathf.Abs(_cellA.TargetX - _cellA.Self.x) > .1f || Mathf.Abs(_cellA.TargetY - _cellA.Self.y) > .1f)
                {
                    _tempPosA = new Vector2(_cellA.TargetX, _cellA.TargetY);
                    _tempPosB = new Vector2(_cellB.TargetX, _cellB.TargetY);

                    _cellA.Self = Vector2.Lerp(_cellA.Self, _tempPosA, .3f);
                    _cellB.Self = Vector2.Lerp(_cellB.Self, _tempPosB, .3f);
                }
                else
                {
                    _tempPosA = new Vector2(_cellA.TargetX, _cellA.TargetY);
                    _tempPosB = new Vector2(_cellB.TargetX, _cellB.TargetY);

                    _cellA.Self = new Vector2(_cellA.TargetX, _cellA.TargetY);
                    _cellB.Self = new Vector2(_cellB.TargetX, _cellB.TargetY);

                    _board.Cells[_cellA.TargetX, _cellA.TargetY] = _cellA;
                    _board.Cells[_cellB.TargetX, _cellB.TargetY] = _cellB;

                    _cellA = _cellB = null;
                }
            }
        }

        public void OnEvent(EventTypeEnum eventTypeEnum, object messageData)
        {
            MoveDirectionType moveDirectionType = MoveDirectionType.None;

            switch (eventTypeEnum)
            {
                case EventTypeEnum.MouseDown:
                    _firstClick = (Vector3) messageData;
                    break;

                case EventTypeEnum.MouseUp:
                    _lastClick = (Vector3) messageData;

                    if (Mathf.Abs(_lastClick.x - _firstClick.x) > 0.3f ||
                        Mathf.Abs(_lastClick.y - _firstClick.y) > 0.3f)
                    {
                        float angle = Mathf.Atan2(_lastClick.y - _firstClick.y, _lastClick.x - _firstClick.x) * 180 /
                                      Mathf.PI;
                        moveDirectionType = FindDirection(angle);
                    }

                    break;

                case EventTypeEnum.CellsInfo:
                    for (int i = 0; i < _board.Width; i++)
                    for (int j = 0; j < _board.Height; j++)
                    {
                        Debug.Log(" cell[" + i + "," + j + "] " + _board.Cells[i, j].ToString());
                    }

                    break;

                default:
                    Debug.Log("EVENT NOT FOUND!!!");
                    break;
            }

            if (moveDirectionType != MoveDirectionType.None)
            {
                int xPos = (int) Mathf.Round(_firstClick.x);
                int yPos = (int) Mathf.Round(_firstClick.y);

                if (xPos >= 0 && xPos < _board.Width && yPos >= 0 && yPos < _board.Height)
                {
                    GetTwoCellsByDirection(moveDirectionType, xPos, yPos);
                    SetCellsTarget(moveDirectionType, _cellA, _cellB);
                }
            }
        }

        private MoveDirectionType FindDirection(float angle)
        {
            MoveDirectionType moveDirectionType = MoveDirectionType.None;

            if (angle > -45 && angle <= 45)
            {
                moveDirectionType = MoveDirectionType.Right;
            }
            else if (angle > 45 && angle <= 135)
            {
                moveDirectionType = MoveDirectionType.Up;
            }
            else if (angle > 135 || angle <= -135)
            {
                moveDirectionType = MoveDirectionType.Left;
            }
            else if (angle >= -135 && angle < -45)
            {
                moveDirectionType = MoveDirectionType.Down;
            }

            return moveDirectionType;
        }

        private void GetTwoCellsByDirection(MoveDirectionType directionType, int xPos, int yPos)
        {
            _cellA = _board.Cells[xPos, yPos];

            switch (directionType)
            {
                case MoveDirectionType.Right:
                    if (xPos < _board.Width - 1)
                        _cellB = _board.Cells[xPos + 1, yPos];
                    break;
                case MoveDirectionType.Left:
                    if (xPos > 0)
                        _cellB = _board.Cells[xPos - 1, yPos];
                    break;
                case MoveDirectionType.Up:
                    if (yPos < _board.Height - 1)
                        _cellB = _board.Cells[xPos, yPos + 1];
                    break;
                case MoveDirectionType.Down:
                    if (yPos > 0)
                        _cellB = _board.Cells[xPos, yPos - 1];
                    break;
            }
        }

        private void SetCellsTarget(MoveDirectionType directionType, ICell cellA, ICell cellB)
        {
            switch (directionType)
            {
                case MoveDirectionType.Right:
                    _cellB.TargetX -= 1;
                    _cellA.TargetX += 1;
                    break;
                case MoveDirectionType.Left:
                    _cellB.TargetX += 1;
                    _cellA.TargetX -= 1;
                    break;
                case MoveDirectionType.Up:
                    _cellB.TargetY -= 1;
                    _cellA.TargetY += 1;
                    break;
                case MoveDirectionType.Down:
                    _cellB.TargetY += 1;
                    _cellA.TargetY -= 1;
                    break;
            }
        }

        public IBoard Board
        {
            get { return _board; }
            set { _board = value; }
        }
    }
}
