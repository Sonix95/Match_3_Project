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
        
        public SpawnManager(IObjectStorage objectStorage)
        {
            _objectStorage = objectStorage;
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

            return cell;
        }
    }
}