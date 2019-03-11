using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using UnityEngine;
using Object = System.Object;

namespace Mathc3Project.Classes.Abstract
{
    public abstract class BaseScene : MonoBehaviour, IScene
    {
        protected INavigationManager _NavigationManager;
        
        public void SetDependencies(SceneTypes sceneType, INavigationManager navigationManager)
        {
            _NavigationManager = navigationManager;
            SceneType = sceneType;
        }

        public abstract void OnExit();

        public abstract void OnEnter(Object transferObject);
        
        public SceneTypes SceneType { get; private set; }
    }
}