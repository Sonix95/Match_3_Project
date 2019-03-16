using Mathc3Project.Classes;
using Mathc3Project.Classes.Cells;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Cells;
using NUnit.Framework;
using Tests.Static;

namespace Tests.EditMode
{
    public class OtherObjectsCreateTests
    {
        [Test]
        public void CellRegistrator_Create_NotNull()
        {
            ICellRegistrator cellRegistrator = ObjectsCreator.CreateCellRegistrator();

            Assert.IsNotNull(cellRegistrator);
        }

        [Test]
        public void Coroutine_Create_NotNull()
        {
            ICoroutiner coroutiner = ObjectsCreator.CreateCoroutiner();

            Assert.IsNotNull(coroutiner);
        }

        [Test]
        public void Level_Create_NotNull()
        {
            int levelId = 2;
            int boardWidth = 3;
            int boardHeight = 9;

            ILevelTask levelTaskA = ObjectsCreator.CreateLevelTask(Strings.Tag_RedCircle, 5);
            ILevelTask levelTaskB = ObjectsCreator.CreateLevelTask(Strings.Tag_BlueMultiAngle, 2); 
            
            ILevelTask[] levelTasks = new ILevelTask[] {levelTaskA, levelTaskB};

            ILevel level = new Level(levelId, boardWidth, boardHeight, levelTasks);
            
            Assert.IsNotNull(level);
        }

        [Test]
        public void LevelTask_Create_NotNull()
        {
            ILevelTask levelTask = ObjectsCreator.CreateLevelTask(Strings.Tag_RedCircle, 5);
            
            Assert.IsNotNull(levelTask);
        }

        [Test]
        public void ObjectStorage_Create_NotNull()
        {
            IObjectStorage objectStorage = new ObjectStorage();
            
            Assert.IsNotNull(objectStorage);
        }

        [Test]
        public void NormalCell_Create_NotNull()
        {
            ICell normalCell = new NormalCell(0,0);
            
            Assert.IsNotNull(normalCell);
        }
        
        [Test]
        public void HollowCell_Create_NotNull()
        {
            ICell hollowCell = new HollowCell(0,0);
            
            Assert.IsNotNull(hollowCell);
        }
    }
}
