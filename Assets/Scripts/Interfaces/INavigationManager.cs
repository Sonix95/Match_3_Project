using System;
using Mathc3Project.Enums;

namespace Mathc3Project.Interfaces
{
    public interface INavigationManager
    {
        IMasterManager MasterManager { get; }

        void Navigate(SceneTypesEnum sceneFrom, SceneTypesEnum sceneTo, Object transferObject);
    }
    
}
