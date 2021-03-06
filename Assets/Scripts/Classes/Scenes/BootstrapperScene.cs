using Mathc3Project.Classes.Abstract;
using Mathc3Project.Classes.Observer;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;
using UnityEngine;

namespace Mathc3Project.Classes.Scenes
{
    public class BootstrapperScene : BaseScene
    {
        private ICoroutiner _coroutiner;
        private IUpdateManager _updateManager;

        private INotifier _gameplayNotifier;
        private INotifier _uiNotifier;

        private IObjectStorage _objectStorage;

        private ISpawnManager _spawnManager;

        private void Start()
        {
            _coroutiner = new GameObject(Strings.COROUTINER).AddComponent<Coroutiner>();
            _updateManager = new GameObject(Strings.UPDATE_MANAGER).AddComponent<UpdateManager>();

            _gameplayNotifier = new Notifier();
            _uiNotifier = new Notifier();

            _objectStorage = new ObjectStorage();

            _spawnManager = new SpawnManager(_objectStorage);

            IMasterManager masterManager = new MasterManager(_coroutiner, _updateManager, _gameplayNotifier,
                _uiNotifier, _objectStorage, _spawnManager);

            INavigationManager navigationManager = new NavigationManager(masterManager);

            navigationManager.Navigate(SceneTypesEnum.BootstrapperScene, SceneTypesEnum.StartScreenScene, null);
        }

        public override void OnExit()
        {
            // -- no implementation -- //
        }

        public override void OnEnter(object transferObject)
        {
            // -- no implementation -- //
        }

    }
}
