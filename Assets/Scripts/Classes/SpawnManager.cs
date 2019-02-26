using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using UnityEngine;

namespace Mathc3Project
{
    public class SpawnManager : ISpawnManager
    {
        private IObjectStorage _objectStorage;
        private IObjectSetter _objectSetter;

        public SpawnManager(IObjectStorage objectStorage, IObjectSetter objectSetter)
        {
            _objectStorage = objectStorage;
            _objectSetter = objectSetter;
        }

        public void GenerateBackTile(Vector3 position, GameObject parent)
        {
            GameObject backTile = _objectStorage.GetBackTile();
            _objectSetter.SetNonGameplayObject(backTile, parent, position);
        }

        public ICell GenerateNormalCell(Vector3 position)
        {
            GameObject normalGameObject = _objectStorage.GetRandomGameElement();
            normalGameObject.transform.position = position;

            ICell normalCell = _objectSetter.SetGameplayObject(CellTypes.Normal, normalGameObject);

            return normalCell;
        }

        public ICell GenerateHollowCell(Vector3 position)
        {
            GameObject hollowGameObject = new GameObject("Hollow");
            hollowGameObject.transform.position = position;

            ICell hollowCell = _objectSetter.SetGameplayObject(CellTypes.Hollow, hollowGameObject);

            return hollowCell;
        }

        public ICell GenerateBreakableCell(Vector3 position)
        {
            GameObject breakableGameObject = _objectStorage.GetBreackableCell();
            breakableGameObject.transform.position = position;

            ICell breakableCell = _objectSetter.SetGameplayObject(CellTypes.Breakable, breakableGameObject);

            return breakableCell;
        }
    }
}
