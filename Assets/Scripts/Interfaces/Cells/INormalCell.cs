using Mathc3Project.Interfaces.Observer;

namespace Mathc3Project.Interfaces.Cells
{
    public interface INormalCell : ICell, IUpdatable
    {
        INotifier Notifier { get; set; }
    }
}
