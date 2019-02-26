using Mathc3Project.Enums;
using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface ICell
    {
        CellTypes CellTypes { get; set; }
        string Tag { get; }
        GameObject CurrentGameObject { get; set; }
        int TargetX { get; set; }
        int TargetY { get; set; }
        bool IsMatched { get; set; }
        bool IsMoving { get; set; }
        bool IsMovingBack { get; set; }
        bool IsFall { get; set; }
        INotifier Notifier { get; set; }
        void AddSubscriber(ISubscriber subscriber);
    }
}
