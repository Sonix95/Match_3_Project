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

        public void Navigate(SceneTypes sceneFrom, SceneTypes sceneTo, Object transferObject)
        {
            _currentScene = GameObject.FindWithTag("Base Scene Object").GetComponent<BaseScene>();
            
            var loadedSceneName = Strings.GetScenePath(sceneTo);
            
            _loadSceneOperation = SceneManager.LoadSceneAsync(loadedSceneName);
            
            MasterManager.Coroutiner.StartCoroutine(SceneLoading(sceneTo, transferObject));
        }

        private IEnumerator SceneLoading(SceneTypes loadedScene, Object transferObject)
        {
            
            
            var isLoaded = _loadSceneOperation.isDone;

            while (isLoaded == false)
            {
                yield return null;
                isLoaded = _loadSceneOperation.isDone;
            }
            
            _currentScene.OnExit();
            yield return null;
            
            _currentScene = GameObject.FindWithTag("Base Scene Object").GetComponent<BaseScene>();
            _currentScene.SetDependencies(loadedScene, this);
            _currentScene.OnEnter(transferObject);
        }

        public IMasterManager MasterManager { get; private set; }
        
    }
}
