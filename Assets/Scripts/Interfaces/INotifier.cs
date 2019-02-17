using System;
using Mathc3Project.Enums;

namespace Mathc3Project.Interfaces
{
    public interface INotifier
    {
        void AddSubscriber(ISubscriber subscriber);
        void RemoveSubscriber(ISubscriber subscriber);
        void Notify(EventTypeEnum eventTypeEnum, Object messageData);
    }
}
