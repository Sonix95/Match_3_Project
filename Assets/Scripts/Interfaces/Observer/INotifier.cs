using System;
using Mathc3Project.Enums;

namespace Mathc3Project.Interfaces.Observer
{
    public interface INotifier
    {
        void AddSubscriber(ISubscriber subscriber);
        void RemoveSubscriber(ISubscriber subscriber);
        void Notify(EventTypes eventType, Object messageData);
    }
}
