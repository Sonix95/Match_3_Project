using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;

namespace Mathc3Project
{
    class LogicManager : MonoBehaviour, ILogicManager
    {
        private IBoard _board;
        private ICommand _command;

        private Vector3 _firstClick;
        private Vector3 _lastClick;

        private ICell _cell;

        private const float SWIPE_SENSTIVITY = 0.3f;

        private void Start()
        {
            StartCoroutine(StartDetect());
        }

        public void SetCommand(ICommand command)
        {
            _command = command;
        }

        public void ExecuteCommand()
        {
            _command.Execute();
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

                    if (Mathf.Abs(_lastClick.x - _firstClick.x) > SWIPE_SENSTIVITY ||
                        Mathf.Abs(_lastClick.y - _firstClick.y) > SWIPE_SENSTIVITY)
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
                        Debug.Log(" cell[" + i + "," + j + "] " + _board.Cells[i, j]);
                    }

                    break;

                case EventTypeEnum.MoveUp:
                    if (_cell != null)
                    {
                        ICommand moveUpCommand = new MoveUpCommand(_cell);
                        SetCommand(moveUpCommand);
                        ExecuteCommand();
                    }

                    break;

                case EventTypeEnum.MoveDown:
                    if (_cell != null)
                    {
                        ICommand moveDownCommand = new MoveDownCommand(_cell);
                        SetCommand(moveDownCommand);
                        ExecuteCommand();
                    }

                    break;

                case EventTypeEnum.MoveRight:
                    if (_cell != null)
                    {
                        ICommand moveRightCommand = new MoveRightCommand(_cell);
                        SetCommand(moveRightCommand);
                        ExecuteCommand();
                    }

                    break;

