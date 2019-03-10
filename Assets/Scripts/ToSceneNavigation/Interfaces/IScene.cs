using System;
using Mathc3Project.Enums;

namespace DefaultNamespace
{
    public interface IScene
    {
        SceneTypes SceneType { get; }
        
        void SetDependencies(SceneTypes sceneType, INavigationManager navigationManager);
        void OnExit();
        void OnEnter(Object transferObject);
    }
}