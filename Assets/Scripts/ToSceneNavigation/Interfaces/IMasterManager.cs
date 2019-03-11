using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;

namespace DefaultNamespace
{
    public interface IMasterManager
    {
        ICoroutiner Coroutiner { get; }
        IUpdateManager UpdateManager { get; }
        INotifier GameplayNotifier { get; set; }
        INotifier UINotifier { get; set; }
        IObjectStorage ObjectStorage { get; }
        IObjectSetter ObjectSetter { get; }
        ISpawnManager SpawnManager { get; }



    }
}