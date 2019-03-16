using System.Collections;
using System.Collections.Generic;
using Mathc3Project.Classes;
using Mathc3Project.Classes.Cells;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Cells;
using NUnit.Framework;
using Tests.Static;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class CreateBoardWithOnlyOneElementTypeTest
    {
        [UnityTest]
        [TestCase(GameElementTypes.OrangeBox, 5, 5, ExpectedResult = null)]
        [TestCase(GameElementTypes.RedCircle, 7, 8, ExpectedResult = null)]
        [TestCase(GameElementTypes.GreenDownTriangle, 3, 3, ExpectedResult = null)]
        [TestCase(GameElementTypes.YellowUpTriangle, 5, 2, ExpectedResult = null)]
        [TestCase(GameElementTypes.BlueMultiAngle, 9, 4, ExpectedResult = null)]
        public IEnumerator Board_CreateWithOnlyOneElementType(GameElementTypes gameElement, int boardWidth,
            int boardHeight)
        {
            #region Create Board

            IObjectStorage objectStorage = new ObjectStorage();
            ISpawnManager spawnManager = new SpawnManager(objectStorage);
            IBoard board = ObjectsCreator.CreateBoard(boardWidth, boardHeight);

            #endregion

            yield return null;

            #region Fill Board

            for (int i = 0; i < board.Width; i++)
            for (int j = 0; j < board.Height; j++)
            {
                if (board.Cells[i, j] == null)
                {
                    Vector2 tempPos = new Vector2(i, j);

                    ICell newCell = spawnManager.SpawnNormalCell(gameElement, tempPos);
                    board.Cells[i, j] = newCell;

                    yield return new WaitForSeconds(.1f);
                }
            }

            #endregion

            #region Remove From Scene

            yield return new WaitForSeconds(.5f);
            foreach (var cell in board.Cells)
                GameObject.Destroy(cell.CurrentGameObject);

            #endregion
        }

    }
}
