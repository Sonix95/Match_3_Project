using System.Collections.Generic;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Mathc3Project.Classes
{
    public class UIManager : IUIManager
    {
        private readonly ILevelsManager _levelsManager;
        private int _chosenLevel;

        private GameObject _levelPanel;
        private GameObject _levelDescriptionGameObject;

        public UIManager(ILevelsManager levelsManager)
        {
            _levelsManager = levelsManager;

            _levelPanel = GameObject.Find("Panel Levels");
            _levelDescriptionGameObject = GameObject.FindWithTag("LevelDescription");

            OnEvent(EventTypes.UI_FirstLoad, null);
        }

        public void OnEvent(EventTypes eventType, object messageData)
        {
            switch (eventType)
            {
                case EventTypes.UI_FirstLoad:
                    _levelPanel.SetActive(false);
                    _levelDescriptionGameObject.SetActive(false);
                    break;

                case EventTypes.UI_OpenLevelsMapButton:
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

                case EventTypes.UI_OpenLevelDescription:
                    _levelDescriptionGameObject.SetActive(true);
                    
                    _chosenLevel = (int) messageData;
                    
                    ILevel level = _levelsManager.Levels[_chosenLevel];
                    
                    GameObject tasksHolderPanel = GameObject.Find("Panel Tasks Holder");
                    
                    Text currentGameObjectText = GameObject.Find("Text Current Level").GetComponent<Text>();
                    currentGameObjectText.text = "Level " + level.LevelId.ToString();

                    int tastsCount = level.LevelTasks.Length;
                    
                    for (int i = 0; i < tastsCount; i++)
                    {
                        GameObject taskPrefab =
                            Object.Instantiate(Resources.Load("Prefabs/UI/Panel Task") as GameObject);

                        taskPrefab.transform.SetParent(tasksHolderPanel.transform);
                        
                        Vector2 tempPos = Helper.SetUITaskPosition(tastsCount, i);
                        taskPrefab.transform.localPosition = tempPos;
                        
                        Text countTask = taskPrefab.GetComponentInChildren<Text>();
                        countTask.text = level.LevelTasks[i].Count.ToString();

                        Image taskSprite = taskPrefab.GetComponentsInChildren<Image>()[1];
                        taskSprite.sprite = level.LevelTasks[i].SpriteElement;
                    }

                    break;

                case EventTypes.UI_CloseLevelDescription:
                    
                    GameObject[] levelTaskPanels = GameObject.FindGameObjectsWithTag("Level Task");

                    foreach (var levelTaskPanel in levelTaskPanels)
                        GameObject.Destroy(levelTaskPanel);

                    _chosenLevel = 0;
                    _levelDescriptionGameObject.SetActive(false);
                    Debug.Log("Close Description of Level");
                    break;
                
                case EventTypes.UI_SelectLevel:
                    Debug.Log(_levelsManager.Levels[_chosenLevel].LevelTasks.Length);
                    SceneManager.LoadScene("Resources/Scenes/Gameplay Level");
                    break;
            }
        }



    }
}