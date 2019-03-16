using System.Collections;
using System.Collections.Generic;
using Mathc3Project.Classes;
using Mathc3Project.Classes.Cells;
using Mathc3Project.Classes.Commands;
using Mathc3Project.Classes.Observer;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Cells;
using Mathc3Project.Interfaces.Command;
using Mathc3Project.Interfaces.Observer;
using NUnit.Framework;
using Tests.Static;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TryCheckSwipedCells
    {
        [UnityTest]
        public IEnumerator Cells_TryCheck()
        {
            #region Create Managers And Board

            IMasterManager masterManager;
            IBoard board = ObjectsCreator.CreateBoard(9, 9, out masterManager);
            IUpdateManager updateManager = masterManager.UpdateManager;
            IGameplayLogicManager gameplayLogicManager = ObjectsCreator.CreateGameplayLogicManager();
            INotifier gameplayNotifier = masterManager.GameplayNotifier;
            ISpawnManager spawnManager = masterManager.SpawnManager;
            IInputManager inputManager = new InputManager(gameplayNotifier);
            ICheckManager checkManager = new CheckManager();
            ICellRegistrator cellRegistrator = new CellRegistrator(gameplayNotifier, updateManager);

            #endregion

            #region Create Cells

            ICell cellA = new NormalCell(2, 2);
            ICell cellB = new NormalCell(2, 3);
            ICell cellC = new NormalCell(3, 4);
            ICell cellD = new NormalCell(2, 5);

            cellA.CurrentGameObject = spawnManager.SpawnPrefab(GameElementTypes.RedCircle, new Vector3(2, 2, 0));
            cellB.CurrentGameObject = spawnManager.SpawnPrefab(GameElementTypes.RedCircle, new Vector3(2, 3, 0));
            cellC.CurrentGameObject = spawnManager.SpawnPrefab(GameElementTypes.RedCircle, new Vector3(3, 4, 0));
            cellD.CurrentGameObject = spawnManager.SpawnPrefab(GameElementTypes.RedCircle, new Vector3(2, 5, 0));

            cellRegistrator.RegistrateNormalCell(cellA as NormalCell);
            cellRegistrator.RegistrateNormalCell(cellB as NormalCell);
            cellRegistrator.RegistrateNormalCell(cellC as NormalCell);
            cellRegistrator.RegistrateNormalCell(cellD as NormalCell);

            board.Cells[cellA.TargetX, cellA.TargetY] = cellA;
            board.Cells[cellB.TargetX, cellB.TargetY] = cellB;
            board.Cells[cellC.TargetX, cellC.TargetY] = cellC;
            board.Cells[cellD.TargetX, cellD.TargetY] = cellD;

            #endregion

            yield return new WaitForSeconds(1f);

            #region SetUp Board And Managers

            board.Initial();

            checkManager.Board = board;

            gameplayLogicManager.Board = board;
            gameplayLogicManager.CheckManager = checkManager;
            gameplayLogicManager.SpawnManager = spawnManager;
            gameplayLogicManager.Notifier = masterManager.UINotifier;

            inputManager.AddSubscriber(gameplayLogicManager);
            updateManager.AddUpdatable(inputManager as IUpdatable);

            updateManager.IsUpdate = true;

            #endregion

            yield return new WaitForSeconds(1f);

            #region Create And SetUp MacroCommand to use swap cells

            ICommand[] commands = new ICommand[]
            {
                new SwipeLeftCommand(cellC),
                new SwipeRightCommand(board.Cells[cellC.TargetX - 1, cellC.TargetY]),
            };

            ICommand macroCommand = new MacroCommand(commands);

            gameplayLogicManager.MacroCommand = macroCommand;

            #endregion

            #region Try Swap Cells and get Check

            gameplayLogicManager.MacroCommand.Execute();

            yield return new WaitForSeconds(1f);

            gameplayLogicManager.TryCheckSwipedCells(cellC);

            #endregion

            #region Remove From Scene
            
            yield return new WaitForSeconds(1f);
            updateManager.IsUpdate = false;
            foreach (var cell in board.Cells)
                GameObject.Destroy(cell.CurrentGameObject);

            #endregion
        }

    }
}
