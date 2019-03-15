using Mathc3Project.Classes;
using Mathc3Project.Classes.Observer;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;
using NUnit.Framework;

namespace Editor.Tests
{
   public class ManagersCreateTests
   {
      [Test]
      public void CheckManagerCreate_Test()
      {
         ICheckManager checkManager = new CheckManager();
         
         Assert.IsNotNull(checkManager);
      }
      
      [Test]
      public void GameplayLogicManager()
      {
         IGameplayLogicManager gameplayLogicManager =  ObjectsCreator.CreateGameplayLogicManager();

         Assert.IsNotNull(gameplayLogicManager);
      }
      
      [Test]
      public void InputManagerCreateTest()
      {
         INotifier somethingNotifier = new Notifier();
         IInputManager inputManager = new InputManager(somethingNotifier);

         Assert.IsNotNull(inputManager);
      }
      
      [Test]
      public void LevelsManagerCreate_Test()
      {
         ILevelsManager levelManager = new LevelsManager();

         Assert.IsNotNull(levelManager);
      }
      
      [Test]
      public void MasterManagerCreate_Test()
      {
         IMasterManager masterManager =  ObjectsCreator.CreateMasterManager();

         Assert.IsNotNull(masterManager);
      }
      
      [Test]
      public void NavigationManagerCreate_Test()
      {
         IMasterManager masterManager =  ObjectsCreator.CreateMasterManager();

         INavigationManager navigationManager = new NavigationManager(masterManager);

         Assert.IsNotNull(navigationManager);
      }
      
      [Test]
      public void SpawnManagerCreate_Test()
      {
         IObjectStorage objectStorage = new ObjectStorage();
         ISpawnManager spawnManager = new SpawnManager(objectStorage);

         Assert.IsNotNull(spawnManager);
      }
      
      [Test]
      public void TaskManagerCreate_Test()
      {
         ITaskManager taskManager = ObjectsCreator.CreateTaskManager();

         Assert.IsNotNull(taskManager);
      }

      [Test]
      public void UpdateManagerCreate_Test()
      {
         IUpdateManager updateManager =  ObjectsCreator.CreateUpdateManager();

         Assert.IsNotNull(updateManager);
      }

      [Test]
      public void GameplayNotifierCreate_Test()
      {
         INotifier gameplayNotifier = new Notifier();
         
         Assert.IsNotNull(gameplayNotifier);
      }

      [Test]
      public void UINotifierCreate_Test()
      {
         INotifier uiNotifier = new Notifier();

         Assert.IsNotNull(uiNotifier);
      }

   }
}
