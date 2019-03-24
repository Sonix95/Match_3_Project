using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using UnityEngine;
using Object = System.Object;

namespace Mathc3Project.Classes.Abstract
{
    public abstract class BaseScene : MonoBehaviour, IScene
    {
        protected INavigationManager _NavigationManager;
        
        public void SetDependencies(SceneTypesEnum sceneTypeEnum, INavigationManager navigationManager)
        {
            _NavigationManager = navigationManager;
            SceneTypeEnum = sceneTypeEnum;
        }

        public abstract void OnExit();

        public abstract void OnEnter(Object transferObject);
                
        public SceneTypesEnum SceneTypeEnum { get; private set; }
    }
}