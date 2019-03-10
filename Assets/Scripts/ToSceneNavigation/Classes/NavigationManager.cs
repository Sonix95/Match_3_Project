using System.Collections;
using DefaultNamespace;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Enums;
using ToSceneNavigation.Classes.Abstract;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = System.Object;

namespace ToSceneNavigation.Classes
{
    public class NavigationManager : INavigationManager
    {
        private AsyncOperation _loadSceneOperation;

        public NavigationManager(IMasterManager masterManager)
        {
            MasterManager = masterManager;
        }

        public void Navigate(SceneTypes sceneFrom, SceneTypes sceneTo, Object transferObject)
        {
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
            
            IScene sceneBaseObject = GameObject.FindWithTag("Base Scene Object").GetComponent<BaseScene>();

            sceneBaseObject.SetDependencies(loadedScene, this);
            sceneBaseObject.OnEnter(transferObject);
        }

        public IMasterManager MasterManager { get; private set; }
        
    }
}
