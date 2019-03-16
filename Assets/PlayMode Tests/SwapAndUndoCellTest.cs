using System.Collections;
using Mathc3Project.Classes;
using Mathc3Project.Classes.Cells;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Cells;
using Mathc3Project.Interfaces.Command;
using Mathc3Project.Interfaces.Observer;
using NUnit.Framework;
using Tests.Enum;
using Tests.Static;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class SwapAndUndoCellTest
    {
        [UnityTest]
        [TestCase(SwapTypes.Up, ExpectedResult = null)]
        [TestCase(SwapTypes.Down, ExpectedResult = null)]
        [TestCase(SwapTypes.Left, ExpectedResult = null)]
        [TestCase(SwapTypes.Right, ExpectedResult = null)]
        public IEnumerator Cell_SwapAndUndo(SwapTypes swapType)
        {
            #region Create Managers

            IMasterManager masterManager = ObjectsCreator.CreateMasterManager();

            ISpawnManager spawnManager = masterManager.SpawnManager;
            INotifier notifier = masterManager.GameplayNotifier;
            IUpdateManager updateManager = masterManager.UpdateManager;
            ICellRegistrator cellRegistrator = new CellRegistrator(notifier, updateManager);

            #endregion

            #region Create Cell

            Vector3 position = new Vector3(1, 1, 0);
            ICell cellA = spawnManager.SpawnRandomNormalCell(position);
            cellRegistrator.RegistrateNormalCell(cellA as NormalCell);

            #endregion

            #region SetUp UpdateManager

            updateManager.AddUpdatable(cellA as IUpdatable);
            updateManager.IsUpdate = true;

            #endregion

            yield return new WaitForSeconds(0.3f);

            #region Swap Cell and do Undo

            ICommand swapCommand = TestHelper.GetSwapCommand(swapType, cellA);
            swapCommand.Execute();

            yield return new WaitForSeconds(0.5f);

            swapCommand.Undo();

            #endregion

            #region Remove From Scene

            yield return new WaitForSeconds(0.5f);
            GameObject.Destroy(cellA.CurrentGameObject);

            #endregion
        }

    }
}
