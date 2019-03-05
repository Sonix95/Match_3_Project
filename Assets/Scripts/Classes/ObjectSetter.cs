using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Cells;
using Mathc3Project.Interfaces.Observer;
using UnityEngine;

namespace Mathc3Project.Classes
{
    public class ObjectSetter : IObjectSetter
    {
        private IUpdateManager _updateManager;
        private INotifier _notifier;

        public ObjectSetter(IUpdateManager updateManager,INotifier notifier)
        {
            _updateManager = updateManager;
            _notifier = notifier;
        }

        public void SetGameObject(GameObject go, Vector3 position)
        {
            go.name = "CELL[" + position.x + "x" + position.y + "]:" + go.tag;
            go.transform.position = position;
        }

        public void SetNormalCell(INormalCell normalCell,GameObject go)
        {
            normalCell.CurrentGameObject = go;
            normalCell.Notifier = _notifier;
            _updateManager.AddUpdatable(normalCell);
        }
    }
}
