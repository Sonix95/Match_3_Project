using Mathc3Project.Interfaces.Observer;

namespace Mathc3Project.Interfaces
{
    public interface IMasterManager
    {
        ICoroutiner Coroutiner { get; }
        INotifier GameplayNotifier { get; set; }
        INotifier UINotifier { get; set; }
        IObjectStorage ObjectStorage { get; }
        ISpawnManager SpawnManager { get; }



    }
}