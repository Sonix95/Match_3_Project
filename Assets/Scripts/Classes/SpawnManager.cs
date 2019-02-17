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
            GameObject backTile = _objectStorage.GetObjectByType(GameElementsType.BackTile);

            backTile.transform.parent = parent.transform;
            backTile.transform.position = position + Vector3.forward;
            backTile.name = "PREF_(" + position.x + ", " + position.y + ")";
        }

        public ICell GenerateGameElement(Vector3 position)
        {
            GameObject gameElement = _objectStorage.GetObjectByType(GameElementsType.RandomPrefab);
            gameElement.transform.position = position;

            ICell cell = gameElement.AddComponent<Cell>();
            cell.Name = "PREF_" + cell.Name.Substring(0, cell.Name.Length - 7);
            cell.Notifier = _notifier;
            cell.AddSubscriber(_logicManager);

            return cell;
        }
    }
}
