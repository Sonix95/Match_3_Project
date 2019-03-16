using System.Collections;
using System.Collections.Generic;
using Mathc3Project.Classes;
using Mathc3Project.Classes.Cells;
using Mathc3Project.Classes.Observer;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Cells;
using Mathc3Project.Interfaces.Observer;
using NUnit.Framework;
using Tests.Static;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class UsePowerUpTest
    {
        [UnityTest]
        [TestCase(PowerUpTypes.Bomb, ExpectedResult = null)]
        [TestCase(PowerUpTypes.Vertical, ExpectedResult = null)]
        [TestCase(PowerUpTypes.Horizontal, ExpectedResult = null)]
        public IEnumerator PowerUp_UseOnePowerUp_Review(PowerUpTypes powerUpType)
        {
            #region Create Managers

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

            #region Create And SetUp Cell with PowerUp

            ICell cellWithPowerUp = new NormalCell(4, 4);
            Vector3 cellPosition = new Vector2(4, 4);
            cellWithPowerUp.CurrentGameObject = spawnManager.SpawnPowerPrefab(powerUpType, cellPosition);
            cellRegistrator.RegistrateNormalCell(cellWithPowerUp as NormalCell);
            board.Cells[cellWithPowerUp.TargetX, cellWithPowerUp.TargetY] = cellWithPowerUp;

            #endregion

            yield return new WaitForSeconds(0.1f);

            #region SetUp Board and Managers

            board.Initial();

            checkManager.Board = board;

            gameplayLogicManager.Board = board;
            gameplayLogicManager.CheckManager = checkManager;
            gameplayLogicManager.SpawnManager = spawnManager;
            gameplayLogicManager.Notifier = new Notifier();

            inputManager.AddSubscriber(gameplayLogicManager);
            updateManager.AddUpdatable(inputManager as IUpdatable);

            updateManager.IsUpdate = true;

            #endregion

            yield return new WaitForSeconds(0.3f);

            #region Try Use PowerUp

            int swipeCount = 2;
            gameplayLogicManager.TryCheckSwipedCells(cellWithPowerUp, swipeCount);

            #endregion

            #region Remove From Scene

            yield return new WaitForSeconds(2f);
            updateManager.IsUpdate = false;
            foreach (var cell in board.Cells)
                GameObject.Destroy(cell.CurrentGameObject);

            #endregion
        }

        [UnityTest]
        [TestCase(PowerUpTypes.Bomb, PowerUpTypes.Vertical, ExpectedResult = null)]
        [TestCase(PowerUpTypes.Bomb, PowerUpTypes.Horizontal, ExpectedResult = null)]
        [TestCase(PowerUpTypes.Vertical, PowerUpTypes.Horizontal, ExpectedResult = null)]
        [TestCase(PowerUpTypes.Horizontal, PowerUpTypes.Vertical, ExpectedResult = null)]
        public IEnumerator PowerUp_UseTwoPowerUp_Review(PowerUpTypes powerUpTypeA, PowerUpTypes powerUpTypeB)
        {
            #region Create Managers

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

            #region Create And SetUp Cells with PowerUp

            IList<ICell> cellsWithPowerUp = new List<ICell>();

            IList<Vector3> positions = new List<Vector3>();

            switch (powerUpTypeA)
            {
                case PowerUpTypes.Bomb:
                    positions.Add(new Vector2(4, 4));
                    positions.Add(new Vector2(2, 4));
                    break;
                case PowerUpTypes.Vertical:
                    positions.Add(new Vector2(3, 3));
                    positions.Add(new Vector2(3, 6));
                    break;
                case PowerUpTypes.Horizontal:
                    positions.Add(new Vector2(3, 3));
                    positions.Add(new Vector2(6, 3));
                    break;
            }

            for (int i = 0; i < 2; i++)
            {
                ICell cell = new NormalCell((int) positions[i].x, (int) positions[i].y);
                Vector3 cellPosition = new Vector2((int) positions[i].x, (int) positions[i].y);
                cell.CurrentGameObject =
                    spawnManager.SpawnPowerPrefab(i == 0 ? powerUpTypeA : powerUpTypeB, positions[i]);
                cellRegistrator.RegistrateNormalCell(cell as NormalCell);
                board.Cells[cell.TargetX, cell.TargetY] = cell;
                cellsWithPowerUp.Add(cell);
            }

            #endregion

            yield return new WaitForSeconds(0.1f);

            #region SetUp Board and Managers

            board.Initial();

            checkManager.Board = board;

            gameplayLogicManager.Board = board;
            gameplayLogicManager.CheckManager = checkManager;
            gameplayLogicManager.SpawnManager = spawnManager;
            gameplayLogicManager.Notifier = new Notifier();

            inputManager.AddSubscriber(gameplayLogicManager);
            updateManager.AddUpdatable(inputManager as IUpdatable);

            updateManager.IsUpdate = true;

            #endregion

            yield return new WaitForSeconds(0.3f);

            #region Try Use PowerUp

            int swipeCount = 2;
            gameplayLogicManager.TryCheckSwipedCells(cellsWithPowerUp[0], swipeCount);

            #endregion

            #region Remove From Scene

            yield return new WaitForSeconds(2f);
            updateManager.IsUpdate = false;
            foreach (var cell in board.Cells)
                GameObject.Destroy(cell.CurrentGameObject);

            #endregion
        }

    }
}
