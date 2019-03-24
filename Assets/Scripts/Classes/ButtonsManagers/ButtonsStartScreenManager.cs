using System.Collections.Generic;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;
using UnityEngine;
using UnityEngine.UI;

namespace Mathc3Project.Classes.ButtonsManager
{
    public class ButtonsStartScreenManager : IButtonsManager
    {
        private INotifier _notifier;
        private IList<ISubscriber> _subscribes;

        private Button _start_PanelStartScreen;
        
        public ButtonsStartScreenManager(INotifier notifier)
        {
            _subscribes = new List<ISubscriber>();
            _notifier = notifier;
            
            FindButtons();
            SetUpButtons();
        }
        
         private void FindButtons()
        {
            _start_PanelStartScreen = GameObject.Find("Button Start Game").GetComponent<Button>();
        }

        private void SetUpButtons()
        {
            _start_PanelStartScreen.onClick.AddListener(StartGame);
        }

        #region Listener's for Buttons
        private void StartGame()
        {
            Notify(EventTypesEnum.UI_OpenMenu, null);
        }
        
        #endregion
        
        #region Notify implimentation

        public void AddSubscriber(ISubscriber subscriber)
        {
            if (subscriber != null && !_subscribes.Contains(subscriber))
                _notifier.AddSubscriber(subscriber);
        }

        public void RemoveSubscriber(ISubscriber subscriber)
        {
            if (subscriber != null && _subscribes.Contains(subscriber))
                _notifier.RemoveSubscriber(subscriber);
        }

        public void Notify(EventTypesEnum eventTypeEnum, object messageData)
        {
            _notifier.Notify(eventTypeEnum, messageData);
        }
        
        public INotifier Notifier
        {
            get { return _notifier; }
            set { _notifier = value; }
        }
        
        #endregion
        
    }
}