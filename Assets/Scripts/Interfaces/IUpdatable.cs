namespace Mathc3Project.Interfaces
{
    public interface IUpdatable
    {
        bool canUpdate { get; set; }

        void DoUpdate();
    }
}
