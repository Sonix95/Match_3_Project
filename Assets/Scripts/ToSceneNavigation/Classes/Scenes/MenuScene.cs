using System.Collections;
using Mathc3Project.Classes;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;
using ToSceneNavigation.Classes.Abstract;
using UnityEngine;
using Object = System.Object;

namespace ToSceneNavigation.Classes
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
            Initial();
            SetUp();
        }
        
        private void Initial()
        {
            _uiNotifier = _NavigationManager.MasterManager.UINotifier;
            
            _levelsManager = new LevelsManager();
            _buttonsManager =  new ButtonsMenuManager(_uiNotifier);
            _uiManager = new UIMenuManager(_NavigationManager, _levelsManager);
        }

        private void SetUp()
        {
            _buttonsManager.AddSubscriber(_uiManager);
        }

        public override void OnExit()
        {
            Debug.Log("Exit from Menu Scene");
        }
    }
}