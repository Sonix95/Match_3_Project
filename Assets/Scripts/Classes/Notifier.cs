using System;
using System.Collections.Generic;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;

namespace Mathc3Project
{
    public class Notifier : INotifier
    {
        #region Fields

        private readonly IList<ISubscriber> _subscribersList;

        #endregion

        #region Constructor

        public Notifier()
        {
            _subscribersList = new List<ISubscriber>();
        }

        #endregion

        #region Methods

        public void AddSubscriber(ISubscriber subscriber)
        {
            if (_subscribersList != null && _subscribersList.Contains(subscriber) == false)
            {
                _subscribersList.Add(subscriber);
            }
        }

        public void RemoveSubscriber(ISubscriber subscriber)
        {
            if (_subscribersList != null && _subscribersList.Contains(subscriber) == true)
            {
                _subscribersList.Remove(subscriber);
            }
        }

        public void Notify(EventTypeEnum eventTypeEnum, Object messageData)
        {
            foreach (var subscriber in _subscribersList)
            {
                subscriber.OnEvent(eventTypeEnum, messageData);
            }
        }

        #endregion

    }
}
