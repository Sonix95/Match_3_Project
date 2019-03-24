using System.Collections;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Interfaces;
using NUnit.Framework;
using Tests.Static;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class CreateBoardFromLevelTest
    {
        [UnityTest]
        public IEnumerator Board_CreateFromLevel_NotNull()
        {
            #region create level
            
            int levelID = 1;
            int boardWidth = 5;
            int boardHeight = 5;

            ILevelTask levelTaskA = ObjectsCreator.CreateLevelTask(Strings.TAG_REDCIRCLE, 5);
            ILevelTask levelTaskB = ObjectsCreator.CreateLevelTask(Strings.TAG_BLUEMULTIANGLE, 12);
            ILevelTask[] levelTasks = new[] {levelTaskA, levelTaskB};
            
            ILevel level = ObjectsCreator.CreateLevel(levelID, boardWidth, boardHeight,levelTasks);
            
            #endregion
            
            #region create Board
            int width = level.BoardWidth;
            int height = level.BoardHeight;
            
            IBoard board = ObjectsCreator.CreateBoard(width, height);
            
            #endregion
            
            #region Check board
            
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
