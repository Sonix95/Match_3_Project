using Mathc3Project.Classes;
using Mathc3Project.Classes.Cells;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Cells;
using NUnit.Framework;
using Test.Static;

namespace Editor.Tests.EditModeTests
{
    public class OtherObjectsCreateTests
    {
        [Test]
        public void CellRegistratorCreate_Test()
        {
            ICellRegistrator cellRegistrator = ObjectsCreator.CreateCellRegistrator();

            Assert.IsNotNull(cellRegistrator);
        }

        [Test]
        public void CoroutineCreate_Test()
        {
            ICoroutiner coroutiner = ObjectsCreator.CreateCoroutiner();

            Assert.IsNotNull(coroutiner);
        }

        [Test]
        public void LevelCreate_Test()
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
        public void LevelTaskCreate_Test()
        {
            ILevelTask levelTask = ObjectsCreator.CreateLevelTask(Strings.Tag_RedCircle, 5);
            
            Assert.IsNotNull(levelTask);
        }

        [Test]
        public void ObjectStorageCreate_Test()
        {
            IObjectStorage objectStorage = new ObjectStorage();
            
            Assert.IsNotNull(objectStorage);
        }

        [Test]
        public void NormalCellCreate_Test()
        {
            ICell normalCell = new NormalCell(0,0);
            
            Assert.IsNotNull(normalCell);
        }
        
        [Test]
        public void HollowCellCreate_Test()
        {
            ICell hollowCell = new HollowCell(0,0);
            
            Assert.IsNotNull(hollowCell);
        }
        
    }
}
