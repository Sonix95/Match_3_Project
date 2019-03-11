using System;
using Mathc3Project.Enums;

namespace Mathc3Project.Interfaces
{
    public interface INavigationManager
    {
        IMasterManager MasterManager { get; }

        void Navigate(SceneTypes sceneFrom, SceneTypes sceneTo, Object transferObject);
    }
    
}
