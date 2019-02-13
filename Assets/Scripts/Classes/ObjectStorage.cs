using System.Collections.Generic;
using System.Linq;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using UnityEngine;

namespace Mathc3Project
{
    public class ObjectStorage : IObjectStorage
    {
        private GameObject backtilePref;
        private IList<GameObject> prefabsList;
        
        public ObjectStorage()
        {
            prefabsList = new List<GameObject>();
            backtilePref = Resources.Load("Prefabs/Empty") as GameObject;
            
            Object[] objTempArray = Resources.LoadAll("Prefabs/Elements/");
            for (int i = 0; i < objTempArray.Length; i++)
            {
                prefabsList.Add(objTempArray[i] as GameObject);
            }
        }
        
        public GameObject GetObjectByType(GameElementsType gameElementType)
        {
            GameObject gameElement = null;
            switch (gameElementType)
            {
                case GameElementsType.BackTile:
                    gameElement = Object.Instantiate(backtilePref);
                    break;
                
                case GameElementsType.RandomPrefab:
                    int cellIndex = Random.Range(0, prefabsList.Count);
                    var prefToUse = prefabsList.ElementAt(cellIndex);
                    gameElement = Object.Instantiate(prefToUse);
                    break;
            }
            
           return gameElement;
        }
    }
}