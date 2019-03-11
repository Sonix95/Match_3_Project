using DefaultNamespace;
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
        
        public void OnEvent(EventTypes eventType, object messageData)
        {
            switch (eventType)
            {
                case EventTypes.UI_SceneLoaded:
                    if (_loadingImage == null)
                        throw new UnityException("Loading image is " + _loadingImage);
                    else
                        _loadingImage.SetActive(false);
                    break;
                
                case EventTypes.UI_Start:
                    if (_navigationManager == null)
                        throw new UnityException("Navigation Manager is" + _navigationManager);
                    else
                        _navigationManager.Navigate(SceneTypes.StartScreenScene, SceneTypes.Menu, null);
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
