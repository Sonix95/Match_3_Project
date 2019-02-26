using Object = System.Object;
using UnityEngine;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;

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
                Object dataMessage = new Object();
                dataMessage = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                _notifier.Notify(EventTypeEnum.MOUSE_down, dataMessage);
                return;
            }

            if (Input.GetMouseButtonUp(0))
            {
                Object dataMessage = new Object();
                dataMessage = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                _notifier.Notify(EventTypeEnum.MOUSE_up, dataMessage);
                return;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                _notifier.Notify(EventTypeEnum.UTILITY_boardCellsInfo, null);
                return;
            }
        }

        public void AddSubscriber(ISubscriber subscriber)
        {
            _notifier.AddSubscriber(subscriber);
        }
    }
}
