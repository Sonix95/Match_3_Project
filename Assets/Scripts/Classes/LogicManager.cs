 using System;
using System.Collections;
using System.Collections.Generic;
 using System.Linq;
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

         private IList<ICell> lastCellInColumn;

         private IBoard _board;
         private ICommand _macroCommand;

         private Vector3 _clickA;
         private Vector3 _clickB;

         private bool _isMarket;
         private int _tryCheckSwipedCellsCounter;

         private void Start()
         {
             lastCellInColumn = new List<ICell>();
             StartCoroutine(TryCheckBoard());
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
                     TryCheckSwipedCells((ICell) messageData);
                     break;

                 case EventTypeEnum.CELL_endingMoveBack:
                     ICell cellAfterBack = (ICell) messageData;

                     _board.Cells[cellAfterBack.TargetX, cellAfterBack.TargetY] = cellAfterBack;
                     break;


                 case EventTypeEnum.CELL_fall:
                     ICell cellAfterFall = (ICell) messageData;
                     _board.Cells[cellAfterFall.TargetX, cellAfterFall.TargetY] = cellAfterFall;

                     if (lastCellInColumn.Count == 0)
                         StartCoroutine(TryCheckBoard());
                     break;

                 case EventTypeEnum.CELL_fallOnePoint:
                     ICell c = (ICell) messageData;

                     if (lastCellInColumn.Contains(c) && c.TargetY < _board.Height - 1)
                         Spawn(c.TargetY, c.TargetX, _board.Height);

                     lastCellInColumn.Remove(c);
                     break;


                 case EventTypeEnum.BOARD_collapse:
                     int[] cellParams = (int[]) messageData;

                     if (cellParams != null)
                         _board.Cells[cellParams[0], cellParams[1]] = null;

                     ExecuteMacroCommand();
                     break;

                 default:
                     Debug.Log("EVENT NOT FOUND!!!");
                     break;
             }
         }

         private IEnumerator TryCheckBoard()
         {
             yield return new WaitForSeconds(.1f);

             foreach (var cell in _board.Cells)
             {
                 CheckAndMarkManager.CheckCell(cell, _board);
                 CheckAndMarkManager.MarkCell(cell);
             }

             yield return new WaitForSeconds(.2f);

             for (int i = 0; i < _board.Width; i++)
             for (int j = 0; j < _board.Height; j++)
             {
                 if (_board.Cells[i, j] != null && _board.Cells[i, j].IsMatched)
                 {
                     Destroy(_board.Cells[i, j].CurrentGameObject);
                     _board.Cells[i, j] = null;
                 }
             }

             yield return new WaitForSeconds(.2f);

             DecreaseRow();
         }

         private void DecreaseRow()
         {
             int nullCount = 0;
             int highestColumnCell = 0;
             ICell cell = null;
             lastCellInColumn.Clear();

             for (int i = 0; i < _board.Width; i++)
             {
                 for (int j = 0; j < _board.Height; j++)
                 {
                     if (_board.Cells[i, j] == null)
                         nullCount++;

                     else if (nullCount > 0 && _board.Cells[i, j] != null)
                     {
                         if (j > highestColumnCell)
                         {
                             highestColumnCell = j;
                             cell = _board.Cells[i, j];
                         }

                         StartCoroutine(StartFall(cell, nullCount, new int[] {i, j}));
                     }
                 }

                 if (cell != null)
                     lastCellInColumn.Add(cell);

                 if (cell == null && nullCount > 0)
                 {
                     int notNullHigherCellIndex = (_board.Height - 1) - nullCount;
                     Spawn(notNullHigherCellIndex, i, _board.Height);
                 }

                 cell = null;
                 nullCount = 0;
                 highestColumnCell = 0;
             }
         }

         private IEnumerator StartFall(ICell cell, int offset, int[] cellParams)
         {
             yield return new WaitForSeconds(.05f);

             ICommand[] commands = {new FallCommand(cell, offset)};
             SetMacroCommand(commands);

             OnEvent(EventTypeEnum.BOARD_collapse, cellParams);
         }

         private void Spawn(int highterCellYPos, int xPos, int yPos)
         {
             int offset = _board.Height - 1 - highterCellYPos;

             ICell newCell = _board.SpawnManager.GenerateGameElement(new Vector3(xPos, yPos, 0));
             lastCellInColumn.Add(newCell);

             StartCoroutine(StartFall(newCell, offset, null));
         }

         private void TryCheckSwipedCells(ICell cell)
         {
             _tryCheckSwipedCellsCounter++;

             CheckAndMarkManager.CheckCell(cell, _board);

             if (cell.IsMatched)
             {
                 _isMarket = true;
             }

             if (_tryCheckSwipedCellsCounter > 1)
             {
                 if (_isMarket)
                     StartCoroutine(TryCheckBoard());
                 else
                     UndoMacroCommand();

                 _isMarket = false;
                 _tryCheckSwipedCellsCounter = 0;
             }
         }

         private void SwipeCells(MoveDirectionType direction)
         {
             int xPos = (int) Mathf.Round(_clickA.x);
             int yPos = (int) Mathf.Round(_clickA.y);

             if (xPos >= 0 && xPos < _board.Width && yPos >= 0 && yPos < _board.Height)
             {
                 ICell cellA = _board.Cells[xPos, yPos];
                 ICell cellB;

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

         #region Command Implimentation

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

         #endregion

         public IBoard Board
         {
             get { return _board; }
             set { _board = value; }
         }
     }
 }
