 using System;
using System.Collections;
using System.Collections.Generic;
using Mathc3Project.Commands;
using UnityEngine;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using UnityEditor;
 using Object = System.Object;

 namespace Mathc3Project
 {
     class LogicManager : MonoBehaviour, ILogicManager
     {
         private const float SWIPE_SENSTIVITY = 0.3f;

         private IBoard _board;
         private ICommand _macroCommand;

         private Vector3 _clickA;
         private Vector3 _clickB;
         
         private bool _isMarket = false;
         private int _TryCheckedCounter = 0;
         
         private void Start()
         {
             StartCoroutine(FirstTryChecks());
         }

         //TODO ADD method to generate unique board cells and after delete THIS method
         IEnumerator FirstTryChecks()
         {
             yield return new WaitForSeconds(.2f);

             foreach (var cell in _board.Cells)
             {
                 CheckCell(cell);
                 MarkMatchedCell(cell);
             }

             yield return new WaitForSeconds(.4f);

             for (int i = 0; i < _board.Width; i++)
             for (int j = 0; j < _board.Height; j++)
             {
                 if (_board.Cells[i, j] != null && _board.Cells[i, j].IsMatched)
                 {
                     Destroy(_board.Cells[i, j].CurrentGameObject);
                     _board.Cells[i, j] = null;
                 }
             }

             yield return new WaitForSeconds(.4f);

             DecrieaseRow();
         }

         private void DecrieaseRow()
         {
             int nullCount = 0;
             for (int i = 0; i < _board.Width; i++)
             {
                 for (int j = 0; j < _board.Height; j++)
                 {
                     if (_board.Cells[i, j] == null)
                         nullCount++;
                     else if (nullCount > 0)
                     {
                         ICommand[] commands = {new FallCommand(_board.Cells[i, j], nullCount)};
                         SetMacroCommand(commands);

                         OnEvent(EventTypeEnum.BOARD_collapse, new int[] {i, j});
                     }
                 }

                 nullCount = 0;
             }
         }

         public void OnEvent(EventTypeEnum eventTypeEnum, object messageData)
         {
             switch (eventTypeEnum)
             {
                 case EventTypeEnum.MOUSE_down:
                     _clickA = (Vector3) messageData;
                     break;

                 case EventTypeEnum.MOUSE_up:
                     _clickB = (Vector3) messageData;

                     if (Mathf.Abs(_clickB.x - _clickA.x) > SWIPE_SENSTIVITY ||
                         Mathf.Abs(_clickB.y - _clickA.y) > SWIPE_SENSTIVITY)
                     {
                         MoveDirectionType swipeDirection = GetDirection(_clickA, _clickB);
                         SwipeCells(swipeDirection);
                     }
                     else
                     {
                         int Xpos = (int) _clickA.x;
                         int Ypos = (int) _clickA.y;

                         if (Xpos >= 0 && Ypos >= 0 && Xpos < _board.Width && Ypos < _board.Height)
                         {
                             Debug.Log(_board.Cells[Xpos, Ypos]);
                         }
                     }

                     break;

                 case EventTypeEnum.MOVE_up:
                     ExecuteMacroCommand();
                     break;

                 case EventTypeEnum.MOVE_down:
                     ExecuteMacroCommand();
                     break;

                 case EventTypeEnum.MOVE_left:
                     ExecuteMacroCommand();
                     break;

                 case EventTypeEnum.MOVE_right:
                     ExecuteMacroCommand();
                     break;

                 case EventTypeEnum.CELL_endingMove:
                     TryCheckCell((ICell) messageData);
                     break;

                 case EventTypeEnum.CELL_endingMoveBack:
                     ICell cellAfterBack = (ICell) messageData;

                     _board.Cells[cellAfterBack.TargetX, cellAfterBack.TargetY] = cellAfterBack;
                     break;
                 case EventTypeEnum.CELL_fall:
                     ICell cellAfterFall = (ICell) messageData;
                     
                     _board.Cells[cellAfterFall.TargetX, cellAfterFall.TargetY] = cellAfterFall;
                     TryCheckCell((ICell) messageData);
                     break;

                 case EventTypeEnum.BOARD_collapse:
                     int[] cellParams = (int[]) messageData;

                     ExecuteMacroCommand();
                     _board.Cells[cellParams[0], cellParams[1]] = null;
                     break;

                 case EventTypeEnum.UTILITY_boardCellsInfo:
                     foreach (var cell in _board.Cells)
                     {
                         Debug.Log(cell);
                     }

                     break;

                 default:
                     Debug.Log("EVENT NOT FOUND!!!");
                     break;
             }
         }

         private void TryCheckCell(ICell cell)
         { 
             _TryCheckedCounter++;
             if (_TryCheckedCounter > 2)
             {
                 _TryCheckedCounter = 1;
                 _isMarket = false;
             }

             CheckCell(cell);

             if (cell.IsMatched || _isMarket)
             {
                 _isMarket = true;
                 StartCoroutine(MarkAndCollapseCells());
             }
             else
             {
                 if (_TryCheckedCounter > 1)
                 {
                     _isMarket = false;
                     UndoMacroCommand();
                 }
             }
         }

         private void CheckMatchByLine(LineDirectionType lineDirection, ICell cell)
         {
             IList<ICell> sideAList = new List<ICell>();
             IList<ICell> sideBList = new List<ICell>();

             int column = cell.TargetX;
             int row = cell.TargetY;

             int boardLimit = 0;
             int axis = 0;

             ICell sideCell = null;

             if (lineDirection == LineDirectionType.Horizontal)
             {
                 boardLimit = _board.Width;
                 axis = column;
             }
             else
             {
                 boardLimit = _board.Height;
                 axis = row;
             }

             if (axis > 0 && axis < boardLimit)
             {
                 for (int i = axis - 1; i >= 0; i--)
                 {
                     if (lineDirection == LineDirectionType.Horizontal)
                         sideCell = _board.Cells[i, row];
                     else
                         sideCell = _board.Cells[column, i];

                     if (sideCell != null)
                         if (sideCell.CurrentGameObject.CompareTag(cell.CurrentGameObject.tag))
                             sideAList.Add(sideCell);
                         else
                             break;
                 }
             }

             if (axis >= 0 && axis < boardLimit)
             {
                 for (int i = axis + 1; i < boardLimit; i++)
                 {
                     if (lineDirection == LineDirectionType.Horizontal)
                         sideCell = _board.Cells[i, row];
                     else
                         sideCell = _board.Cells[column, i];

                     if (sideCell != null)
                         if (sideCell.CurrentGameObject.CompareTag(cell.CurrentGameObject.tag))
                             sideBList.Add(sideCell);
                         else
                             break;
                 }
             }

             if (sideAList.Count + sideBList.Count > 1)
             {
                 foreach (var cellInListA in sideAList)
                     cellInListA.IsMatched = true;
                 foreach (var cellInListB in sideBList)
                     cellInListB.IsMatched = true;

                 cell.IsMatched = true;
             }
         }
         
         private void CheckCell(ICell cell)
         {
             CheckMatchByLine(LineDirectionType.Vertical, cell);
             CheckMatchByLine(LineDirectionType.Horizontal, cell);
         }

         private void MarkMatchedCell(ICell cell)
         {
             if (cell != null && cell.IsMatched)
             {
                 SpriteRenderer render = cell.CurrentGameObject.GetComponent<SpriteRenderer>();
                 render.color = new Color(render.color.r, render.color.g, render.color.b, .2f);
             }
         }

         IEnumerator MarkAndCollapseCells()
         {
             foreach (var cell in _board.Cells)
             {
                 MarkMatchedCell(cell);
             }

             yield return new WaitForSeconds(.4f);

             for (int i = 0; i < _board.Width; i++)
             for (int j = 0; j < _board.Height; j++)
             {
                 if (_board.Cells[i, j] != null && _board.Cells[i, j].IsMatched)
                 {
                     Destroy(_board.Cells[i, j].CurrentGameObject);
                     _board.Cells[i, j] = null;
                 }
             }

             yield return new WaitForSeconds(.4f);

             DecrieaseRow();
         }

         private void SwipeCells(MoveDirectionType direction)
         {
             int xPos = (int) Mathf.Round(_clickA.x);
             int yPos = (int) Mathf.Round(_clickA.y);

             ICell cellA = null;
             ICell cellB = null;

             if (xPos >= 0 && xPos < _board.Width && yPos >= 0 && yPos < _board.Height)
             {
                 cellA = _board.Cells[xPos, yPos];

                 switch (direction)
                 {
                     case MoveDirectionType.Up:
                         if (yPos < _board.Height - 1)
                         {
                             cellB = _board.Cells[xPos, yPos + 1];
                             if (cellB != null)
                             {
                                 _board.Cells[xPos, yPos + 1] = cellA;
                                 _board.Cells[xPos, yPos] = cellB;

                                 ICommand[] commands = {new MoveUpCommand(cellA), new MoveDownCommand(cellB)};
                                 SetMacroCommand(commands);

                                 OnEvent(EventTypeEnum.MOVE_up, null);
                             }
                         }

                         break;

                     case MoveDirectionType.Down:
                         if (yPos > 0)
                         {
                             cellB = _board.Cells[xPos, yPos - 1];
                             if (cellB != null)
                             {
                                 _board.Cells[xPos, yPos - 1] = cellA;
                                 _board.Cells[xPos, yPos] = cellB;

                                 ICommand[] commands = {new MoveDownCommand(cellA), new MoveUpCommand(cellB)};
                                 SetMacroCommand(commands);

                                 OnEvent(EventTypeEnum.MOVE_down, null);
                             }
                         }

                         break;

                     case MoveDirectionType.Left:
                         if (xPos > 0)
                         {
                             cellB = _board.Cells[xPos - 1, yPos];
                             if (cellB != null)
                             {
                                 _board.Cells[xPos - 1, yPos] = cellA;
                                 _board.Cells[xPos, yPos] = cellB;

                                 ICommand[] commands = {new MoveLeftCommand(cellA), new MoveRightCommand(cellB)};
                                 SetMacroCommand(commands);

                                 OnEvent(EventTypeEnum.MOVE_left, null);
                             }
                         }

                         break;

                     case MoveDirectionType.Right:
                         if (xPos < _board.Width - 1)
                         {
                             cellB = _board.Cells[xPos + 1, yPos];
                             if (cellB != null)
                             {
                                 _board.Cells[xPos + 1, yPos] = cellA;
                                 _board.Cells[xPos, yPos] = cellB;

                                 ICommand[] commands = {new MoveRightCommand(cellA), new MoveLeftCommand(cellB)};
                                 SetMacroCommand(commands);

                                 OnEvent(EventTypeEnum.MOVE_right, null);
                             }
                         }

                         break;
                 }
             }
         }

         private MoveDirectionType GetDirection(Vector3 clickA, Vector3 clickB)
         {
             MoveDirectionType moveDirectionType = MoveDirectionType.none;
             float angle = Mathf.Atan2(clickB.y - clickA.y, clickB.x - clickA.x) * 180 / Mathf.PI;

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

         public void SetMacroCommand(ICommand[] commands)
         {
             _macroCommand = new MacroCommand(commands);
         }

         public void ExecuteMacroCommand()
         {
             _macroCommand.Execute();
         }

         public void UndoMacroCommand()
         {
             _macroCommand.Undo();
         }

         public IBoard Board
         {
             get { return _board; }
             set { _board = value; }
         }
     }
 }
