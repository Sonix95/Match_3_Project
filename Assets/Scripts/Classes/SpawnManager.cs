using System.Collections.Generic;
using Mathc3Project.Classes.Cells;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Cells;
using UnityEngine;

namespace Mathc3Project.Classes
{
    public class SpawnManager : ISpawnManager
    {
        private readonly IObjectStorage _objectStorage;

        public SpawnManager(IObjectStorage objectStorage)
        {
            _objectStorage = objectStorage;
        }

        public GameObject SpawnPrefab(Vector3 position)
        {
            GameObject newGameObject = _objectStorage.GetRandomGameElement();
            
            newGameObject.name = newGameObject.tag;
            newGameObject.transform.position = position;
            
            return newGameObject;
        }

        public GameObject SpawnPowerPrefab(PowerUpTypes powerUpType, Vector3 position)
        {
            GameObject powerGameObject = _objectStorage.GetPowerElement(powerUpType);
            
            powerGameObject.name = powerGameObject.tag;
            powerGameObject.transform.position = position;
            
            return powerGameObject;
        }
        
        public ICell SpawnNormalCell(Vector3 position)
        {
            GameObject newGameObject = SpawnPrefab(position);
     
            ICell newNormalCell = new NormalCell((int) position.x, (int) position.y);

            newNormalCell.CurrentGameObject = newGameObject;
            
            return newNormalCell;
        }
    
    }
}
