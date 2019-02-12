using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Boo.Lang;
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

        public LogicManager(IBoard board)
        {
            _board = board;
        }

        private void Start()
        {
            //StartCoroutine(DetectMatchedCells());
        }
        
        private void Update()
        {
            MovePiece();
        }

        void MovePiece()
        {
            if (_cellA != null && _cellB != null)
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
                    //DetectMatch(_cellB);

                    _cellA = _cellB = null;
                    
                    MatchedCell();
                   /*

                    if (_cellA.IsMatched || _cellB.IsMatched)
                    {
                        Debug.Log("Match");
                        
                        _cellA.PrevTargetX = _cellA.TargetX;
                        _cellA.PrevTargetY = _cellA.TargetY;
                        _cellB.PrevTargetX = _cellB.TargetX;
                        _cellB.PrevTargetY = _cellB.TargetY;
                        
                        _board.Cells[_cellA.TargetX, _cellA.TargetY] = _cellA;
                        _board.Cells[_cellB.TargetX, _cellB.TargetY] = _cellB;

                        _cellA = _cellB = null;

                        StartCoroutine(DetectMatchedCells());
                    }
                    else
                    {
                        Debug.Log("Not Match");
                        
                        _cellA.TargetX = _cellA.PrevTargetX;
                        _cellA.TargetY = _cellA.PrevTargetY;
                        _cellB.TargetX = _cellB.PrevTargetX;
                        _cellB.TargetY = _cellB.PrevTargetY;
                    }
                    
                    */
                }
                
                
            }
        }

        IEnumerator DetectMatchedCells()
        {
            yield return new WaitForSeconds(.1f);
            
            MatchedCell();
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

        private void DetectMatch(ICell cell)
        {
            int column = (int) cell.TargetX;
            int row = (int) cell.TargetY;
            
            IList<ICell> horizontalCellList = new System.Collections.Generic.List<ICell>();

            if (column >= 0 && column < _board.Width - 1)
            {
                ICell sideCell;

                horizontalCellList.Add(cell);

                for (int i = column + 1; i < _board.Width; i++)
                {
                    sideCell = _board.Cells[i, row];
                    if (sideCell.Self.x - horizontalCellList[horizontalCellList.Count - 1].Self.x < 1.1f &&
                        sideCell.CurrentGameObject.tag == cell.CurrentGameObject.tag)
                        horizontalCellList.Add(sideCell);
                    else break;
                }

                for (int i = column - 1; i >= 0; i--)
                {
                    sideCell = _board.Cells[i, row];
                    if (horizontalCellList[horizontalCellList.Count - 1].Self.x - sideCell.Self.x < 1.1f &&
                        sideCell.CurrentGameObject.tag == cell.CurrentGameObject.tag)
                        horizontalCellList.Add(sideCell);
                    else break;
                }
            }

            IList<ICell> verticalUpCellList = new System.Collections.Generic.List<ICell>();
            IList<ICell> verticalDownCellList = new System.Collections.Generic.List<ICell>();

            if (row >= 0 && row < _board.Height)
            {
                ICell sideCell;
                for (int i = row + 1; i < _board.Height; i++)
                {
                    sideCell = _board.Cells[column, i];

                    if (sideCell.Self.y - _board.Cells[column, i - 1].Self.y < 1.1f &&
                        sideCell.CurrentGameObject.tag == cell.CurrentGameObject.tag)
                    {
                        verticalUpCellList.Add(sideCell);
                    }
                    else break;
                }
            }

            if (row > 0 && row < _board.Height)
            {
                ICell sideCell;
                for (int i = row - 1; i > 0; i--)
                {
                    sideCell = _board.Cells[column, i];
                    
                    if ( Mathf.Abs(_board.Cells[column, i - 1].Self.y - sideCell.Self.y)  < 1.2f &&
                        sideCell.CurrentGameObject.tag == cell.CurrentGameObject.tag)
                    {
                        verticalDownCellList.Add(sideCell);
                    }
                    else break;
                }


            }



/*
            if (horizontalCellList.Count > 2)
            {
                foreach (var cel in horizontalCellList)
                {
                    Debug.Log("sideCell[" + cel.Self.x + "x"+ cel.Self.y +"]: " + cel.CurrentGameObject.tag);
                }
            }
            */
            
                foreach (var cel in verticalDownCellList)
                {
                    Debug.Log("sideCell[" + cel.Self.x + "x"+ cel.Self.y +"]: " + cel.CurrentGameObject.tag);
                }

           
        }

        private void MatchedCell()
        {
            foreach (var cell in _board.Cells)
            {
                if (cell.IsMatched)
                {
                    SpriteRenderer render = cell.CurrentGameObject.GetComponent<SpriteRenderer>();
                    render.color = new Color(render.color.r,render.color.g,render.color.b,.2f);
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
