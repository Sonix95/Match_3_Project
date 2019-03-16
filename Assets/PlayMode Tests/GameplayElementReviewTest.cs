﻿using System.Collections;
using Mathc3Project.Classes;
using Mathc3Project.Classes.Cells;
using Mathc3Project.Enums;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Cells;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class GameplayElementReviewTest
    {
        [UnityTest]
        public IEnumerator GameplayElement_Review()
        {
            #region Prepare

            IObjectStorage objectStorage = new ObjectStorage();
            ISpawnManager spawnManager = new SpawnManager(objectStorage);

            #endregion

            yield return new WaitForSeconds(0.5f);

            #region Prepare Cell and Output to Screen

            Vector3 position = new Vector3(0, 0, 0);

            ICell cell = new NormalCell(0, 0);

            foreach (GameElementTypes gameElement in System.Enum.GetValues(typeof(GameElementTypes)))
            {
                cell.CurrentGameObject = spawnManager.SpawnPrefab(gameElement, position);

                yield return new WaitForSeconds(0.5f);

                GameObject.Destroy(cell.CurrentGameObject);
            }

            #endregion
        }

        [UnityTest]
        public IEnumerator PowerUp_Review()
        {
            #region Prepare

            IObjectStorage objectStorage = new ObjectStorage();
            ISpawnManager spawnManager = new SpawnManager(objectStorage);

            #endregion

            yield return new WaitForSeconds(0.5f);

            #region Prepare Cell and Output to Screen

            Vector3 position = new Vector3(0, 0, 0);

            ICell cell = new NormalCell(0, 0);

            foreach (PowerUpTypes powerUp in System.Enum.GetValues(typeof(PowerUpTypes)))
            {
                cell.CurrentGameObject = spawnManager.SpawnPowerPrefab(powerUp, position);

                yield return new WaitForSeconds(0.5f);

                GameObject.Destroy(cell.CurrentGameObject);
            }

            #endregion
        }

    }
}
