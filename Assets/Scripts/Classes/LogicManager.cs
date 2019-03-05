using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mathc3Project.Classes.Commands;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Cells;
using Mathc3Project.Interfaces.Command;
using UnityEngine;

namespace Mathc3Project.Classes
{
    public class LogicManager : MonoBehaviour, ILogicManager
    {
        private const float SWIPE_D = 0.1f;

        private IBoard _board;
        private ICheckManager _checkManager;
        private ISpawnManager _spawnManager;

        private Vector3 _clickA;
        private Vector3 _clickB;

        private MoveDirectionTypes _swipeDirection;

        private ICommand _macroCommand;

        private GameStates _gameState = GameStates.Ready;

        private bool _isMatchedSwipe;
        private int _swipeCounter;

        private readonly IDictionary<ICell, IList<ICell>> _matchedCellsDictionary = new Dictionary<ICell, IList<ICell>>();
        private readonly IDictionary<GameObject, Vector2> _fallCellsDictionary = new Dictionary<GameObject, Vector2>();
        private readonly IDictionary<Vector2, PowerTypes> _powersDictionary = new Dictionary<Vector2, PowerTypes>();
        private ICell _lastFallCell;
        private bool _lastSpawnedCell;


        public void OnEvent(EventTypes eventType, object messageData)
        {
            switch (eventType)
            {
                case EventTypes.LMB_Down:
                    _clickA = (Vector3) messageData;
                    break;

                case EventTypes.LMB_Up:
                    _clickB = (Vector3) messageData;

                    if (_gameState == GameStates.Ready &&
                        _clickA.x >= 0 && _clickA.x < _board.Width && _clickA.y >= 0 && _clickA.y < _board.Height &&
                        (Mathf.Abs(_clickB.x - _clickA.x) > SWIPE_D || Mathf.Abs(_clickB.y - _clickA.y) > SWIPE_D))
                    {
                        _swipeDirection = Helper.FindMoveDirection(_clickA, _clickB);
                        SwipeCells(_swipeDirection);
                    }

                    break;

                case EventTypes.Swipe_Up:
                    _gameState = GameStates.Wait;
                    ExecuteMacroCommand();
                    break;

                case EventTypes.Swipe_Down:
                    _gameState = GameStates.Wait;
                    ExecuteMacroCommand();
                    break;

                case EventTypes.Swipe_Left:
                    _gameState = GameStates.Wait;
                    ExecuteMacroCommand();
                    break;

                case EventTypes.Swipe_Right:
                    _gameState = GameStates.Wait;
                    ExecuteMacroCommand();
                    break;

                case EventTypes.CELL_EndMove:
                    TryCheckSwipedCells((ICell) messageData);
                    break;

                case EventTypes.CELL_EndMoveBack:
                    ICell cellBack = (ICell) messageData;

                    _board.Cells[cellBack.TargetX, cellBack.TargetY] = cellBack;
                    cellBack.CellState = CellStates.Wait;

                    _gameState = GameStates.Ready;
                    break;

                case EventTypes.CELL_Fall:
                    ICell cellFall = (ICell) messageData;

                    cellFall.CellState = CellStates.Wait;

                    if (cellFall == _lastFallCell)
                        CheckBoard();
                    break;

                case EventTypes.POWER_Use:
                    ArrayList arr = (ArrayList) messageData;

                    PowerTypes power = Helper.StringToPowerType(arr[0].ToString());
                    Vector3 position = (Vector3) arr[1];

                    _powersDictionary.Add(position,power);
                    break;

                case EventTypes.BOARD_collapse:
                    ExecuteMacroCommand();
                    break;

                case EventTypes.BOARD_EndDestroyMatchedCells:
                    if (_powersDictionary.Count > 0)
                    {
                        Vector2 pos = _powersDictionary.First().Key;
                        PowerTypes powerType = _powersDictionary.First().Value;
                        
                        List<ICell> cellsList = new List<ICell>(_checkManager.PowerCheck(powerType,pos));
                        ICell cell = _board.Cells[(int) pos.x, (int) pos.y];
                        
                        _matchedCellsDictionary.Add(cell, cellsList);
                        _powersDictionary.Remove(_powersDictionary.First());
                        
                        StartCoroutine(MarkAndDestroy(_matchedCellsDictionary));
                    }
                    else
                        StartCoroutine(RefillBoard());
                    break;
                
                //TODO DELETE ON FINISH
                case EventTypes.UTILITY_BoardCellsInfo:
                    foreach (var _powers in _powersDictionary)
                    {
                        Debug.Log((_powers.Key + " - " + _powers.Value));
                    }

                    break;

                default:
                    Debug.Log("EVENT NOT FOUND");
                    break;
            }
        }

