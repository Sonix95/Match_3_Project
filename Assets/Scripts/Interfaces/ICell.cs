using Mathc3Project.Enums;
using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface ICell
    { 
        INotifier Notifier { get; set; }
        string Tag { get; }
        string Name { get; set; }
        GameObject CurrentGameObject{ get; set; }
        int TargetX { get; set; }
        int TargetY { get; set; }
        bool IsMoving { get; set; }
        bool IsMovingBack { get; set; }
        bool IsFall { get; set; }
        bool IsMatched{ get; set; }
       
        void AddSubscriber(ISubscriber subscriber);
    }
}
