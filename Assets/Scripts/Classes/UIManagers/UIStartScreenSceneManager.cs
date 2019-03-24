using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Mathc3Project.Classes
{
    public class UIStartScreenSceneManager : IUIManager
    {
        private INavigationManager _navigationManager;
        private GameObject _loadingImage;
        public UIStartScreenSceneManager()
        {
            _loadingImage = GameObject.FindWithTag("Image Loading");
        }
        
        public void OnEvent(EventTypesEnum eventTypeEnum, object messageData)
        {
            switch (eventTypeEnum)
            {
                case EventTypesEnum.UI_SceneLoaded:
                    if (_loadingImage == null)
                        throw new UnityException("Loading image is " + _loadingImage);
                    else
                        _loadingImage.SetActive(false);
                    break;
                
                case EventTypesEnum.UI_OpenMenu:
                    if (_navigationManager == null)
                        throw new UnityException("Navigation Manager is" + _navigationManager);
                    else
                    {
                        _loadingImage.SetActive(true);
                        _navigationManager.Navigate(SceneTypesEnum.StartScreenScene, SceneTypesEnum.Menu, null);
                    }
                    break;
            }
        }

        public INavigationManager NavigationManager
        {
            get { return _navigationManager; }
            set { _navigationManager = value; }
        }

    }
}