        private void TryCheckSwipedCells(ICell cell)
        {
            _swipeCounter++;

            IList<ICell> cellsList = new List<ICell>(_checkManager.CheckCell(cell));

            if (cellsList.Count > 2 || cell.CurrentGameObject.CompareTag("Power")) 
            {
                if (cell.CurrentGameObject.CompareTag("Power"))
                {
                    cellsList.Add(cell);
                }
                
                _isMatchedSwipe = true;
                _matchedCellsDictionary.Add(cell, cellsList);
            }

            if (_swipeCounter > 1)
            {
                if (_isMatchedSwipe)
                    StartCoroutine(MarkAndDestroy(_matchedCellsDictionary));
                else
                {
                    _matchedCellsDictionary.Clear();
                    _powersDictionary.Clear();
                    UndoMacroCommand();
                }

                _isMatchedSwipe = false;
                _swipeCounter = 0;
            }
        }

        private void CheckBoard()
        {
            _lastSpawnedCell = false;
            _fallCellsDictionary.Clear();
            
            if (HaveMatches())
            {
                FindMatches();
                return;
            }

            _gameState = GameStates.Ready;
        }

        private bool HaveMatches()
        {
            for (int i = 0; i < _board.Width; i++)
            for (int j = 0; j < _board.Height; j++)
            {
                if (_board.Cells[i, j].CellType != CellTypes.Hollow && _board.Cells[i, j].CurrentGameObject != null)
                    if (_checkManager.HaveMatch(_board.Cells[i, j]))
                        return true;
            }

            return false;
        }
       
        private void FindMatches()
        {
            foreach (var cell in _board.Cells)
            {
                IList<ICell> matchedCellsList = new List<ICell>();
                LineDirectionTypes highedDirection = LineDirectionTypes.Undefined;
                
                if (cell.CellType != CellTypes.Hollow && cell.CellState != CellStates.Check)
                {
                    _checkManager.JustCheckCell(cell, out matchedCellsList);
                    _matchedCellsDictionary.Add(cell, matchedCellsList);
                }
            }

            StartCoroutine(MarkAndDestroy(_matchedCellsDictionary));
        }

        private IEnumerator MarkAndDestroy(IDictionary<ICell, IList<ICell>> cellsDictionary)
        {
            foreach (var cellList in cellsDictionary)
            {
                MarkMatchedCells(cellList.Value);
            }

            yield return new WaitForSeconds(0.2f);

            foreach (var cellList in cellsDictionary)
            {
                WorkAfterMatch(cellList.Value);
            }
            
            _matchedCellsDictionary.Clear();

            OnEvent(EventTypes.BOARD_EndDestroyMatchedCells, null);
        }
        
        IEnumerator RefillBoard()
        {            
            DecreaseBoard();

            yield return new WaitForSeconds(0.2f);
            SpawnNewCells();
        }

        private void SpawnNewCells()
        {
            IList<Vector2> spawnTarget = new List<Vector2>();

            for (int j = 0; j < _board.Height; j++)
            for (int i = 0; i < _board.Width; i++)
            {
                if (_board.Cells[i, j].CellType != CellTypes.Hollow && _board.Cells[i, j].CurrentGameObject == null)
                {
                    spawnTarget.Add(new Vector2(i, j));
                }
            }

            StartCoroutine(FindTargetForNewCell(spawnTarget));
        }

        private IEnumerator FindTargetForNewCell(IList<Vector2> spawnTargets)
        {
            IDictionary<int, IList<Vector2>> spawnTargetsDictionary = new Dictionary<int, IList<Vector2>>();

            for (int i = 0; i < _board.Height; i++)
            {
                List<Vector2> tempList = new List<Vector2>();

                foreach (var spawnTarget in spawnTargets)
                {
                    if (spawnTarget.y == i)
                        tempList.Add(spawnTarget);
                }

                spawnTargetsDictionary.Add(i, tempList);
            }

            foreach (var row in spawnTargetsDictionary)
            {
                if (row.Key == spawnTargetsDictionary.Keys.Last())
                    _lastSpawnedCell = true;

                if (row.Value.Count > 0)
                {
                    SpawnRow(row.Value);
                    yield return new WaitForSeconds(.3f);
                }
            }
        }

        private void SpawnRow(IList<Vector2> positionsList)
        {
            IList<ICell> cells = new List<ICell>();

            foreach (var position in positionsList)
            {
                Vector3 tempPosition = new Vector3(position.x, _board.Height, 0f);
                GameObject spawnedGameObject = _spawnManager.SpawnPrefab(tempPosition);

                ICell tempCell = _board.Cells[(int) position.x, (int) position.y];
                tempCell.CurrentGameObject = spawnedGameObject;

                cells.Add(tempCell);
            }

            if (_lastSpawnedCell)
                _lastFallCell = cells.Last();

            StartFallCommand(cells);
        }

        private void DecreaseBoard()
        {
            for (int i = 0; i < _board.Width; i++)
            for (int j = 0; j < _board.Height; j++)
                if (_board.Cells[i, j].CellType != CellTypes.Hollow && _board.Cells[i, j].CurrentGameObject == null)
                {
                    for (int k = j + 1; k < _board.Height; k++)
                    {
                        if (_board.Cells[i, k].CurrentGameObject != null)
                        {
                            GameObject go = _board.Cells[i, k].CurrentGameObject;
                            _fallCellsDictionary.Add(go, new Vector2(i, j));

                            _board.Cells[i, k].CurrentGameObject = null;
                            break;
                        }
                    }
                }

            StartFall(_fallCellsDictionary);
        }

