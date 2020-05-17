using Asteroids.Datas;
using Asteroids.Entities;
using Asteroids.Utilities.Messages;
using Asteroids.Utilities.Pools;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Systems
{
    /// <summary>
    /// Handles the load and management of a level´s asteroids
    /// </summary>
    public class AsteroidsHandler
    {
        private const int SMALL_ASTEROIDS_PER_BIG = 4;

        private AsteroidState asteroidPref;
        private AsteroidsStagesData asteroidsStagesData;

        private Action asteroidsCleanedCallback;

        private int asteroidsToClean;
        private int asteroidsCleaned;

        private Camera cameraMain;

        public AsteroidsHandler(AsteroidState asteroidPref, int preloadAsteroidPrefs, AsteroidsStagesData asteroidsStagesData, Action asteroidsCleanedCallback)
        {
            this.asteroidPref = asteroidPref;
            this.asteroidsStagesData = asteroidsStagesData;
            this.asteroidsCleanedCallback = asteroidsCleanedCallback;

            cameraMain = Camera.main;

            SimplePool.Preload(asteroidPref, preloadAsteroidPrefs);
            SimplePool.Preload(asteroidsStagesData.BigStage.DestroyFXPlayerPref, preloadAsteroidPrefs);

            Messenger<AsteroidDestroyedMessage>.AddListener("OnAsteroidDestroyed", ProcessAsteroidDestroyed);
        }

        public void UnsubscribeEvents()
        {
            Messenger<AsteroidDestroyedMessage>.RemoveListener("OnAsteroidDestroyed", ProcessAsteroidDestroyed);
        }

        #region Asteroids load methods

        public void LoadLevel(AsteroidsLevelData asteroidsLevelData)
        {
            asteroidsToClean = asteroidsLevelData.AsteroidsSpawnInfo.Length * SMALL_ASTEROIDS_PER_BIG;
            asteroidsCleaned = 0;

            foreach (AsteroidSpawnInfo asteroidSpawnInfo in asteroidsLevelData.AsteroidsSpawnInfo)
            {
                LoadAsteroid(asteroidSpawnInfo);
            }
        }

        private void LoadAsteroid(AsteroidSpawnInfo asteroidSpawnInfo)
        {
            Vector3 position = cameraMain.ViewportToWorldPoint(asteroidSpawnInfo.ViewportPosition);
            position.y = 0;

            SpawnAsteroid(asteroidsStagesData.BigStage, position, asteroidSpawnInfo.Direction, "");
        }

        private void SpawnAsteroid(AsteroidStageData stage, Vector3 position, Vector3 dir, string shotCreatorAsteroidID)
        {
            AsteroidState asteroid = (AsteroidState)SimplePool.Spawn(asteroidPref, position, Quaternion.identity);
            asteroid.Setup(stage, dir, shotCreatorAsteroidID);
        }

        public void ResetAsteroids()
        {
            List<PoolMember> asteroidsActive = SimplePool.GetActiveInstances(asteroidPref);

            if (asteroidsActive.Count > 0)
            {
                for (int i = asteroidsActive.Count - 1; i >= 0; i--)
                {
                    AsteroidState asteroid = (AsteroidState)asteroidsActive[i];
                    asteroid.Unsetup();
                }
            }
        }

        #endregion

        #region Asteroids destruction methods

        private void ProcessAsteroidDestroyed(AsteroidDestroyedMessage destroyMssg)
        {
            if (destroyMssg.stageData != asteroidsStagesData.SmallStage)
                SpawnSplitAsteroids(destroyMssg);
            else
                AddCleanedAsteroid();
        }

        private void AddCleanedAsteroid()
        {
            asteroidsCleaned++;

            if (asteroidsCleaned == asteroidsToClean)
                asteroidsCleanedCallback();
        }

        private void SpawnSplitAsteroids(AsteroidDestroyedMessage destroyMssg)
        {
            SpawnSplitAsteroid(destroyMssg, Vector3.up);
            SpawnSplitAsteroid(destroyMssg, Vector3.down);
        }

        private void SpawnSplitAsteroid(AsteroidDestroyedMessage destroyMssg, Vector3 hitDirection)
        {
            float distance = Vector3.Distance(destroyMssg.AsteroidPos, destroyMssg.RocketPos);

            Vector3 direction = destroyMssg.RocketPos - destroyMssg.AsteroidPos;
            direction = Vector3.Cross(direction, hitDirection).normalized;
            direction *= distance;

            Vector3 asteroidPos = destroyMssg.AsteroidPos + direction;
            Vector3 asteroidDir = (asteroidPos - destroyMssg.AsteroidPos).normalized;

            AsteroidStageData stageData = GetNextAsteroidStage(destroyMssg.stageData);
            SpawnAsteroid(stageData, asteroidPos, asteroidDir, destroyMssg.ShotID);
        }

        #endregion

        #region Utilities methods

        private AsteroidStageData GetNextAsteroidStage(AsteroidStageData stage)
        {
            AsteroidStageData newStage = null;

            if (stage == asteroidsStagesData.BigStage)
            {
                newStage = asteroidsStagesData.MediumStage;
            }
            else if (stage == asteroidsStagesData.MediumStage)
            {
                newStage = asteroidsStagesData.SmallStage;
            }

            return newStage;
        }

        #endregion
    }
}