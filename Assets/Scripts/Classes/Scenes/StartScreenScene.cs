using System.Collections;
using Mathc3Project.Classes;
using Mathc3Project.Classes.Abstract;
using Mathc3Project.Classes.ButtonsManager;
using Mathc3Project.Classes.Observer;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;
using UnityEngine;
using Object = System.Object;

namespace Mathc3Project.Classes.Scenes
{
    public class StartScreenScene : BaseScene
    {
        private IButtonsManager _buttonsManager;
        private IUIManager _uiManager;
        
        private INotifier _uiNotifier;

        public override void OnEnter(Object transferObject)
        {
            Debug.Log("Enter in Start Screen Scene");
            _NavigationManager.MasterManager.Coroutiner.StartCoroutine(InitialAndSetup());
        }

        private IEnumerator InitialAndSetup()
        {
            yield return null;
            Initial();
            SetUp();
        }
        
        private void Initial()
        {
            _uiNotifier = _NavigationManager.MasterManager.UINotifier;
            _buttonsManager =  new ButtonsStartScreenManager(_uiNotifier);
            _uiManager = new UIStartScreenSceneManager();
        }

        private void SetUp()
        {
            _uiManager.NavigationManager = _NavigationManager;
            _buttonsManager.AddSubscriber(_uiManager);
            _buttonsManager.Notify(EventTypesEnum.UI_SceneLoaded,null);
        }

        public override void OnExit()
        {
            Debug.Log("Exit from Start Screen Scene");
            _NavigationManager.MasterManager.UINotifier = new Notifier();
        }
    }
}