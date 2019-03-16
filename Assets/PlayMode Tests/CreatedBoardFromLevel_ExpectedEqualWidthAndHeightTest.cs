using System.Collections;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Interfaces;
using NUnit.Framework;
using Tests.Static;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class CreatedBoardFromLevel_ExpectedEqualWidthAndHeightTest
    {
        [UnityTest]
        [TestCase(1, 1, ExpectedResult = null)]
        [TestCase(3, 3, ExpectedResult = null)]
        [TestCase(5, 5, ExpectedResult = null)]
        [TestCase(9, 9, ExpectedResult = null)]
        [TestCase(15, 15, ExpectedResult = null)]
        public IEnumerator Board_CreateFromLevel_ExpectedEqualWidth(int boardWidth, int expectedResult)
        {
            int levelID = 1;
            int boardHeight = 5;

            ILevelTask levelTaskA = ObjectsCreator.CreateLevelTask(Strings.Tag_RedCircle, 5);
            ILevelTask levelTaskB = ObjectsCreator.CreateLevelTask(Strings.Tag_BlueMultiAngle, 12);
            ILevelTask[] levelTasks = new[] {levelTaskA, levelTaskB};

            ILevel level = ObjectsCreator.CreateLevel(levelID, boardWidth, boardHeight, levelTasks);

            int width = level.BoardWidth;
            int height = level.BoardHeight;

            IBoard board = ObjectsCreator.CreateBoard(width, height);
            board.Initial();

            Assert.That(board.Width, Is.EqualTo(expectedResult));

            yield return new WaitForSeconds(0.25f);
            foreach (var cell in board.Cells)
                GameObject.Destroy(cell.CurrentGameObject);

        }

        [UnityTest]
        [TestCase(2, 2, ExpectedResult = null)]
        [TestCase(4, 4, ExpectedResult = null)]
        [TestCase(8, 8, ExpectedResult = null)]
        [TestCase(12, 12, ExpectedResult = null)]
        [TestCase(22, 22, ExpectedResult = null)]
        public IEnumerator Board_CreateFromLevel_ExpectedEqualHeight(int boardHeight, int expectedResult)
        {
            int levelID = 1;
            int boardWidth = 5;

            ILevelTask levelTaskA = ObjectsCreator.CreateLevelTask(Strings.Tag_RedCircle, 5);
            ILevelTask levelTaskB = ObjectsCreator.CreateLevelTask(Strings.Tag_BlueMultiAngle, 12);
            ILevelTask[] levelTasks = new[] {levelTaskA, levelTaskB};

            ILevel level = ObjectsCreator.CreateLevel(levelID, boardWidth, boardHeight, levelTasks);

            int width = level.BoardWidth;
            int height = level.BoardHeight;

            IBoard board = ObjectsCreator.CreateBoard(width, height);
            board.Initial();

            Assert.That(board.Height, Is.EqualTo(expectedResult));

            yield return new WaitForSeconds(0.25f);
            foreach (var cell in board.Cells)
                GameObject.Destroy(cell.CurrentGameObject);
        }

    }
}
