using Mathc3Project.Classes;
using Mathc3Project.Classes.Observer;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;
using UnityEngine;

namespace Mathc3Project.Main
{
    public class Bootstrapper : MonoBehaviour
    {
        private IButtonsManager _buttonsManager;
        private IUIManager _uiManager;
        
        private INotifier _uiNotifier;

        private void Start()
        {
            Initial();
            SetUp();
        }

        private void Initial()
        {
            _uiNotifier = new Notifier();
            
            _buttonsManager =  new ButtonsManager(_uiNotifier);
            _uiManager = new UIManager();
        }

        private void SetUp()
        {
            _buttonsManager.AddSubscriber(_uiManager);
        }
        
    }
}
