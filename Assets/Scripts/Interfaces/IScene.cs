using System;
using Mathc3Project.Enums;

namespace Mathc3Project.Interfaces
{
    public interface IScene
    {
        SceneTypesEnum SceneTypeEnum { get; }
        
        void SetDependencies(SceneTypesEnum sceneTypeEnum, INavigationManager navigationManager);
        void OnExit();
        void OnEnter(Object transferObject);
    }
}