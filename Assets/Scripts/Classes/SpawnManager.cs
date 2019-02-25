using System.Collections.Generic;
using System.Linq;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using UnityEngine;

namespace Mathc3Project
{
    public class SpawnManager : ISpawnManager
    {
        private IObjectStorage _objectStorage;
        private ILogicManager _logicManager;
        private INotifier _notifier;

        public SpawnManager(IObjectStorage objectStorage, ILogicManager logicManager, INotifier notifier)
        {
            _objectStorage = objectStorage;
            _logicManager = logicManager;
            _notifier = notifier;
        }

        public void GenerateBackTile(Vector3 position, GameObject parent)
        {
            GameObject backTile = _objectStorage.GetBackTile();

            backTile.transform.parent = parent.transform;
            backTile.transform.position = position + Vector3.forward;
            backTile.name = "(" + position.x + ", " + position.y + ")";
        }

        public ICell GenerateHollowCell(Vector3 position)
        {
            GameObject hollowGO = _objectStorage.GetHollowCell();
            hollowGO.transform.position = position + Vector3.back;

            ICell hollowCell = hollowGO.AddComponent<Cell>();
            hollowCell.Name = "Hollow";
            hollowCell.CurrentGameObject = null;

            return hollowCell;
        }

        public ICell GenerateRandomGameElement(Vector3 position)
        {
            GameObject randomGO = _objectStorage.GetRandomGameElement();
            randomGO.transform.position = position;

            ICell randomCell = randomGO.AddComponent<Cell>();
            randomCell.Name = "Random";
            randomCell.CurrentGameObject = randomGO;
            randomCell.Notifier = _notifier;
            randomCell.AddSubscriber(_logicManager);

            return randomCell;
        }
    }
}
