using System.Collections.Generic;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;
using UnityEngine;
using UnityEngine.UI;

namespace Mathc3Project.Classes
{
    public class ButtonsManager : IButtonsManager
    {
        private INotifier _notifier;
        private IList<ISubscriber> _subscribes;
        
        private readonly Button _openLevelButton;
        private readonly Button _toLeftButton;
        private readonly Button _toRightButton;
        private readonly Button _backToMenuButton;

        private IDictionary<int, Button> _levelPanelsButtons;

        public ButtonsManager(INotifier notifier)
        {
            _notifier = notifier;
            _subscribes = new List<ISubscriber>();
            _levelPanelsButtons = new Dictionary<int, Button>();
            
            _openLevelButton = GameObject.Find("Button Open Level Menu").GetComponent<Button>();
            _toLeftButton = GameObject.Find("Button To Left").GetComponent<Button>();
            _toRightButton = GameObject.Find("Button To Right").GetComponent<Button>();
            _backToMenuButton = GameObject.Find("Button Back To Menu").GetComponent<Button>();

            GameObject[] levelBtnPanels = GameObject.FindGameObjectsWithTag("LevelBtnPanel");
            
            for(int i = 0; i < levelBtnPanels.Length; i++)
                _levelPanelsButtons.Add(i+1,levelBtnPanels[i].GetComponent<Button>());
            
            InitUI();
        }


        private void InitUI()
        {
            _openLevelButton.onClick.AddListener(OpenLevelMenu);
            _toLeftButton.onClick.AddListener(PrevLevelLocation);
            _toRightButton.onClick.AddListener(NextLevelLocation);
            _backToMenuButton.onClick.AddListener(BackToMenu);

            foreach (var levelBtnPanel in _levelPanelsButtons)
            {
                int level = levelBtnPanel.Key;
                levelBtnPanel.Value.onClick.AddListener(delegate { SelectLevel(level); });
            }
        }

        private void OpenLevelMenu()
        {
            Notify(EventTypes.UI_OpenLevelButton, null);
        }
        
        private void PrevLevelLocation()
        {
            Notify(EventTypes.UI_PrevLocationButton, null);
        }
        
        private void NextLevelLocation()
        {
            Notify(EventTypes.UI_NextLocationButton, null);
        }
        
        private void BackToMenu()
        {
            Notify(EventTypes.UI_BackToMenuButton, null);
        }
        
        private void SelectLevel(int level)
        {
            Notify(EventTypes.UI_SelectLevel, level);
        }

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