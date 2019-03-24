using System.Collections.Generic;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

namespace Mathc3Project.Classes
{
    public class UIGameplayLevelManager : IUIManager
    {
        private INavigationManager _navigationManager;

        private ILevel _level;

        private GameObject _loadingImage;
        private GameObject _tasksHolderPanel;
        private GameObject _levelCompletePanel;

        private IDictionary<string, GameObject> _tasksPanel;
        private IDictionary<string, int> _tasksMaxCount;
        private IDictionary<string, int> _task;

        public UIGameplayLevelManager(ILevel level)
        {
            _level = level;
            _tasksPanel = new Dictionary<string, GameObject>();
            _tasksMaxCount = new Dictionary<string, int>();
            _task = new Dictionary<string, int>();

            FindUI();
            SetupTasksPanel();
        }

        private void FindUI()
        {
            _loadingImage = GameObject.FindWithTag("Image Loading");
            _tasksHolderPanel = GameObject.Find("Panel Tasks Holder");
            _levelCompletePanel = GameObject.Find("Panel Level Complite");
        }

        private void SetupTasksPanel()
        {
            int tastsCount = _level.LevelTasks.Length;

            for (int i = 0; i < tastsCount; i++)
            {
                GameObject taskPrefab =
                    UnityEngine.Object.Instantiate(
                        Resources.Load("Prefabs/UI/Panel Level Task") as GameObject);

                taskPrefab.transform.SetParent(_tasksHolderPanel.transform);

                Vector2 tempPos = Helper.SetUITaskPosition(tastsCount, i, SceneTypesEnum.GameplayLevel);
                taskPrefab.transform.localPosition = tempPos;
                taskPrefab.transform.localScale *= 0.7f;

                Text countTask = taskPrefab.GetComponentInChildren<Text>();
                countTask.text = 0.ToString() + " / " + _level.LevelTasks[i].Count.ToString();

                Image taskSprite = taskPrefab.GetComponentsInChildren<Image>()[2];
                taskSprite.sprite = _level.LevelTasks[i].SpriteElement;

                _tasksPanel.Add(_level.LevelTasks[i].ElementName, taskPrefab);
                _task.Add(_level.LevelTasks[i].ElementName, i);
                _tasksMaxCount.Add(_level.LevelTasks[i].ElementName, _level.LevelTasks[i].Count);
            }
        }

        public void OnEvent(EventTypesEnum eventTypeEnum, Object messageData)
        {
            switch (eventTypeEnum)
            {
                case EventTypesEnum.UI_SceneLoaded:
                    _levelCompletePanel.SetActive(false);
                    _loadingImage.SetActive(false);
                    break;

                case EventTypesEnum.UI_OpenMenu:
                    _loadingImage.SetActive(true);
                    _navigationManager.Navigate(SceneTypesEnum.GameplayLevel, SceneTypesEnum.Menu, null);
                    break;

                case EventTypesEnum.UI_PlayLevel:
                    _loadingImage.SetActive(true);
                    
                    for (int i = 0; i < _level.LevelTasks.Length; i++)
                        _level.LevelTasks[i].Count = _tasksMaxCount[_level.LevelTasks[i].ElementName];
                    
                    _navigationManager.Navigate(SceneTypesEnum.GameplayLevel, SceneTypesEnum.GameplayLevel, _level);
                    break;

                case EventTypesEnum.CELL_Destroy:
                    string elementName = (string) messageData;
                    if (_tasksPanel.ContainsKey(elementName))
                    {
                        var taskCount = _task[elementName];
                        var maxCount = _tasksMaxCount[elementName];

                        GameObject taskPanel = _tasksPanel[elementName];

                        Text countTask = taskPanel.GetComponentInChildren<Text>();
                        countTask.text = (maxCount - _level.LevelTasks[taskCount].Count).ToString() + " / " +
                                         maxCount.ToString();
                    }

                    break;

                case EventTypesEnum.TASK_Finished:
                    _levelCompletePanel.SetActive(true);
                    
                    for (int i = 0; i < _level.LevelTasks.Length; i++)
                        _level.LevelTasks[i].Count = _tasksMaxCount[_level.LevelTasks[i].ElementName];
                    
                    Debug.Log("FIHISHED");
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