namespace Mathc3Project.Interfaces
{
    public interface ILogicManager : ISubscriber
    {
        IBoard Board { get; set; }
    }
}