using System.Collections.Generic;
using System.Linq;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using UnityEngine;

namespace Mathc3Project.Classes
{
    public class ObjectStorage : IObjectStorage
    {
        private readonly IList<GameObject> _normalCellsPrefabsList;
        private IDictionary<PowerTypes, GameObject> _powersPrefabs;

        public ObjectStorage()
        {
            _powersPrefabs = new Dictionary<PowerTypes, GameObject>();
            _normalCellsPrefabsList = new List<GameObject>();

            Object[] objTempArray = Resources.LoadAll("Prefabs/Gameplay/Elements");
            for (int i = 0; i < objTempArray.Length; i++)
            {
                _normalCellsPrefabsList.Add(objTempArray[i] as GameObject);
            }
            
            _powersPrefabs.Add(PowerTypes.Horizontal, Resources.Load("Prefabs/Gameplay/Powers/Horizontal") as GameObject);
            _powersPrefabs.Add(PowerTypes.Vertical, Resources.Load("Prefabs/Gameplay/Powers/Vertical") as GameObject);
            _powersPrefabs.Add(PowerTypes.Bomb, Resources.Load("Prefabs/Gameplay/Powers/Bomb") as GameObject);
            _powersPrefabs.Add(PowerTypes.ColorBomb, Resources.Load("Prefabs/Gameplay/Powers/ColorBomb") as GameObject);
        }

        public GameObject GetRandomGameElement()
        {
            int cellIndex = Random.Range(0, _normalCellsPrefabsList.Count);
            var prefToUse = _normalCellsPrefabsList.ElementAt(cellIndex);
            return Object.Instantiate(prefToUse);
        }
        
        public GameObject GetPowerElement(PowerTypes powerType)
        {
            GameObject powerGameObject = Object.Instantiate(_powersPrefabs[powerType]);
            return powerGameObject;
        }

    }
}
