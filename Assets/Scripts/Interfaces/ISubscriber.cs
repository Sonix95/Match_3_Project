using System;
using Mathc3Project.Enums;

namespace Mathc3Project.Interfaces
{
    public interface ISubscriber
    {
        void OnEvent(EventTypeEnum eventTypeEnum, Object messageData);
    }
}
