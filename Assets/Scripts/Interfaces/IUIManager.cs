using DefaultNamespace;
using Mathc3Project.Interfaces.Observer;

namespace Mathc3Project.Interfaces
{
    public interface IUIManager : ISubscriber
    {
        INavigationManager NavigationManager { get; set; }
    }
}