        private void StartFall(IDictionary<GameObject, Vector2> dictionary)
        {
            IList<ICell> cells = new List<ICell>();

            foreach (var cell in dictionary)
            {
                ICell tempCell = _board.Cells[(int) cell.Value.x, (int) cell.Value.y];
                tempCell.CurrentGameObject = cell.Key;

                cells.Add(tempCell);
            }

            StartFallCommand(cells);
        }

        private void StartFallCommand(IList<ICell> cellsList)
        {
            ICommand[] commands = new ICommand[cellsList.Count];

            for (int i = 0; i < cellsList.Count; i++)
                commands[i] = new FallCommand(cellsList[i]);

            SetMacroCommand(commands);
            OnEvent(EventTypes.BOARD_collapse, null);
        }

        private void MarkMatchedCells(IList<ICell> cellsToMarkList)
        {
            foreach (var cell in cellsToMarkList)
                if(cell.CurrentGameObject != null && cell.CurrentGameObject.CompareTag("Power") == false)
                MarkCell(cell);
        }

        private void WorkAfterMatch(IList<ICell> cellsAfterMarkList)
        {
            foreach (var cell in cellsAfterMarkList)
                cell.DoAfterMatch();
        }

        private void MarkCell(ICell cell)
        {
            SpriteRenderer render = cell.CurrentGameObject.GetComponent<SpriteRenderer>();
            render.color = new Color(render.color.r, render.color.g, render.color.b, .2f);
        }

        private void SwipeCells(MoveDirectionTypes direction)
        {
            int xPos = (int) Mathf.Round(_clickA.x);
            int yPos = (int) Mathf.Round(_clickA.y);

            ICell cellA = _board.Cells[xPos, yPos];

            if (cellA.CellType == CellTypes.Normal && cellA.CurrentGameObject != null)
            {
                switch (direction)
                {
                    case MoveDirectionTypes.Up:
                        if (yPos < _board.Height - 1)
                        {
                            ICell cellB = _board.Cells[xPos, yPos + 1];
                            if (cellB.CellType == CellTypes.Normal && cellB.CurrentGameObject != null)
                            {
                                _board.Cells[xPos, yPos + 1] = cellA;
                                _board.Cells[xPos, yPos] = cellB;

                                ICommand[] commands = {new SwipeUpCommand(cellA), new SwipeDownCommand(cellB),};
                                SetMacroCommand(commands);

                                OnEvent(EventTypes.Swipe_Up, null);
                            }
                        }

                        break;

                    case MoveDirectionTypes.Down:
                        if (yPos > 0)
                        {
                            ICell cellB = _board.Cells[xPos, yPos - 1];
                            if (cellB.CellType == CellTypes.Normal && cellB.CurrentGameObject != null)
                            {
                                _board.Cells[xPos, yPos - 1] = cellA;
                                _board.Cells[xPos, yPos] = cellB;

                                ICommand[] commands = {new SwipeDownCommand(cellA), new SwipeUpCommand(cellB),};
                                SetMacroCommand(commands);

                                OnEvent(EventTypes.Swipe_Down, null);
                            }
                        }

                        break;

                    case MoveDirectionTypes.Left:
                        if (xPos > 0)
                        {
                            ICell cellB = _board.Cells[xPos - 1, yPos];
                            if (cellB.CellType == CellTypes.Normal && cellB.CurrentGameObject != null)
                            {
                                _board.Cells[xPos - 1, yPos] = cellA;
                                _board.Cells[xPos, yPos] = cellB;

                                ICommand[] commands = {new SwipeLeftCommand(cellA), new SwipeRightCommand(cellB),};
                                SetMacroCommand(commands);

                                OnEvent(EventTypes.Swipe_Left, null);
                            }
                        }

                        break;

                    case MoveDirectionTypes.Right:
                        if (xPos < _board.Width - 1)
                        {
                            ICell cellB = _board.Cells[xPos + 1, yPos];
                            if (cellB.CellType == CellTypes.Normal && cellB.CurrentGameObject != null)
                            {
                                _board.Cells[xPos + 1, yPos] = cellA;
                                _board.Cells[xPos, yPos] = cellB;

                                ICommand[] commands = {new SwipeRightCommand(cellA), new SwipeLeftCommand(cellB),};
                                SetMacroCommand(commands);

                                OnEvent(EventTypes.Swipe_Right, null);
                            }
                        }

                        break;
                }
            }
        }

        #region Command Implimentation

        private void SetMacroCommand(ICommand[] commands)
        {
            _macroCommand = new MacroCommand(commands);
        }

        private void ExecuteMacroCommand()
        {
            _macroCommand.Execute();
        }

        private void UndoMacroCommand()
        {
            _macroCommand.Undo();
        }

        #endregion

        public IBoard Board
        {
            get { return _board; }
            set { _board = value; }
        }
        
        public ICheckManager CheckManager
        {
            get { return _checkManager; }
            set { _checkManager = value; }
        }

        public ISpawnManager SpawnManager
        {
            get { return _spawnManager; }
            set { _spawnManager = value; }
        }
    }
}
