namespace Mathc3Project.Interfaces
{
    public interface IUpdateManager
    {
        bool IsUpdate { get; set; }
        void PrintAllSub();
        void AddUpdatable(IUpdatable updatable);
        void RemoveUpdatable(IUpdatable updatable);
    }
}
