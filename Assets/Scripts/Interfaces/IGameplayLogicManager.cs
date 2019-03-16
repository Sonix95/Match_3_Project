using System;
using System.Collections;
using System.Collections.Generic;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces.Cells;
using Mathc3Project.Interfaces.Command;
using Mathc3Project.Interfaces.Observer;

namespace Mathc3Project.Interfaces
{
    public interface IGameplayLogicManager : ISubscriber
    {
        IBoard Board { get; set; }
        INavigationManager NavigationManager { get; set; }
        ICheckManager CheckManager { get; set; }
        ISpawnManager SpawnManager { get; set; }

        void TryCheckSwipedCells(ICell cell, int swipeCount = -1);
        ICommand MacroCommand{get;set;}
        INotifier Notifier { get; set; }
        void AddSubscriber(ISubscriber subscriber);
        void RemoveSubscriber(ISubscriber subscriber);
        void Notify(EventTypes eventType, Object messageData);
    }
}
