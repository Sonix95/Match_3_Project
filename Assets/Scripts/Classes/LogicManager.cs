using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mathc3Project.Classes.Commands;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Cells;
using Mathc3Project.Interfaces.Command;
using Mathc3Project.Interfaces.Observer;
using UnityEngine;

namespace Mathc3Project.Classes
{
    public class LogicManager : MonoBehaviour, ILogicManager
    {
        private IBoard _board;
        private ICheckManager _checkManager;
        private ISpawnManager _spawnManager;
        
        private INotifier _notifier;
        private IList<ISubscriber> _subscribes;

        private Vector3 _clickA;
        private Vector3 _clickB;

        private ICommand _macroCommand;

        private GameStates _gameState;

        private bool _isMatchedSwipe;
        private int _swipeCounter;
        
        private IDictionary<GameObject, Vector2> _fallCellsDictionary;
        
        private IDictionary<Vector2, PowerUpTypes> _powersDictionary;
        private IDictionary<Vector3, PowerUpTypes> _spawnedPowerUpDictionary;
        
        private IDictionary<IList<ICell>, AxisTypes> _matchedCellsDictionary;
        private IDictionary<ICell, IDictionary<IList<ICell>, AxisTypes>> _matchedCellsWithAxisDictionary;

        private ICell _lastFallCell;
        private bool _lastSpawnedCell;
        
        private void Awake()
        {
            _subscribes = new List<ISubscriber>();

            _fallCellsDictionary = new Dictionary<GameObject, Vector2>();
            _powersDictionary = new Dictionary<Vector2, PowerUpTypes>();
            _spawnedPowerUpDictionary = new Dictionary<Vector3, PowerUpTypes>();
            _matchedCellsDictionary = new Dictionary<IList<ICell>, AxisTypes>();
            _matchedCellsWithAxisDictionary = new Dictionary<ICell, IDictionary<IList<ICell>, AxisTypes>>();
            
            _gameState = GameStates.Ready;
        }

        public void OnEvent(EventTypes eventType, object messageData)
        {
            switch (eventType)
            {
                case EventTypes.LMB_Down:
                    _clickA = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    break;

                case EventTypes.LMB_Up:
                    _clickB = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    if (_gameState == GameStates.Ready)
                    {
                        if (_clickA.x >= 0 && _clickA.x < _board.Width && _clickA.y >= 0 && _clickA.y < _board.Height &&
                            (Mathf.Abs(_clickB.x - _clickA.x) > Strings.SWIPE_SENSITIVITY ||
                             Mathf.Abs(_clickB.y - _clickA.y) > Strings.SWIPE_SENSITIVITY))
                        {
                            MoveDirectionTypes swipeDirection = Helper.FindMoveDirection(_clickA, _clickB);
                            SwipeCells(swipeDirection);
                        }
                    }

                    break;

                case EventTypes.Swipe:
                    _gameState = GameStates.Wait;
                    
                    SetMacroCommand((ICommand[]) messageData);
                    ExecuteMacroCommand();
                    break;

                case EventTypes.BOARD_collapse:
                    ExecuteMacroCommand();
                    break;

                case EventTypes.BOARD_EndDestroyMatchedCells:
                    if (_powersDictionary.Count > 0)
                    {
                        Vector2 pos = _powersDictionary.First().Key;
                        PowerUpTypes powerUpType = _powersDictionary.First().Value;

                        List<ICell> cellsList = new List<ICell>(_checkManager.PowerCheck(powerUpType, pos));
                        ICell cell = _board.Cells[(int) pos.x, (int) pos.y];

                        _matchedCellsDictionary.Add(cellsList, AxisTypes.Undefined);
                        _matchedCellsWithAxisDictionary.Add(cell, _matchedCellsDictionary);

                        _powersDictionary.Remove(_powersDictionary.First());

                        StartCoroutine(MarkAndDestroy(_matchedCellsWithAxisDictionary));
                    }
                    else
                        StartCoroutine(RefillBoard());

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
                
                case EventTypes.CELL_Destroy:
                    string cellTag = (string) messageData;
                    Debug.Log(cellTag);
                    Notify(EventTypes.CELL_Destroy, cellTag);
                    break;

                case EventTypes.POWER_Use:
                    ArrayList arr = (ArrayList) messageData;

                    PowerUpTypes powerUp = Helper.StringToPowerType(arr[0].ToString());
                    Vector3 position = (Vector3) arr[1];

                    _powersDictionary.Add(position, powerUp);
                    break;
                
                case EventTypes.TASK_Finished:
                    Debug.Log("Задачи выполненны ");
                    break;

                default:
                    Debug.Log("EVENT NOT FOUND");
                    break;
            }
        }

