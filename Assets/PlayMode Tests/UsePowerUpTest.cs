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
        public IEnumerator PowerUp_Use_Review(PowerUpTypes powerUpType)
        {
            IMasterManager masterManager;
            IBoard board = ObjectsCreator.CreateBoard(9, 9, out masterManager);
            IUpdateManager updateManager = masterManager.UpdateManager;
            IGameplayLogicManager gameplayLogicManager = ObjectsCreator.CreateGameplayLogicManager();
            INotifier gameplayNotifier = masterManager.GameplayNotifier;
            ISpawnManager spawnManager = masterManager.SpawnManager;
            IInputManager inputManager = new InputManager(gameplayNotifier);
            ICheckManager checkManager = new CheckManager();
            ICellRegistrator cellRegistrator = new CellRegistrator(gameplayNotifier, updateManager);

            ICell cellWithPowerUp = new NormalCell(4, 4);
            Vector3 cellPosition = new Vector2(4, 4);
            cellWithPowerUp.CurrentGameObject = spawnManager.SpawnPowerPrefab(powerUpType, cellPosition);
            cellRegistrator.RegistrateNormalCell(cellWithPowerUp as NormalCell);
            board.Cells[cellWithPowerUp.TargetX, cellWithPowerUp.TargetY] = cellWithPowerUp;

            yield return new WaitForSeconds(0.5f);

            board.Initial();

            checkManager.Board = board;

            gameplayLogicManager.Board = board;
            gameplayLogicManager.CheckManager = checkManager;
            gameplayLogicManager.SpawnManager = spawnManager;
            gameplayLogicManager.Notifier = new Notifier();

            inputManager.AddSubscriber(gameplayLogicManager);
            updateManager.AddUpdatable(inputManager as IUpdatable);

            updateManager.IsUpdate = true;

            yield return new WaitForSeconds(0.3f);

            AxisTypes majorAxis = AxisTypes.Undefined;
            IDictionary<IList<ICell>, AxisTypes> matchedCellsDictionary = new Dictionary<IList<ICell>, AxisTypes>();
            IDictionary<ICell, IDictionary<IList<ICell>, AxisTypes>> matchedCellsWithAxisDictionary =
                new Dictionary<ICell, IDictionary<IList<ICell>, AxisTypes>>();

            IList<ICell> cellsList = new List<ICell>();
            cellsList.Add(cellWithPowerUp);

            matchedCellsDictionary.Add(cellsList, majorAxis);
            matchedCellsWithAxisDictionary.Add(cellWithPowerUp, matchedCellsDictionary);

            yield return new WaitForSeconds(0.3f);

            masterManager.Coroutiner.StartCoroutine(
                gameplayLogicManager.MarkAndDestroy(matchedCellsWithAxisDictionary));

            //Clear Test Scene
            yield return new WaitForSeconds(3f);
            updateManager.IsUpdate = false;
            foreach (var cell in board.Cells)
                GameObject.Destroy(cell.CurrentGameObject);
        }

    }
}
