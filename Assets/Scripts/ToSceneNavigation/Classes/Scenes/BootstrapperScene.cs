using DefaultNamespace;
using Mathc3Project.Classes;
using Mathc3Project.Classes.Observer;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;
using ToSceneNavigation.Classes.Abstract;
using UnityEngine;

namespace ToSceneNavigation.Classes
{
    public class BootstrapperScene : BaseScene
    {
        private ICoroutiner _coroutiner;
        private IUpdateManager _updateManager;

        private INotifier _gameplayNotifier;
        private INotifier _uiNotifier;

        private IObjectStorage _objectStorage;
        private IObjectSetter _objectSetter;

        private ISpawnManager _spawnManager;

        private void Start()
        {
            _coroutiner = new GameObject(Strings.Coroutiner).AddComponent<Coroutiner>();
            _updateManager = new GameObject(Strings.Update_Manager).AddComponent<UpdateManager>();

            _gameplayNotifier = new Notifier();
            _uiNotifier = new Notifier();

            _objectStorage = new ObjectStorage();
            _objectSetter = new ObjectSetter(_updateManager, _gameplayNotifier);

            _spawnManager = new SpawnManager(_objectStorage, _objectSetter);

            IMasterManager masterManager = new MasterManager(_coroutiner, _updateManager, _gameplayNotifier,
                _uiNotifier, _objectStorage, _objectSetter, _spawnManager);

            INavigationManager navigationManager = new NavigationManager(masterManager);

            navigationManager.Navigate(SceneTypes.BootstrapperScene, SceneTypes.StartScreenScene, null);
        }

        public override void OnExit()
        {
            Debug.Log("Exit From Scene Bootstrapper");
        }

        public override void OnEnter(object transferObject)
        {
            // -- no implementation -- //
        }

    }
}
