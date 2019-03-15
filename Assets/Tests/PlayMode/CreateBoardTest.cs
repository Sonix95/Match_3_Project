using System.Collections;
using Mathc3Project.Interfaces;
using NUnit.Framework;
using Test.Static;
using UnityEngine.TestTools;

namespace  Editor.Tests.PlayModeTests
{
    public class CreateBoardTest
    {
        [UnityTest]
        public IEnumerator CreateBoard_Test()
        {
            IBoard board = ObjectsCreator.CreateBoard(9, 12);
            
            yield return null;

            Assert.IsNotNull(board);
        }
    }
}
