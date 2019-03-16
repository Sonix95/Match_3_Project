using Mathc3Project.Classes;
using Mathc3Project.Classes.Observer;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;
using NUnit.Framework;
using Tests.Static;

namespace Tests.EditMode
{
    public class ManagersCreateTests
    {
        [Test]
      public void CheckManager_Create_NotNull()
      {
         ICheckManager checkManager = new CheckManager();
         
         Assert.IsNotNull(checkManager);
      }
      
      [Test]
      public void GameplayLogicManager_Create_NotNull()
      {
         IGameplayLogicManager gameplayLogicManager =  ObjectsCreator.CreateGameplayLogicManager();

         Assert.IsNotNull(gameplayLogicManager);
      }
      
      [Test]
      public void InputManager_Create_NotNull()
      {
         INotifier somethingNotifier = new Notifier();
         IInputManager inputManager = new InputManager(somethingNotifier);

         Assert.IsNotNull(inputManager);
      }
      
      [Test]
      public void LevelsManager_Create_NotNull()
      {
         ILevelsManager levelManager = new LevelsManager();

         Assert.IsNotNull(levelManager);
      }
      
      [Test]
      public void MasterManager_Create_NotNull()
      {
         IMasterManager masterManager =  ObjectsCreator.CreateMasterManager();

         Assert.IsNotNull(masterManager);
      }
      
      [Test]
      public void NavigationManager_Create_NotNull()
      {
         IMasterManager masterManager =  ObjectsCreator.CreateMasterManager();

         INavigationManager navigationManager = new NavigationManager(masterManager);

         Assert.IsNotNull(navigationManager);
      }
      
      [Test]
      public void SpawnManager_Create_NotNull()
      {
         IObjectStorage objectStorage = new ObjectStorage();
         ISpawnManager spawnManager = new SpawnManager(objectStorage);

         Assert.IsNotNull(spawnManager);
      }
      
      [Test]
      public void TaskManager_Create_NotNull()
      {
         ITaskManager taskManager = ObjectsCreator.CreateTaskManager();

         Assert.IsNotNull(taskManager);
      }

      [Test]
      public void UpdateManager_Create_NotNull()
      {
         IUpdateManager updateManager =  ObjectsCreator.CreateUpdateManager();

         Assert.IsNotNull(updateManager);
      }

      [Test]
      public void GameplayNotifier_Create_NotNull()
      {
         INotifier gameplayNotifier = new Notifier();
         
         Assert.IsNotNull(gameplayNotifier);
      }

      [Test]
      public void UINotifier_Create_NotNull()
      {
         INotifier uiNotifier = new Notifier();

         Assert.IsNotNull(uiNotifier);
      }

    }
}
