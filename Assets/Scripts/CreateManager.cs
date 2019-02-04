using UnityEngine;
using Mathc3Project.Interfaces;

namespace Mathc3Project
{
    public class CreateManager : ICreateManager
    {
        private IGameLogicManager _gameLogicManager;

        public CreateManager(IGameLogicManager gameLogicManager)
        {
            _gameLogicManager = gameLogicManager;
        }

        public IGameElement CreateGameElement(int column, float yCoord, bool updateObject)
        {
            IGameElement element = GameObject.CreatePrimitive(PrimitiveType.Sphere).AddComponent<GameElement>();

            element.Size = 0.8f;
            element.Color = new Color(0, Random.Range(0, 3), Random.Range(0, 3));
            element.Name = Random.Range(0, 1000).ToString();
            element.CurrentPosition = new Vector3(column, yCoord);

            //
            element.IsUpdate = updateObject;
            //

            _gameLogicManager.AddGameElementInList(element);

            return element;
        }


           
    }
}