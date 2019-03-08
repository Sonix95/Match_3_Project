using System;
using System.Collections.Generic;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;

namespace Mathc3Project.Classes
{
    public class TaskManager: ITaskManager
    {
        private IDictionary<string, int> _task;

        private IList<ISubscriber> _subscribes;
        private INotifier _notifier;

        //TODO добавить в параметры словарь задач
        public TaskManager(INotifier notifier)
        {
            _task = new Dictionary<string, int>
            {
                {"Red", 1},
                {"Blue", 1}
            };

            _notifier = notifier;
            _subscribes = new List<ISubscriber>();
        }

        public void OnEvent(EventTypes eventType, object messageData)
        {
            switch (eventType)
            {
                case EventTypes.CELL_Destroy:
                    string destroyedCellTag = (string) messageData;

                    if (_task.ContainsKey(destroyedCellTag))
                        foreach (var tag in _task.Keys)
                            if (tag == destroyedCellTag)
                            {
                                if (_task[tag] > 0)
                                    _task[tag] -= 1;
                                else
                                    _task[tag] = 0;
                                break;
                            }

                    IList<string> complitedTask = new List<string>();
                    foreach (var val in _task)
                        if (val.Value == 0)
                            complitedTask.Add(val.Key);

                    if (complitedTask.Count > 0)
                        OnEvent(EventTypes.TASK_ComplitedTask, complitedTask);
                    break;

                case EventTypes.TASK_ComplitedTask:

                    IList<string> tasks = (IList<string>) messageData;

                    foreach (var taskKey in tasks)
                        _task.Remove(taskKey);

                    if (_task.Count == 0)
                        Notify(EventTypes.TASK_Finished, this._task);
                    break;

                default:
                    break;
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

        public INotifier Notifier
        {
            get { return _notifier; }
            set { _notifier = value; }
        }
        
    }
}