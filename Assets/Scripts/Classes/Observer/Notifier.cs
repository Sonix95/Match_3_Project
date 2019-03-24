using System;
using System.Collections.Generic;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces.Observer;

namespace Mathc3Project.Classes.Observer
{
    public class Notifier : INotifier
    {
        private readonly IList<ISubscriber> _subscribersList;

        public Notifier()
        {
            _subscribersList = new List<ISubscriber>();
        }

        public void AddSubscriber(ISubscriber subscriber)
        {
            if (_subscribersList != null && _subscribersList.Contains(subscriber) == false)
            {
                _subscribersList.Add(subscriber);
            }
        }

        public void RemoveSubscriber(ISubscriber subscriber)
        {
            if (_subscribersList != null && _subscribersList.Contains(subscriber))
            {
                _subscribersList.Remove(subscriber);
            }
        }

        public void Notify(EventTypesEnum eventTypeEnumEnum, Object messageData)
        {
            foreach (var subscriber in _subscribersList)
            {
                subscriber.OnEvent(eventTypeEnumEnum, messageData);
            }
        }
        
    }
}
