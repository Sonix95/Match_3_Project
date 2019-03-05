using System;
using Mathc3Project.Enums;

namespace Mathc3Project.Interfaces.Observer
{
    public interface ISubscriber
    {
        void OnEvent(EventTypes eventType, Object messageData);
    }
}
