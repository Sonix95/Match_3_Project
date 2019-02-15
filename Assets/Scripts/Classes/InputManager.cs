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

                _notifier.Notify(EventTypeEnum.MouseDown, dataMessage);
                return;
            }

            if (Input.GetMouseButtonUp(0))
            {
                Object dataMessage = new Object();
                dataMessage = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                _notifier.Notify(EventTypeEnum.MouseUp, dataMessage);
                return;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                _notifier.Notify(EventTypeEnum.CellsInfo, null);
                return;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                _notifier.Notify(EventTypeEnum.MoveUp, null);
                return;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                _notifier.Notify(EventTypeEnum.MoveLeft, null);
                return;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                _notifier.Notify(EventTypeEnum.MoveDown, null);
                return;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                _notifier.Notify(EventTypeEnum.MoveRight, null);
                return;
            }
        }

        public void AddSubscriber(ISubscriber subscriber)
        {
            _notifier.AddSubscriber(subscriber);
        }
    }
}
