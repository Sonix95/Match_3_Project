using System.Collections.Generic;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Mathc3Project.Classes
{
    public class UIManager : IUIManager
    {
        private GameObject _levelPanel;

        public UIManager()
        {
            _levelPanel = GameObject.Find("Panel Levels");
            
            OnEvent(EventTypes.UI_FirstLoad, null);
        }

        public void OnEvent(EventTypes eventType, object messageData)
        {
            switch (eventType)
            {
                case EventTypes.UI_FirstLoad:
                    _levelPanel.SetActive(false);
                    break;
                case EventTypes.UI_OpenLevelButton:
                    _levelPanel.SetActive(true);
                    break;
                case EventTypes.UI_NextLocationButton:
                    Debug.Log("Next Location Menu");
                    break;
                case EventTypes.UI_PrevLocationButton:
                    Debug.Log("Prev Location Menu");
                    break;
                case EventTypes.UI_BackToMenuButton:
                    Debug.Log("Go To Main Menu");
                    _levelPanel.SetActive(false);
                    break;
                case EventTypes.UI_SelectLevel:
                    Debug.Log("Open Description of Level :" + (int) messageData);
                    
                    
                    break;

            }
        }



    }
}