        private void TryCheckSwipedCells(ICell cell)
        {
            _swipeCounter++;

            AxisTypes majorAxis;
            IList<ICell> cellsList = new List<ICell>(_checkManager.CheckCell(cell, out majorAxis));

            if (cellsList.Count > 2 || cell.CurrentGameObject.CompareTag(Strings.Tag_Power))
            {
                if (cell.CurrentGameObject.CompareTag(Strings.Tag_Power))
                    cellsList.Add(cell);

                _matchedCellsDictionary.Add(cellsList, majorAxis);
                _matchedCellsWithAxisDictionary.Add(cell, _matchedCellsDictionary);

                _isMatchedSwipe = true;
            }

            if (_swipeCounter > 1)
            {
                if (_isMatchedSwipe)
                    StartCoroutine(MarkAndDestroy(_matchedCellsWithAxisDictionary));
                else
                {
                    _matchedCellsWithAxisDictionary.Clear();
                    _matchedCellsDictionary.Clear();
                    
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
            _matchedCellsDictionary.Clear();
            _matchedCellsWithAxisDictionary.Clear();

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
                if (Helper.CellIsEmpty(_board.Cells[i, j]) == false)
                    if (_checkManager.HaveMatch(_board.Cells[i, j]))
                        return true;
            }

            return false;
        }

        private void FindMatches()
        {
            foreach (var cell in _board.Cells)
            {
                AxisTypes majorAxis = AxisTypes.Undefined;

                IList<ICell> matchedCellsList = new List<ICell>();

                IDictionary<IList<ICell>, AxisTypes> matchedCellsDictionary =
                    new Dictionary<IList<ICell>, AxisTypes>();

                if (Helper.CellIsEmpty(cell) == false)
                {
                    if (cell.CellState != CellStates.Check)
                    {
                        matchedCellsList = _checkManager.CheckCell(cell, out majorAxis);
                        matchedCellsDictionary.Add(matchedCellsList, majorAxis);
                        _matchedCellsWithAxisDictionary.Add(cell, matchedCellsDictionary);
                    }
                }
            }

            StartCoroutine(MarkAndDestroy(_matchedCellsWithAxisDictionary));
        }
       
        private IEnumerator MarkAndDestroy(IDictionary<ICell, IDictionary<IList<ICell>, AxisTypes>> cellsWithAxisDictionary)
        {
            foreach (var cellDictionary in cellsWithAxisDictionary)
            {
                foreach (var cellList in cellDictionary.Value)
                {
                    MarkMatchedCells(cellList.Key);
                }
            }

            yield return new WaitForSeconds(Strings.TIME_AFTER_MARK);

            foreach (var cellDictionary in cellsWithAxisDictionary)
            {
                foreach (var cellList in cellDictionary.Value)
                {
                    if (cellDictionary.Key.CurrentGameObject != null)
                    {
                        if (cellList.Key.Count > 3 &&
                            cellDictionary.Key.CurrentGameObject.CompareTag(Strings.Tag_Power) == false)
                        {
                            AxisTypes majorAxis = cellList.Value;
                            int matchCount = cellList.Key.Count;

                            PowerUpTypes powerUp = Helper.DetectPowerUp(matchCount, majorAxis);
                            _spawnedPowerUpDictionary.Add(
                                new Vector3(cellDictionary.Key.TargetX, cellDictionary.Key.TargetY, 0f), powerUp);
                        }
                    }

                    WorkAfterMatch(cellList.Key);
                }
            }

            _matchedCellsWithAxisDictionary.Clear();
            _matchedCellsDictionary.Clear();

            yield return new WaitForSeconds(Strings.TIME_AFTER_DESTROY);
            OnEvent(EventTypes.BOARD_EndDestroyMatchedCells, null);
        }

        private IEnumerator RefillBoard()
        {
            if (_spawnedPowerUpDictionary.Count > 0)
            {
                foreach (var spawnedPowerUp in _spawnedPowerUpDictionary)
                {
                    GameObject spawnedPowerUpGO = _spawnManager.SpawnPowerPrefab(spawnedPowerUp.Value, spawnedPowerUp.Key);
                    _board.Cells[(int) spawnedPowerUp.Key.x, (int) spawnedPowerUp.Key.y].CurrentGameObject =
                        spawnedPowerUpGO;
                }
            }

            _spawnedPowerUpDictionary.Clear();

            DecreaseBoard();

            yield return new WaitForSeconds(Strings.TIME_AFTER_DECREASE);
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
                    yield return new WaitForSeconds(Strings.TIME_BETWEEN_SPAWN);
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

        private void WorkAfterMatch(IList<ICell> cellsAfterMarkList)
        {
            foreach (var cell in cellsAfterMarkList)
                cell.DoAfterMatch();
        }

        private void MarkMatchedCells(IList<ICell> cellsToMarkList)
        {
            foreach (var cell in cellsToMarkList)
                if (cell.CurrentGameObject != null && cell.CurrentGameObject.CompareTag(Strings.Tag_Power) == false)
                    Helper.MarkCell(cell);
        }
        
        private void SwipeCells(MoveDirectionTypes direction)
        {
            int xPos = (int) Mathf.Round(_clickA.x);
            int yPos = (int) Mathf.Round(_clickA.y);

            ICell cellA = _board.Cells[xPos, yPos];
            
            if (cellA.CellType == CellTypes.Normal && cellA != null && cellA.CurrentGameObject != null)
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

                                OnEvent(EventTypes.Swipe, commands);
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

                                OnEvent(EventTypes.Swipe, commands);
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

                                OnEvent(EventTypes.Swipe, commands);
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

                                OnEvent(EventTypes.Swipe, commands);
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
      
        #region Notifier implimentation
        public void AddSubscriber(ISubscriber subscriber)
        {
            if (subscriber != null && !_subscribes.Contains(subscriber))
                _notifier.AddSubscriber(subscriber);
        }

        public void RemoveSubscriber(ISubscriber subscriber)
        {
            if (subscriber != null && _subscribes.Contains(subscriber))
                _notifier.RemoveSubscriber(subscriber);
        }

        public void Notify(EventTypes eventType, object messageData)
        {
            _notifier.Notify(eventType, messageData);
        }

        public INotifier Notifier
        {
            get { return _notifier; }
            set { _notifier = value; }
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
