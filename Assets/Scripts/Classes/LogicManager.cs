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

         private int TryCheckedCount = 0;
         private bool isMarket = false;

         private void Start()
         {
             StartCoroutine(CheckCells());
         }

         private IEnumerator CheckCells()
         {
             yield return new WaitForSeconds(.3f);

             foreach (var cell in _board.Cells)
             {
                 CheckMatchByLine(LineDirectionType.Vertical, cell);
                 CheckMatchByLine(LineDirectionType.Horizontal, cell);

                 MarkMatchedCell(cell);
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

         private void MarkMatchedCell(ICell cell)
         {
             if (cell.IsMatched)
             {
                 SpriteRenderer render = cell.CurrentGameObject.GetComponent<SpriteRenderer>();
                 render.color = new Color(render.color.r, render.color.g, render.color.b, .2f);
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
                     Check((ICell) messageData);
                     break;

                 case EventTypeEnum.CELL_endingMoveBack:
                     ICell cellAfterBack = (ICell) messageData;
                     _board.Cells[cellAfterBack.TargetX, cellAfterBack.TargetY] = cellAfterBack;
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

         private void Check(ICell cell)
         {
             TryCheckedCount++;
             if (TryCheckedCount > 2)
             {
                 TryCheckedCount = 1;
                 isMarket = false;
             }

             CheckMatchByLine(LineDirectionType.Vertical, cell);
             CheckMatchByLine(LineDirectionType.Horizontal, cell);

             if (cell.IsMatched || isMarket)
             {
                 isMarket = true;

                 StartCoroutine(MarkAndCollapseCells());
             }
             else
             {
                 if (TryCheckedCount > 1)
                 {
                     isMarket = false;
                     UndoMacroCommand();
                 }
             }
         }

         IEnumerator MarkAndCollapseCells()
         {
             foreach (var cells in _board.Cells)
                 MarkMatchedCell(cells);

             yield return new WaitForSeconds(.5f);
             Debug.Log("COLLAPSE");
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

                             _board.Cells[xPos, yPos + 1] = cellA;
                             _board.Cells[xPos, yPos] = cellB;

                             ICommand[] commands = {new MoveUpCommand(cellA), new MoveDownCommand(cellB)};
                             SetMacroCommand(commands);

                             OnEvent(EventTypeEnum.MOVE_up, null);
                         }

                         break;
                     case MoveDirectionType.Down:
                         if (yPos > 0)
                         {
                             cellB = _board.Cells[xPos, yPos - 1];

                             _board.Cells[xPos, yPos - 1] = cellA;
                             _board.Cells[xPos, yPos] = cellB;

                             ICommand[] commands = {new MoveDownCommand(cellA), new MoveUpCommand(cellB)};
                             SetMacroCommand(commands);

                             OnEvent(EventTypeEnum.MOVE_down, null);
                         }

                         break;
                     case MoveDirectionType.Left:
                         if (xPos > 0)
                         {
                             cellB = _board.Cells[xPos - 1, yPos];

                             _board.Cells[xPos - 1, yPos] = cellA;
                             _board.Cells[xPos, yPos] = cellB;

                             ICommand[] commands = {new MoveLeftCommand(cellA), new MoveRightCommand(cellB)};
                             SetMacroCommand(commands);

                             OnEvent(EventTypeEnum.MOVE_left, null);
                         }

                         break;
                     case MoveDirectionType.Right:
                         if (xPos < _board.Width - 1)
                         {
                             cellB = _board.Cells[xPos + 1, yPos];

                             _board.Cells[xPos + 1, yPos] = cellA;
                             _board.Cells[xPos, yPos] = cellB;

                             ICommand[] commands = {new MoveRightCommand(cellA), new MoveLeftCommand(cellB)};
                             SetMacroCommand(commands);

                             OnEvent(EventTypeEnum.MOVE_right, null);
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
