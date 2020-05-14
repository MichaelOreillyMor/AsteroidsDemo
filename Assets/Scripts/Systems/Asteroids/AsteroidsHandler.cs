using Asteroids.Datas;
using Asteroids.Entities;
using Asteroids.Utilities.Messages;
using Asteroids.Utilities.Pools;

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Systems
{
    /// <summary>
    /// Handles the load and management of a level´s asteroids
    /// </summary>
    public class AsteroidsHandler
    {
        private const int PRELOAD_ASTEROIDS = 10;
        private const int SMALL_ASTEROIDS_PER_BIG = 4;

        private AsteroidState asteroidPref;
        private AsteroidsStagesData asteroidsStagesData;

        private Action asteroidsCleanedCallback;

        private int asteroidsToClean;
        private int asteroidsCleaned;

        private Camera camera;

        public AsteroidsHandler(AsteroidState asteroidPref, AsteroidsStagesData asteroidsStagesData, Action asteroidsCleanedCallback)
        {
            this.asteroidPref = asteroidPref;
            this.asteroidsStagesData = asteroidsStagesData;
            this.asteroidsCleanedCallback = asteroidsCleanedCallback;

            camera = Camera.main;

            SimplePool.Preload(asteroidPref, PRELOAD_ASTEROIDS);
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
            Vector3 position = camera.ViewportToWorldPoint(asteroidSpawnInfo.ViewportPosition);
            position.y = 0;

            SpawnAsteroid(asteroidsStagesData.BigStage, position, asteroidSpawnInfo.Direction);
        }

        private void SpawnAsteroid(AsteroidStageData stage, Vector3 position, Vector3 dir)
        {
            AsteroidState asteroid = (AsteroidState)SimplePool.Spawn(asteroidPref, position, Quaternion.identity);
            asteroid.Setup(stage, dir);
        }

        public void ResetAsteroids()
        {
            List<PoolMember> asteroidsActive = SimplePool.GetActiveInstances(asteroidPref);
            AsteroidState asteroid;

            for (int i = asteroidsActive.Count - 1; i >= 0; i--)
            {
                asteroid = (AsteroidState)asteroidsActive[i];
                asteroid.Unsetup();
            }
        }

        #endregion

        #region Asteroids destruction methods

        private void ProcessAsteroidDestroyed(AsteroidDestroyedMessage asteroidDestroyedMessage)
        {
            AsteroidStageData nexStage = GetNextAsteroidStage(asteroidDestroyedMessage.Stage);

            if (nexStage != null)
                SpawnSplitAsteroids(asteroidDestroyedMessage.AsteroidPos, asteroidDestroyedMessage.RocketPos, nexStage);

            else
                AddCleanedAsteroid();
        }

        private void AddCleanedAsteroid()
        {
            asteroidsCleaned++;

            if (asteroidsCleaned == asteroidsToClean)
                asteroidsCleanedCallback();
        }

        private void SpawnSplitAsteroids(Vector3 asteroidPos, Vector3 rocketPos, AsteroidStageData nexStage)
        {
            float distance = Vector3.Distance(asteroidPos, rocketPos);

            Vector3 dirHit = (rocketPos - asteroidPos).normalized;

            Vector3 leftAsteroidPos = asteroidPos + new Vector3(dirHit.z, 0, -dirHit.x) * distance;
            Vector3 rightAsteroidPos = asteroidPos + new Vector3(-dirHit.z, 0, dirHit.x) * distance;

            Vector3 leftAsteroidDir = (leftAsteroidPos - asteroidPos).normalized;
            Vector3 rightAsteroidDir = (rightAsteroidPos - asteroidPos).normalized;

            SpawnAsteroid(nexStage, leftAsteroidPos, leftAsteroidDir);
            SpawnAsteroid(nexStage, rightAsteroidPos, rightAsteroidDir);
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