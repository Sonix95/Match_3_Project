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
        public IEnumerator Board_Create_NotNull()
        {
            IBoard board = ObjectsCreator.CreateBoard(9, 12);
            
            yield return null;

            Assert.IsNotNull(board);
        }
        
    }
}

