using System;
using System.Collections.Generic;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces.Observer;

namespace Mathc3Project.Interfaces
{
    public interface ITaskManager : ISubscriber
    {
        INotifier Notifier { get; set; }
        
        void AddSubscriber(ISubscriber subscriber);
        void RemoveSubscriber(ISubscriber subscriber);
        void Notify(EventTypes eventType, Object messageData);
    }
}