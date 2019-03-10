

using System;
using Mathc3Project.Enums;

namespace DefaultNamespace
{
    public interface INavigationManager
    {
        IMasterManager MasterManager { get; }

        void Navigate(SceneTypes sceneFrom, SceneTypes sceneTo, Object transferObject);
    }
    
}
