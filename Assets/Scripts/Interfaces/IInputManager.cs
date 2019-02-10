namespace Mathc3Project.Interfaces
{
    public interface IInputManager
    {
        INotifier Notifier { get; set; }
        void AddSubscriber(ISubscriber subscriber);
    }
}
