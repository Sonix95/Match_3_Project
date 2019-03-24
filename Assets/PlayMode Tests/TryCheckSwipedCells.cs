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
        [TestCase(2, true, ExpectedResult = null)]
        [TestCase(2, false, ExpectedResult = null)]
        [TestCase(3, true, ExpectedResult = null)]
        [TestCase(3, false, ExpectedResult = null)]
        [TestCase(4, true, ExpectedResult = null)]
        [TestCase(4, false, ExpectedResult = null)]
        [TestCase(7, true, ExpectedResult = null)]
        [TestCase(7, false, ExpectedResult = null)]
        public IEnumerator Cells_TryCheck(int equalsCellsCount, bool isVertical)
        {
            #region Create Managers And Board

            IMasterManager masterManager;
            ICellRegistrator cellRegistrator;
            IBoard board = ObjectsCreator.CreateBoard(equalsCellsCount + 3, equalsCellsCount + 3, out masterManager, out cellRegistrator);
            IUpdateManager updateManager = masterManager.UpdateManager;
            IGameplayLogicManager gameplayLogicManager = ObjectsCreator.CreateGameplayLogicManager();
            INotifier gameplayNotifier = masterManager.GameplayNotifier;
            ISpawnManager spawnManager = masterManager.SpawnManager;
            IInputManager inputManager = new InputManager(gameplayNotifier);
            ICheckManager checkManager = new CheckManager();

            #endregion

            #region Create Cells

            int startX = 2;
            int startY = 2;
            ICell swipedCell = null;

            for (int i = 0; i < equalsCellsCount; i++)
            {
                int x = startX;
                int y = startY;

                if (isVertical)
                {
                    if (i == Mathf.Floor(equalsCellsCount / 2))
                        x = startX + 1;
                    else
                        x = startX;

                    y = startY + i;
                }
                else
                {
                    if (i == Mathf.Floor(equalsCellsCount / 2))
                        y = startY + 1;
                    else
                        y = startY;

                    x = startX + i;
                }

                ICell cell = new NormalCell(x, y);
                cell.CurrentGameObject = spawnManager.SpawnPrefab(GameElementTypesEnum.RedCircle, new Vector3(x, y, 0));
                cellRegistrator.RegistrateNormalCell(cell as NormalCell);
                board.Cells[cell.TargetX, cell.TargetY] = cell;

                if (i == Mathf.Floor(equalsCellsCount / 2))
                    swipedCell = board.Cells[cell.TargetX, cell.TargetY];
            }

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

            ICommand[] commands;

            if (isVertical)
            {
                commands = new ICommand[]
                {
                    new SwipeLeftCommand(swipedCell),
                    new SwipeRightCommand(board.Cells[swipedCell.TargetX - 1, swipedCell.TargetY]),
                };
            }
            else
            {
                commands = new ICommand[]
                {
                    new SwipeDownCommand(swipedCell),
                    new SwipeUpCommand(board.Cells[swipedCell.TargetX, swipedCell.TargetY - 1]),
                };
            }

            ICommand macroCommand = new MacroCommand(commands);

            gameplayLogicManager.MacroCommand = macroCommand;

            #endregion

            #region Try Swap Cells and get Check

            gameplayLogicManager.MacroCommand.Execute();

            yield return new WaitForSeconds(1f);

            gameplayLogicManager.TryCheckSwipedCells(swipedCell);

            #endregion

            #region Remove From Scene

            yield return new WaitForSeconds(0.2f);
            updateManager.IsUpdate = false;
            foreach (var boadrdCell in board.Cells)
                updateManager.RemoveUpdatable(boadrdCell as IUpdatable);
            foreach (var cell in board.Cells)
                GameObject.Destroy(cell.CurrentGameObject);

            #endregion
        }

    }
}
