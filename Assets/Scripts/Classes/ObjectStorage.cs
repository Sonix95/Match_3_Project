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

            SetUp();
        }

        private void SetUp()
        {
            // Gameplay Elemnts ===============================
            _normalCellsPrefabsList.Add(
                Resources.Load(Strings.Gameplay_Elements + Strings.Tag_OrangeBox) as GameObject);
            _normalCellsPrefabsList.Add(
                Resources.Load(Strings.Gameplay_Elements + Strings.Tag_RedCircle) as GameObject);
            _normalCellsPrefabsList.Add(
                Resources.Load(Strings.Gameplay_Elements + Strings.Tag_BlueMultiAngle) as GameObject);
            _normalCellsPrefabsList.Add(
                Resources.Load(Strings.Gameplay_Elements + Strings.Tag_YellowUpTriangle) as GameObject);
            _normalCellsPrefabsList.Add(
                Resources.Load(Strings.Gameplay_Elements + Strings.Tag_GreenDownTriangle) as GameObject);

            // Gameplay Powers ===============================
            _powersPrefabs.Add(PowerUpTypes.Horizontal,
                Resources.Load(Strings.Power_Element + Strings.Tag_Horizontal) as GameObject);
            _powersPrefabs.Add(PowerUpTypes.Vertical,
                Resources.Load(Strings.Power_Element + Strings.Tag_Vertical) as GameObject);
            _powersPrefabs.Add(PowerUpTypes.Bomb,
                Resources.Load(Strings.Power_Element + Strings.Tag_Bomb) as GameObject);
            _powersPrefabs.Add(PowerUpTypes.ColorBomb,
                Resources.Load(Strings.Power_Element + Strings.Tag_ColorBomb) as GameObject);
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
