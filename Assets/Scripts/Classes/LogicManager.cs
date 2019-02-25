using System;
using System.Collections;
using System.Collections.Generic;
using Mathc3Project.Commands;
using UnityEngine;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using UnityEditor;

 namespace Mathc3Project
 {
     class LogicManager : MonoBehaviour, ILogicManager
     {
         private const float SWIPE_SENSTIVITY = 0.3f;

         private IBoard _board;
         
         private ICommand _macroCommand;

         private GameStates _currentGameState = GameStates.Ready;

         private Vector3 _clickA;
         private Vector3 _clickB;
         
         private bool _isMatchedSwipe;
         private int _swipeCounter;

         private ICell _lastSpawnedCell = null;

         public void OnEvent(EventTypeEnum eventTypeEnum, object messageData)
         {
             switch (eventTypeEnum)
             {
                 case EventTypeEnum.MOUSE_down:
                     _clickA = (Vector3) messageData;
                     break;

                 case EventTypeEnum.MOUSE_up:
                     _clickB = (Vector3) messageData;

                     if (_currentGameState == GameStates.Ready)
                     {
                         if (Mathf.Abs(_clickB.x - _clickA.x) > SWIPE_SENSTIVITY ||
                             Mathf.Abs(_clickB.y - _clickA.y) > SWIPE_SENSTIVITY)
                         {
                             MoveDirectionType swipeDirection = GetDirection(_clickA, _clickB);
                             SwipeCells(swipeDirection);
                         }
                     }

                     break;

                 case EventTypeEnum.MOVE_up:
                     _currentGameState = GameStates.Wait;
                     ExecuteMacroCommand();
                     break;

                 case EventTypeEnum.MOVE_down:
                     _currentGameState = GameStates.Wait;
                     ExecuteMacroCommand();
                     break;

                 case EventTypeEnum.MOVE_left:
                     _currentGameState = GameStates.Wait;
                     ExecuteMacroCommand();
                     break;

                 case EventTypeEnum.MOVE_right:
                     _currentGameState = GameStates.Wait;
                     ExecuteMacroCommand();
                     break;

                 case EventTypeEnum.CELL_endingMove:
                     TryCheckSwipedCells((ICell) messageData);
                     break;

                 case EventTypeEnum.CELL_endingMoveBack:
                     ICell cell = (ICell) messageData;

                     _board.Cells[cell.TargetX, cell.TargetY] = cell;
                     _currentGameState = GameStates.Ready;
                     break;

                 case EventTypeEnum.CELL_fall:
                     ICell fallenCell = (ICell) messageData;

                     if (_lastSpawnedCell == fallenCell)
                         CheckBoard();
                     
                     break;

                 case EventTypeEnum.CELL_destroyed:
                     break;

                 case EventTypeEnum.BOARD_collapse:
                     ExecuteMacroCommand();
                     break;

                 default:
                     Debug.Log("EVENT NOT FOUND!!!");
                     break;
             }
         }

         private void TryCheckSwipedCells(ICell cell)
         {
             _swipeCounter++;

             CheckAndMarkManager.CheckCell(cell, _board);

             if (cell.IsMatched)
                 _isMatchedSwipe = true;

             if (_swipeCounter > 1)
             {
                 if (_isMatchedSwipe)
                     StartCoroutine(MarkAndDestroyMatches());
                 else
                     UndoMacroCommand();

                 _isMatchedSwipe = false;
                 _swipeCounter = 0;
             }
         }

         private void CheckBoard()
         {
             foreach (var cell in _board.Cells)
                 CheckAndMarkManager.CheckCell(cell, _board);

             StartCoroutine(MarkAndDestroyMatches());
         }

         private IEnumerator MarkAndDestroyMatches()
         {
             for (int i = 0; i < _board.Width; i++)
             for (int j = 0; j < _board.Height; j++)
             {
                 if (_board.Cells[i, j] != null && _board.Cells[i, j].IsMatched)
                     CheckAndMarkManager.MarkCell(_board.Cells[i, j]);
             }

             yield return new WaitForSeconds(0.4f);

             for (int i = 0; i < _board.Width; i++)
             for (int j = 0; j < _board.Height; j++)
             {
                 if (_board.Cells[i, j] != null)
                 {
                     if (_board.Cells[i, j].IsMatched)
                     {
                         Destroy(_board.Cells[i, j].CurrentGameObject);
                         _board.Cells[i, j] = null;
                     }
                 }
             }

             StartCoroutine(DecreaseRow());
         }

         private IEnumerator DecreaseRow()
         {
             for (int i = 0; i < _board.Width; i++)
             for (int j = 0; j < _board.Height; j++)
                 if (!_board.HollowCells[i, j] && _board.Cells[i, j] == null)
                 {
                     for (int k = j + 1; k < _board.Height; k++)
                     {
                         if (_board.Cells[i, k] != null)
                         {
                             ICell tempCell = _board.Cells[i, k];
                             _board.Cells[i, k] = null;

                             StartCoroutine(Fall(tempCell, j));
                             break;
                         }
                     }
                 }

             yield return new WaitForSeconds(.4f);
             FillBoard();
         }

         private void FillBoard()
         {
             RefillBoard();

             while (HaveMatches())
                 MarkAndDestroyMatches();
             
             _currentGameState = GameStates.Ready;
         }

         private void RefillBoard()
         {
             for (int i = 0; i < _board.Width; i++)
             for (int j = 0; j < _board.Height; j++)
             {
                 if (_board.Cells[i, j] == null && !_board.HollowCells[i,j])
                 {
                     Vector3 tempPos = new Vector3(i, _board.Height + 5, 0f);
                     ICell newCell = _board.SpawnManager.GenerateRandomGameElement(tempPos);

                     StartCoroutine(Fall(newCell, j));
                 }
             }
         }

         IEnumerator Fall(ICell cell, int newTarget)
         {
             yield return new WaitForSeconds(0.05f);

             ICommand[] commands = {new FallCommand(cell, newTarget, _board)};
             SetMacroCommand(commands);
             _lastSpawnedCell = cell;
             OnEvent(EventTypeEnum.BOARD_collapse, null);
         }

         private bool HaveMatches()
         {
             for (int i = 0; i < _board.Width; i++)
             for (int j = 0; j < _board.Height; j++)
             {
                 if (_board.Cells[i, j] != null)
                     if (_board.Cells[i, j].IsMatched)
                         return true;
             }
             return false;
         }

         private void SwipeCells(MoveDirectionType direction)
         {
             int xPos = (int) Mathf.Round(_clickA.x);
             int yPos = (int) Mathf.Round(_clickA.y);

             if (xPos >= 0 && xPos < _board.Width && yPos >= 0 && yPos < _board.Height)
             {
                 ICell cellA = _board.Cells[xPos, yPos];

                 if (cellA != null)
                 {
                     switch (direction)
                     {
                         case MoveDirectionType.Up:
                             if (yPos < _board.Height - 1)
                             {
                                 ICell cellB = _board.Cells[xPos, yPos + 1];
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
                                 ICell cellB = _board.Cells[xPos, yPos - 1];
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
                                 ICell cellB = _board.Cells[xPos - 1, yPos];
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
                                 ICell cellB = _board.Cells[xPos + 1, yPos];
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
