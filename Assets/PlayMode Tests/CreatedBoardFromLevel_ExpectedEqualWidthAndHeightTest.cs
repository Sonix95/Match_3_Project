using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Interfaces;
using NUnit.Framework;
using Tests.Static;

namespace Tests.PlayMode
{
    public class CreatedBoardFromLevel_ExpectedEqualWidthAndHeightTest
    {
        [Test]
        [TestCase(1, 1)]
        [TestCase(3, 3)]
        [TestCase(5, 5)]
        [TestCase(9, 9)]
        [TestCase(15, 15)]
        public void Board_CreateFromLevel_ExpectedEqualWidth(int boardWidth, int expectedResult)
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

            Assert.That(board.Width, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(2, 2)]
        [TestCase(4, 4)]
        [TestCase(8, 8)]
        [TestCase(12, 12)]
        [TestCase(22, 22)]
        public void Board_CreateFromLevel_ExpectedEqualHeight(int boardHeight, int expectedResult)
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

            Assert.That(board.Height, Is.EqualTo(expectedResult));
        }

    }
}