                case EventTypeEnum.MoveLeft:
                    if (_cell != null)
                    {
                        ICommand moveLeftCommand = new MoveLeftCommand(_cell);
                        SetCommand(moveLeftCommand);
                        ExecuteCommand();
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
                    SwipeCells(moveDirectionType, xPos, yPos);
                }
            }
        }

        private void DetectMatch(ICell cell)
        {
            CheckMatchByLine(LineDirectionEnum.Horizontal, cell);
            CheckMatchByLine(LineDirectionEnum.Vertical, cell);
        }

        private void CheckMatchByLine(LineDirectionEnum lineDirection, ICell currentCell)
        {
            int column = currentCell.TargetX;
            int row = currentCell.TargetY;

            IList<ICell> sideAList = new List<ICell>();
            IList<ICell> sideBList = new List<ICell>();

            ICell sideCell;

            switch (lineDirection)
            {
                case LineDirectionEnum.Horizontal:
                    if (column > 0 && column < _board.Width)
                    {
                        for (int i = column - 1; i >= 0; i--)
                        {
                            sideCell = _board.Cells[i, row];

                            if (sideCell != null)
                                if (sideCell.CurrentGameObject.CompareTag(currentCell.CurrentGameObject.tag))
                                    sideAList.Add(sideCell);
                                else
                                    break;
                        }
                    }

                    if (column >= 0 && column < _board.Width)
                    {
                        for (int i = column + 1; i < _board.Width; i++)
                        {
                            sideCell = _board.Cells[i, row];

                            if (sideCell != null)
                                if (sideCell.CurrentGameObject.CompareTag(currentCell.CurrentGameObject.tag))
                                    sideBList.Add(sideCell);
                                else
                                    break;
                        }
                    }

                    break;

                case LineDirectionEnum.Vertical:
                    if (row >= 0 && row < _board.Height)
                    {
                        for (int i = row + 1; i < _board.Height; i++)
                        {
                            sideCell = _board.Cells[column, i];

                            if (sideCell != null)
                                if (sideCell.CurrentGameObject.CompareTag(currentCell.CurrentGameObject.tag))
                                    sideAList.Add(sideCell);
                                else
                                    break;
                        }
                    }

                    if (row > 0 && row < _board.Height)
                    {
                        for (int i = row - 1; i >= 0; i--)
                        {
                            sideCell = _board.Cells[column, i];

                            if (sideCell != null)
                                if (sideCell.CurrentGameObject.CompareTag(currentCell.CurrentGameObject.tag))
                                    sideBList.Add(sideCell);
                                else
                                    break;
                        }
                    }

                    break;
            }

            if (sideAList.Count + sideBList.Count > 1)
            {
                foreach (var cell in sideAList)
                    cell.IsMatched = true;
                foreach (var cell in sideBList)
                    cell.IsMatched = true;

                currentCell.IsMatched = true;
            }
        }

        private void MatchedCell()
        {
            foreach (var cell in _board.Cells)
            {
                if (cell != null)
                {
                    if (cell.IsMatched)
                    {
                        SpriteRenderer render = cell.CurrentGameObject.GetComponent<SpriteRenderer>();
                        render.color = new Color(render.color.r, render.color.g, render.color.b, .2f);
                    }
                }
            }

            StartCoroutine(DestroyMatched());
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

        private void SwipeCells(MoveDirectionType directionType, int xPos, int yPos)
        {
            ICell tempCell = null;

            _cell = _board.Cells[xPos, yPos];

            switch (directionType)
            {
                case MoveDirectionType.Right:
                    if (xPos < _board.Width - 1)
                    {
                        OnEvent(EventTypeEnum.MoveRight, null);

                        tempCell = _board.Cells[xPos + 1, yPos];
                        _board.Cells[xPos + 1, yPos] = _cell;

                        _cell = tempCell;
                        OnEvent(EventTypeEnum.MoveLeft, null);
                    }

                    break;

                case MoveDirectionType.Left:
                    if (xPos > 0)
                    {
                        OnEvent(EventTypeEnum.MoveLeft, null);

                        tempCell = _board.Cells[xPos - 1, yPos];
                        _board.Cells[xPos - 1, yPos] = _cell;

                        _cell = tempCell;
                        OnEvent(EventTypeEnum.MoveRight, null);
                    }

                    break;

                case MoveDirectionType.Up:
                    if (yPos < _board.Height - 1)
                    {
                        OnEvent(EventTypeEnum.MoveUp, null);

                        tempCell = _board.Cells[xPos, yPos + 1];
                        _board.Cells[xPos, yPos + 1] = _cell;

                        _cell = tempCell;
                        OnEvent(EventTypeEnum.MoveDown, null);
                    }

                    break;

                case MoveDirectionType.Down:
                    if (yPos > 0)
                    {
                        OnEvent(EventTypeEnum.MoveDown, null);

                        tempCell = _board.Cells[xPos, yPos - 1];
                        _board.Cells[xPos, yPos - 1] = _cell;

                        _cell = tempCell;
                        OnEvent(EventTypeEnum.MoveUp, null);
                    }

                    break;
            }

            _board.Cells[xPos, yPos] = _cell;
        }

        IEnumerator StartDetect()
        {
            yield return new WaitForSeconds(.2f);
            foreach (var cell in _board.Cells)
            {
                if (cell != null)
                    if (!cell.IsMatched)
                        DetectMatch(cell);
            }

            MatchedCell();
        }

        IEnumerator DestroyMatched()
        {
            yield return new WaitForSeconds(.2f);

            foreach (var cell in _board.Cells)
            {
                if (cell != null)
                {
                    if (cell.IsMatched)
                        Destroy(cell.CurrentGameObject);
                }
            }

            for (int i = 0; i < _board.Width; i++)
            for (int j = 0; j < _board.Height; j++)
            {
                if (_board.Cells[i, j] != null)
                    if (_board.Cells[i, j].IsMatched)
                        _board.Cells[i, j] = null;
            }
        }

        public IBoard Board
        {
            get { return _board; }
            set { _board = value; }
        }

    }
}
