using Mathc3Project.Interfaces.Observer;

namespace Mathc3Project.Interfaces
{
    public interface ILogicManager : ISubscriber
    {
        IBoard Board { get; set; }
        ICheckManager CheckManager { get; set; }
        ISpawnManager SpawnManager { get; set; }
    }
}
