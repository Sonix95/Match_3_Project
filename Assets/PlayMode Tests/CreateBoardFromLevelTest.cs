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
            int levelID = 1;
            int boardWidth = 5;
            int boardHeight = 5;

            ILevelTask levelTaskA = ObjectsCreator.CreateLevelTask(Strings.Tag_RedCircle, 5);
            ILevelTask levelTaskB = ObjectsCreator.CreateLevelTask(Strings.Tag_BlueMultiAngle, 12);
            ILevelTask[] levelTasks = new[] {levelTaskA, levelTaskB};
            
            ILevel level = ObjectsCreator.CreateLevel(levelID, boardWidth, boardHeight,levelTasks);
            
            int width = level.BoardWidth;
            int height = level.BoardHeight;
            
            IBoard board = ObjectsCreator.CreateBoard(width, height);
            
            yield return null; 
            
            board.Initial();

            Assert.IsNotNull(board);

            //Remove from Scene    
            foreach (var cell in board.Cells)
                GameObject.Destroy(cell.CurrentGameObject);
        }
        
    }
}
