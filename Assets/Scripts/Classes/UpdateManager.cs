using System.Collections.Generic;
using Mathc3Project.Interfaces;
using UnityEngine;

namespace Mathc3Project.Classes
{
    public class UpdateManager : MonoBehaviour, IUpdateManager
    {
        IList<IUpdatable> _updatableList = new List<IUpdatable>();
        private bool isUpdate;

        private void Update()
        {
            if (isUpdate)
            {
                foreach (var updatable in _updatableList)
                {
                    if (updatable.canUpdate)
                        updatable.DoUpdate();
                }
            }
        }

        public void AddUpdatable(IUpdatable updatable)
        {
            if (updatable != null)
            {
                if (_updatableList.Contains(updatable) == false)
                    _updatableList.Add(updatable);
            }
        }

        public void RemoveUpdatable(IUpdatable updatable)
        {
            if (updatable != null)
            {
                if (_updatableList.Contains(updatable))
                    _updatableList.Remove(updatable);
            }
        }

        public bool IsUpdate
        {
            get { return isUpdate; }
            set { isUpdate = value; }
        }

    }
}
