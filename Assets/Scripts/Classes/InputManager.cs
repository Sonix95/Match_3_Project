using System.Collections.Generic;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;
using UnityEngine;
using Object = System.Object;

namespace Mathc3Project.Classes
{
    public class InputManager : IInputManager, IUpdatable
    {
        private bool _canUpdate;
        private INotifier _notifier;

        private IList<ISubscriber> _subscribes;

        public InputManager(INotifier notifier)
        {
            _notifier = notifier;
            _subscribes = new List<ISubscriber>();
            _canUpdate = true;
        }

        public void CustomUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Notify(EventTypes.LMB_Down, null);
                return;
            }

            if (Input.GetMouseButtonUp(0))
            {
                Notify(EventTypes.LMB_Up, null);
                return;
            }
        }

        public void AddSubscriber(ISubscriber subscriber)
        {
            if (subscriber != null && !_subscribes.Contains(subscriber))
                _notifier.AddSubscriber(subscriber);
        }

        public void RemoveSubscriber(ISubscriber subscriber)
        {
            if (subscriber != null && _subscribes.Contains(subscriber))
                _notifier.RemoveSubscriber(subscriber);
        }

        public void Notify(EventTypes eventType, Object messageData)
        {
            _notifier.Notify(eventType, messageData);
        }

        public bool canUpdate
        {
            get { return _canUpdate; }
            set { _canUpdate = value; }
        }

        public INotifier Notifier
        {
            get { return _notifier; }
            set { _notifier = value; }
        }

    }
}
