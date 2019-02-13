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
        private ISpawnManager _spawnManager;

        private Vector3 _firstClick;
        private Vector3 _lastClick;

        private ICell _cellA;
        private ICell _cellB;

        private void Start()
        {
            StartCoroutine(StartDetect());
        }

        private void Update()
        {
            if (_cellA != null && _cellB != null)
            {
                MovePiece();
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
        
        void MovePiece()
        {
            Vector2 tempPosA = new Vector2(_cellA.TargetX, _cellA.TargetY);
            Vector2 tempPosB = new Vector2(_cellB.TargetX, _cellB.TargetY);

            if (Mathf.Abs(_cellA.TargetX - _cellA.Self.x) > .1f || Mathf.Abs(_cellA.TargetY - _cellA.Self.y) > .1f)
            {
                _cellA.Self = Vector2.Lerp(_cellA.Self, tempPosA, .3f);
                _cellB.Self = Vector2.Lerp(_cellB.Self, tempPosB, .3f);
            }
            else
            {
                _cellA.Self = tempPosA;
                _cellB.Self = tempPosB;

                _board.Cells[_cellA.TargetX, _cellA.TargetY] = _cellA;
                _board.Cells[_cellB.TargetX, _cellB.TargetY] = _cellB;

                DetectMatch(_cellA);
                DetectMatch(_cellB);

                if (!_cellA.IsMatched && !_cellB.IsMatched)
                { 
                    _cellA.TargetX = _cellA.PrevTargetX;
                    _cellA.TargetY = _cellA.PrevTargetY;
                    _cellB.TargetX = _cellB.PrevTargetX;
                    _cellB.TargetY = _cellB.PrevTargetY;

                    if (Mathf.Abs(_cellA.TargetX - _cellA.Self.x) > .1f ||
                        Mathf.Abs(_cellA.TargetY - _cellA.Self.y) > .1f)
                    {
                        return;
                    }
                }
                else
                {
                    _cellA.PrevTargetX = _cellA.TargetX;
                    _cellA.PrevTargetY = _cellA.TargetY;
                    _cellB.PrevTargetX = _cellB.TargetX;
                    _cellB.PrevTargetY = _cellB.TargetY;
                }
                
                _cellA = _cellB = null;
                
                MatchedCell();
            }
        }

        private void DetectMatch(ICell cell)
        {
            CheckMatchByLine(LineDirectionEnum.Horizontal, cell);
            CheckMatchByLine(LineDirectionEnum.Vertical, cell);
        }

        private void CheckMatchByLine(LineDirectionEnum lineDirection ,ICell currentCell)
        { 
            int column = currentCell.TargetX;
            int row =  currentCell.TargetY;
            
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

                            if (sideCell != null && sideCell.CurrentGameObject.CompareTag(currentCell.CurrentGameObject.tag))
                                sideAList.Add(sideCell);
                            else break;
                        }
                    }
                    if (column >= 0 && column < _board.Width)
                    {
                        for (int i = column + 1; i < _board.Width; i++)
                        {
                            sideCell = _board.Cells[i, row];

                            if (sideCell != null && sideCell.CurrentGameObject.CompareTag(currentCell.CurrentGameObject.tag))
                                sideBList.Add(sideCell);
                            else break;
                        }
                    }
                    break;
                
                case LineDirectionEnum.Vertical:
                    if (row >= 0 && row < _board.Height)
                    {
                        for (int i = row + 1; i < _board.Height; i++)
                        {
                            sideCell = _board.Cells[column, i];

                            if (sideCell != null && sideCell.CurrentGameObject.CompareTag(currentCell.CurrentGameObject.tag))
                                sideAList.Add(sideCell);
                            else break;
                        }
                    }
                    if (row > 0 && row < _board.Height)
                    { 
                        for (int i = row - 1; i >= 0; i--)
                        {
                            sideCell = _board.Cells[column, i];
                    
                            if(sideCell != null && sideCell.CurrentGameObject.CompareTag(currentCell.CurrentGameObject.tag))
                                sideBList.Add(sideCell);
                            else break;
                        }
                    }
                    break;
            }
            
            if (sideAList.Count + sideBList.Count  > 1)
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
                if (cell.CurrentGameObject != null && cell.IsMatched)
                {
                    SpriteRenderer render = cell.CurrentGameObject.GetComponent<SpriteRenderer>();
                    render.color = new Color(render.color.r,render.color.g,render.color.b,.2f);
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
        
        IEnumerator StartDetect()
        {
            yield return new WaitForSeconds(.2f);
            foreach (var cell in _board.Cells)
            {
                if(cell != null && !cell.IsMatched)
                    DetectMatch(cell);
            }
            MatchedCell();
        }
        
        IEnumerator DestroyMatched()
        {
            yield return new WaitForSeconds(.2f);

            foreach (var cell in _board.Cells)
            {
                if(cell.IsMatched)
                    Destroy(cell.CurrentGameObject);
            }
        }

        public IBoard Board
        {
            get { return _board; }
            set { _board = value; }
        } 
        
        public ISpawnManager SpawnManager
        {
            get { return _spawnManager; }
            set { _spawnManager = value; }
        }
    }
}
