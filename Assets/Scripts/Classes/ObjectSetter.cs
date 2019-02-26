using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using UnityEngine;

namespace Mathc3Project
{
    public class ObjectSetter : IObjectSetter
    {
        private ILogicManager _logicManager;
        private INotifier _notifier;

        public ObjectSetter(ILogicManager logicManager, INotifier notifier)
        {
            _logicManager = logicManager;
            _notifier = notifier;
        }

        public ICell SetGameplayObject(CellTypes type, GameObject go)
        {
            ICell cell = go.AddComponent<Cell>();

            switch (type)
            {
                case CellTypes.Normal:
                    cell.CurrentGameObject = go;
                    cell.CellTypes = CellTypes.Normal;
                    cell.Notifier = _notifier;
                    cell.AddSubscriber(_logicManager);
                    break;
                case CellTypes.Hollow:
                    cell.CellTypes = CellTypes.Hollow;
                    break;
                case CellTypes.Breakable:
                    cell.CellTypes = CellTypes.Breakable;
                    break;
            }

            return cell;
        }

        public void SetNonGameplayObject(GameObject go, GameObject parent, Vector3 position)
        {
            go.transform.parent = parent.transform;
            go.transform.position = position + Vector3.forward;
            go.name = "(" + position.x + ", " + position.y + ")";
        }
    }
}
