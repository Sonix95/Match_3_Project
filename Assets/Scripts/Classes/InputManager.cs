using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using UnityEngine;
using Object = System.Object;

namespace Mathc3Project
{
    public class InputManager : MonoBehaviour, IInputManager
    {
        private INotifier _notifier;

        public INotifier Notifier
        {
            get { return _notifier; }
            set { _notifier = value; }
        }
    
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Object o = new Object();
                o = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                _notifier.Notify(EventTypeEnum.MouseDown, o);
                return;
            }
        
            if (Input.GetMouseButtonUp(0))
            {
                Object o = new Object();
                o = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                _notifier.Notify(EventTypeEnum.MouseUp, o);
                return;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                _notifier.Notify(EventTypeEnum.CellsInfo, null);
                return;
            }
        }

        public void AddSubscriber(ISubscriber subscriber)
        {
            _notifier.AddSubscriber(subscriber);
        }
    }
}