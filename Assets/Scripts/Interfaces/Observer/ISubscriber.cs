using System;
using Mathc3Project.Enums;

namespace Mathc3Project.Interfaces.Observer
{
    public interface ISubscriber
    {
        void OnEvent(EventTypesEnum eventTypeEnum, Object messageData);
    }
}
