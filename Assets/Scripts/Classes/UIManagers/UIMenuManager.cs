using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

namespace Mathc3Project.Classes
{
    public class UIMenuManager : IUIManager
    {
        private INavigationManager _navigationManager;
        private ILevelsManager _levelsManager;
        private int _selectedLevel;

        private GameObject _loadingImage;
        private GameObject _gameObjectDescription_PanelLevels;

        public UIMenuManager(ILevelsManager levelsManager)
        {
            _levelsManager = levelsManager;
            FindUI();
        }
        
        private void FindUI()
        {
            _loadingImage = GameObject.FindWithTag("Image Loading");
            _gameObjectDescription_PanelLevels = GameObject.Find("GameObject Description");
        }

        public void OnEvent(EventTypesEnum eventTypeEnum, Object messageData)
        {
            switch (eventTypeEnum)
            {
                case EventTypesEnum.UI_SceneLoaded:
                    _gameObjectDescription_PanelLevels.SetActive(false);
                    _loadingImage.SetActive(false);
                    break;

                case EventTypesEnum.UI_BackToStartScreen:
                    if (_navigationManager == null)
                        throw new UnityException("Navigation Manager is" + _navigationManager);
                    else
                        _navigationManager.Navigate(SceneTypesEnum.Menu, SceneTypesEnum.StartScreenScene, null);
                    break;

                case EventTypesEnum.UI_PrevLocation:
                    Debug.Log("go to Previous location");
                    break;

                case EventTypesEnum.UI_NextLocation:
                    Debug.Log("go to Next location");
                    break;

                case EventTypesEnum.UI_OpenLevelDescription:
                    _selectedLevel = (int) messageData;

                    if (_selectedLevel > _levelsManager.Levels.Length - 1)
                    {
                        Debug.Log("Level not created");
                        return;
                    }
                    else
                    {
                        ILevel level = _levelsManager.Levels[_selectedLevel];

                        _gameObjectDescription_PanelLevels.SetActive(true);

                        GameObject tasksHolderPanel = GameObject.Find("Panel Tasks Holder");

                        Text currentGameObjectText = GameObject.Find("Text Selected Level").GetComponent<Text>();
                        currentGameObjectText.text = "Level " + level.LevelId.ToString();

                        int tastsCount = level.LevelTasks.Length;

                        for (int i = 0; i < tastsCount; i++)
                        {
                            GameObject taskPrefab =
                                UnityEngine.Object.Instantiate(
                                    Resources.Load("Prefabs/UI/Panel Level Task") as GameObject);

                            taskPrefab.transform.SetParent(tasksHolderPanel.transform);

                            Vector2 tempPos = Helper.SetUITaskPosition(tastsCount, i, SceneTypesEnum.Menu);
                            taskPrefab.transform.localPosition = tempPos;

                            Text countTask = taskPrefab.GetComponentInChildren<Text>();
                            countTask.text = level.LevelTasks[i].Count.ToString();

                            Image taskSprite = taskPrefab.GetComponentsInChildren<Image>()[2];
                            taskSprite.sprite = level.LevelTasks[i].SpriteElement;
                        }
                    }

                    break;

                case EventTypesEnum.UI_CLoseLevelDescription:
                    GameObject[] levelTaskPanels = GameObject.FindGameObjectsWithTag("Level Task");

                    foreach (var levelTaskPanel in levelTaskPanels)
                        GameObject.Destroy(levelTaskPanel);

                    _selectedLevel = -1;
                    _gameObjectDescription_PanelLevels.SetActive(false);
                    Debug.Log("Close Description of Level");
                    break;
                
                case EventTypesEnum.UI_PlayLevel:
                    ILevel selectedLevel = _levelsManager.Levels[_selectedLevel];
                    
                    _loadingImage.SetActive(true);
                    _navigationManager.Navigate(SceneTypesEnum.Menu, SceneTypesEnum.GameplayLevel, selectedLevel);
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
