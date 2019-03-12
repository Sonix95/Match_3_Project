using System.Collections.Generic;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;
using UnityEngine;
using UnityEngine.UI;

namespace Mathc3Project.Classes.ButtonsManager
{
    public class ButtonsGameplaySceneManager : IButtonsManager
    {
        private ILevel _level;

        private INotifier _notifier;
        private IList<ISubscriber> _subscribes;

        private Button _backToMenuButton;
        private Button _restartButton;

        public ButtonsGameplaySceneManager(INotifier notifier)
        {
            _notifier = notifier;
            _subscribes = new List<ISubscriber>();

            FindButtons();
            SetUpButtons();
        }

        private void FindButtons()
        {
            _backToMenuButton = GameObject.Find("Button Back To Menu").GetComponent<Button>();
            _restartButton = GameObject.Find("Button Restart").GetComponent<Button>();
        }

        private void SetUpButtons()
        {
            _backToMenuButton.onClick.AddListener(BackToMenu);
            _restartButton.onClick.AddListener(StartLevel);
        }

        #region Listener's for Buttons

        private void BackToMenu()
        {
            Notify(EventTypes.UI_OpenMenu, null);
        }

        private void StartLevel()
        {
            Notify(EventTypes.UI_PlayLevel, null);
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

        public void Notify(EventTypes eventType, object messageData)
        {
            _notifier.Notify(eventType, messageData);
        }

        public INotifier Notifier
        {
            get { return _notifier; }
            set { _notifier = value; }
        }

        #endregion
    }
}
