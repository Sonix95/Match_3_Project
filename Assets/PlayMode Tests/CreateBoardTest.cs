using System.Collections;
using Mathc3Project.Interfaces;
using NUnit.Framework;
using Tests.Static;
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
            IBoard board = ObjectsCreator.CreateBoard(width, height);
            
            yield return null;
            
            board.Initial();

            Assert.IsNotNull(board);
        }
        
    }
}

