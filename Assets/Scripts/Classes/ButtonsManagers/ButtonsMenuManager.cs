using System.Collections.Generic;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;
using UnityEngine;
using UnityEngine.UI;

namespace Mathc3Project.Classes.ButtonsManager
{
    public class ButtonsMenuManager : IButtonsManager
    {
        private INotifier _notifier;
        private IList<ISubscriber> _subscribes;
        
        private Button _backToStartScreen_PanelLevels;
        private Button _prevLocation_PanelLevels;
        private Button _nextLocation_PanelLevels;
        private IDictionary<int, Button> _levelPanel_PanelLevelsHolder;
        private Button _closeLevelDescription_GameObjectDescription;
        private Button _playLevel_PanelDescription;
        private Button _closeDescriptionLevel_PanelDescription;

        public ButtonsMenuManager(INotifier notifier)
        {
            _notifier = notifier;
            _subscribes = new List<ISubscriber>();
            
            FindButtons();
            SetUpButtons();
        }

        private void FindButtons()
        {
            _levelPanel_PanelLevelsHolder = new Dictionary<int, Button>();

            _backToStartScreen_PanelLevels = GameObject.Find("Button Back To Start Screen").GetComponent<Button>();
            _prevLocation_PanelLevels = GameObject.Find("Button Prev Location").GetComponent<Button>();
            _nextLocation_PanelLevels = GameObject.Find("Button Next Location").GetComponent<Button>();

            GameObject[] levelsPanel = GameObject.FindGameObjectsWithTag("Level Panel");
            for (int i = 0; i < levelsPanel.Length; i++)
                _levelPanel_PanelLevelsHolder.Add(i, levelsPanel[i].GetComponent<Button>());

            _closeLevelDescription_GameObjectDescription =
                GameObject.Find("Image Close Level Description").GetComponent<Button>();
            _playLevel_PanelDescription = GameObject.Find("Button Play Level").GetComponent<Button>();
            _closeDescriptionLevel_PanelDescription = GameObject.Find("Image Close Description").GetComponent<Button>();
        }

        private void SetUpButtons()
        {
            _backToStartScreen_PanelLevels.onClick.AddListener(BackToStartScreen);
            _prevLocation_PanelLevels.onClick.AddListener(LoadPrevLocation);
            _nextLocation_PanelLevels.onClick.AddListener(LoadNextLocation);
            
            foreach (var levelPanel in _levelPanel_PanelLevelsHolder)
            {
                int level = levelPanel.Key;
                levelPanel.Value.onClick.AddListener(delegate { OpenLevelDescription(level); });
            }
            
            _closeLevelDescription_GameObjectDescription.onClick.AddListener(CloseLevelDescription);
            _playLevel_PanelDescription.onClick.AddListener(StartLevel);
            _closeDescriptionLevel_PanelDescription.onClick.AddListener(CloseLevelDescription);
        }

        #region Listener's for Buttons
        
        private void BackToStartScreen()
        {
            Notify(EventTypes.UI_BackToStartScreen, null);
        }

        private void LoadPrevLocation()
        {
            Notify(EventTypes.UI_PrevLocation, null);
        }
        
        private void LoadNextLocation()
        {
            Notify(EventTypes.UI_NextLocation, null);
        }

        private void OpenLevelDescription(int level)
        {
            Notify(EventTypes.UI_OpenLevelDescription, level);
        }
        
        private void CloseLevelDescription()
        {
            Notify(EventTypes.UI_CLoseLevelDescription, null);
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
