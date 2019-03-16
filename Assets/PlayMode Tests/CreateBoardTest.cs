using System.Collections;
using Mathc3Project.Interfaces;
using NUnit.Framework;
using Tests.Static;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class CreateBoardTest
    {
        [UnityTest]
        [TestCase(3, 5, ExpectedResult = null)]
        [TestCase(7, 4, ExpectedResult = null)]
        [TestCase(6, 6, ExpectedResult = null)]
        public IEnumerator Board_Create_NotNull(int width, int height)
        {
            #region Create Board

            IBoard board = ObjectsCreator.CreateBoard(width, height);

            #endregion

            #region Check Board

            board.Initial();
            Assert.IsNotNull(board);

            #endregion

            #region Remove From Scene

            yield return new WaitForSeconds(0.25f);
            foreach (var cell in board.Cells)
                GameObject.Destroy(cell.CurrentGameObject);

            #endregion
        }

    }
}

