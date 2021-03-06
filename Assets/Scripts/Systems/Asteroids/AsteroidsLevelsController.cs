﻿using Asteroids.Datas;
using Asteroids.Utilities.Messages;

using System.Collections;
using UnityEngine;

namespace Asteroids.Systems
{
    /// <summary>
    /// Handles the levels load using the information provided by AsteroidsLevelsData
    /// </summary>
    public class AsteroidsLevelsController : MonoBehaviour
    {
        private AsteroidsHandler asteroidsHandler;
        private AsteroidsLevelsData asteroidsLevelsData;

        private int currentLevel;
        private int delayToStartLevels;

        public void Setup(AsteroidsLevelsData asteroidsLevelsData)
        {
            this.asteroidsLevelsData = asteroidsLevelsData;
            delayToStartLevels = asteroidsLevelsData.DelayToStartLevels;

            asteroidsHandler =  new AsteroidsHandler(asteroidsLevelsData.AsteroidPref, asteroidsLevelsData.PreloadAsteroidPrefs, 
                                                    asteroidsLevelsData.AsteroidsStagesData, LevelCompletedCallback);
            asteroidsHandler.Setup();
            ResetState();
        }

        public void Unsetup()
        {
            asteroidsHandler.Unsetup();
        }

        #region Levels load methods

        public void ResetState()
        {
            currentLevel = 0;
            asteroidsHandler.ResetAsteroids();
            StartCoroutine(DelayLevelLoad());
        }

        private void LevelCompletedCallback()
        {
            currentLevel++;

            if (currentLevel < asteroidsLevelsData.Levels.Length)
            {
                StartCoroutine(DelayLevelLoad());
            }
            else
            {
                Messenger<bool>.Broadcast(Messages.ON_ENDGAME, true);
            }
        }

        private IEnumerator DelayLevelLoad()
        {
            Messenger<int>.Broadcast(Messages.ON_START_LEVEL, currentLevel);
            yield return new WaitForSeconds(delayToStartLevels);
            asteroidsHandler.LoadLevel(asteroidsLevelsData.Levels[currentLevel]);
        }

        #endregion
    }
}