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
        private readonly IDictionary<PowerUpTypesEnum, GameObject> _powersPrefabs;
        private readonly IDictionary<GameElementTypesEnum, GameObject> _normalCellsPrefabs;

        public ObjectStorage()
        {
            _powersPrefabs = new Dictionary<PowerUpTypesEnum, GameObject>();
            _normalCellsPrefabs = new Dictionary<GameElementTypesEnum, GameObject>();

            SetUp();
        }
        
        private void SetUp()
        {
            _normalCellsPrefabs.Add(GameElementTypesEnum.OrangeBox,
                Resources.Load(Strings.GAMEPLAY_ELEMENTS + Strings.TAG_ORANGEBOX) as GameObject);
            _normalCellsPrefabs.Add(GameElementTypesEnum.RedCircle,
                Resources.Load(Strings.GAMEPLAY_ELEMENTS + Strings.TAG_REDCIRCLE) as GameObject);
            _normalCellsPrefabs.Add(GameElementTypesEnum.BlueMultiAngle,
                Resources.Load(Strings.GAMEPLAY_ELEMENTS + Strings.TAG_BLUEMULTIANGLE) as GameObject);
            _normalCellsPrefabs.Add(GameElementTypesEnum.YellowUpTriangle,
                Resources.Load(Strings.GAMEPLAY_ELEMENTS + Strings.TAG_YELLOWUPTRIANGLE) as GameObject);
            _normalCellsPrefabs.Add(GameElementTypesEnum.GreenDownTriangle,
                Resources.Load(Strings.GAMEPLAY_ELEMENTS + Strings.TAG_GREENDOWNTIRANGLE) as GameObject);
            
            _powersPrefabs.Add(PowerUpTypesEnum.Horizontal,
                Resources.Load(Strings.POWERUP_ELEMENTS + Strings.TAG_HORIZONTAL) as GameObject);
            _powersPrefabs.Add(PowerUpTypesEnum.Vertical,
                Resources.Load(Strings.POWERUP_ELEMENTS + Strings.TAG_VERICAL) as GameObject);
            _powersPrefabs.Add(PowerUpTypesEnum.Bomb,
                Resources.Load(Strings.POWERUP_ELEMENTS + Strings.TAG_BOMB) as GameObject);
            _powersPrefabs.Add(PowerUpTypesEnum.ColorBomb,
                Resources.Load(Strings.POWERUP_ELEMENTS + Strings.TAG_COLORBOMB) as GameObject);
        }
        
        public GameObject GetRandomGameElement()
        {
            int cellIndex = Random.Range(0, _normalCellsPrefabs.Keys.Count);
            var prefToUse = _normalCellsPrefabs.ElementAt(cellIndex);
            return Object.Instantiate(prefToUse.Value);
        }

        public GameObject GetGameElement(GameElementTypesEnum gameElementTypeEnum)
        {
            GameObject gameElement =  Object.Instantiate(_normalCellsPrefabs[gameElementTypeEnum]);
            return gameElement;
        }

        public GameObject GetPowerElement(PowerUpTypesEnum powerUpTypeEnum)
        {
            GameObject powerGameObject = Object.Instantiate(_powersPrefabs[powerUpTypeEnum]);
            return powerGameObject;
        }

    }
}
