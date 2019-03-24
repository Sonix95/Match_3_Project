using System.Collections;
using Mathc3Project.Classes.Abstract;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = System.Object;

namespace Mathc3Project.Classes
{
    public class NavigationManager : INavigationManager
    {
        private AsyncOperation _loadSceneOperation;
        private IScene _currentScene;
        public NavigationManager(IMasterManager masterManager)
        {
            MasterManager = masterManager;
        }

        public void Navigate(SceneTypesEnum sceneFrom, SceneTypesEnum sceneTo, Object transferObject)
        {
            _currentScene = GameObject.FindWithTag(Strings.BASE_SCENE_OBJECT).GetComponent<BaseScene>();
            
            var loadedSceneName = Strings.GetScenePath(sceneTo);
            
            _loadSceneOperation = SceneManager.LoadSceneAsync(loadedSceneName);
            
            MasterManager.Coroutiner.StartCoroutine(SceneLoading(sceneTo, transferObject));
        }

        private IEnumerator SceneLoading(SceneTypesEnum loadedScene, Object transferObject)
        {
            
            
            var isLoaded = _loadSceneOperation.isDone;

            while (isLoaded == false)
            {
                yield return null;
                isLoaded = _loadSceneOperation.isDone;
            }
            
           
            yield return null; _currentScene.OnExit();
            
            _currentScene = GameObject.FindWithTag(Strings.BASE_SCENE_OBJECT).GetComponent<BaseScene>();
            _currentScene.SetDependencies(loadedScene, this);
            _currentScene.OnEnter(transferObject);
        }

        public IMasterManager MasterManager { get; private set; }
        
    }
}
