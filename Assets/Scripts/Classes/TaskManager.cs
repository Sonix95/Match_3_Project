using System;
using System.Collections.Generic;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;

namespace Mathc3Project.Classes
{
    public class TaskManager : ITaskManager
    {
        private readonly ILevelTask[] _levelTasks;

        private IList<ISubscriber> _subscribes;
        private INotifier _notifier;

        public TaskManager(ILevelTask[] levelTask)
        {
            _subscribes = new List<ISubscriber>();
            _levelTasks = levelTask;
        }

        public void OnEvent(EventTypesEnum eventTypeEnum, object messageData)
        {
            switch (eventTypeEnum)
            {
                case EventTypesEnum.CELL_Destroy:
                    string destroyedCellTag = (string) messageData;

                    foreach (var levelTask in _levelTasks)
                    {
                        if (levelTask.ElementName == destroyedCellTag)
                        {
                            levelTask.Count -= 1;
                            if (levelTask.Count == 0)
                                OnEvent(EventTypesEnum.TASK_ComplitedTask, null);
                            return;
                        }
                    }

                    break;

                case EventTypesEnum.TASK_ComplitedTask:
                    
                    foreach (var levelTask in _levelTasks)
                    {
                        if (levelTask.Completed == false)
                            return;
                    }
                    
                    Notify(EventTypesEnum.TASK_Finished, this);
                    break;

                default:
                    break;
            }
        }

        #region Observer implimentation

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

        public void Notify(EventTypesEnum eventTypeEnum, Object messageData)
        {
            _notifier.Notify(eventTypeEnum, messageData);
        }

        public INotifier Notifier
        {
            get { return _notifier; }
            set { _notifier = value; }
        }

        #endregion
        
    }
}
