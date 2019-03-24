using System.Collections;
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
    public class MenuScene : BaseScene
    {
        private IButtonsManager _buttonsManager;
        private ILevelsManager _levelsManager;
        private IUIManager _uiManager;
        
        private INotifier _uiNotifier;

        public override void OnEnter(Object transferObject)
        {
            Debug.Log("Enter in Menu Scene");
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
            
            _levelsManager = new LevelsManager();
            _buttonsManager =  new ButtonsMenuManager(_uiNotifier);
            _uiManager = new UIMenuManager(_levelsManager);
        }

        private void SetUp()
        {
            _uiManager.NavigationManager = _NavigationManager;
            _buttonsManager.AddSubscriber(_uiManager);
            _buttonsManager.Notify(EventTypesEnum.UI_SceneLoaded,null);
        }

        public override void OnExit()
        {
            Debug.Log("Exit from Menu Scene");            
            _buttonsManager.RemoveSubscriber(_uiManager);
            _NavigationManager.MasterManager.UINotifier = new Notifier();
        }
    }
}