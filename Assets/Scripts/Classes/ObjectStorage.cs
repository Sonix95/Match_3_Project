using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using UnityEngine;

namespace Mathc3Project
{
    public class ObjectStorage : IObjectStorage
    {
        private GameObject backtilePref;
        private GameObject _breackablePref;
        private IList<GameObject> prefabsList;

        public ObjectStorage()
        {
            prefabsList = new List<GameObject>();
            backtilePref = Resources.Load("Prefabs/Empty") as GameObject;
            _breackablePref = Resources.Load("Prefabs/Breackable") as GameObject;

            Object[] objTempArray = Resources.LoadAll("Prefabs/Elements/");
            for (int i = 0; i < objTempArray.Length; i++)
            {
                prefabsList.Add(objTempArray[i] as GameObject);
            }
        }

        public GameObject GetBackTile()
        {
            return Object.Instantiate(backtilePref);
        }

        public GameObject GetBreackableCell()
        {
            return Object.Instantiate(_breackablePref);
        }

        public GameObject GetGameElement(int cellIndex)
        {
            if (cellIndex >= 0 && cellIndex <= prefabsList.Count)
            {
                var prefToUse = prefabsList.ElementAt(cellIndex);
                return Object.Instantiate(prefToUse);
            }

            return null;
        }

        public GameObject GetRandomGameElement()
        {
            int cellIndex = Random.Range(0, prefabsList.Count);
            var prefToUse = prefabsList.ElementAt(cellIndex);
            return Object.Instantiate(prefToUse);
        }
    }
}
