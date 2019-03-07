using System.Collections.Generic;
using System.Linq;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using UnityEngine;

namespace Mathc3Project.Classes
{
    public class ObjectStorage : IObjectStorage
    {
        private readonly IList<GameObject> _normalCellsPrefabsList;
        private readonly IDictionary<PowerUpTypes, GameObject> _powersPrefabs;

        public ObjectStorage()
        {
            _powersPrefabs = new Dictionary<PowerUpTypes, GameObject>();
            _normalCellsPrefabsList = new List<GameObject>();

            Object[] objTempArray = Resources.LoadAll(MagicStrings.Gameplay_Elements);
            for (int i = 0; i < objTempArray.Length; i++)
            {
                _normalCellsPrefabsList.Add(objTempArray[i] as GameObject);
            }

            _powersPrefabs.Add(PowerUpTypes.Horizontal, Resources.Load(MagicStrings.Power_Horizontal) as GameObject);
            _powersPrefabs.Add(PowerUpTypes.Vertical, Resources.Load(MagicStrings.Power_Vertical) as GameObject);
            _powersPrefabs.Add(PowerUpTypes.Bomb, Resources.Load(MagicStrings.Power_Bomb) as GameObject);
            _powersPrefabs.Add(PowerUpTypes.ColorBomb, Resources.Load(MagicStrings.Power_ColorBomb) as GameObject);
        }

        public GameObject GetRandomGameElement()
        {
            int cellIndex = Random.Range(0, _normalCellsPrefabsList.Count);
            var prefToUse = _normalCellsPrefabsList.ElementAt(cellIndex);
            return Object.Instantiate(prefToUse);
        }

        public GameObject GetPowerElement(PowerUpTypes powerUpType)
        {
            GameObject powerGameObject = Object.Instantiate(_powersPrefabs[powerUpType]);
            return powerGameObject;
        }

    }
}
