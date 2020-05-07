using Asteroids.Datas;
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
        private static int TIME_TO_START_LEVEL = 2;

        public AsteroidsHandler asteroidsHandler { get; set; }

        private AsteroidsLevelsData asteroidsLevelsData;
        private int currentLevel;

        public void Setup(AsteroidsLevelsData asteroidsLevelsData)
        {
            this.asteroidsLevelsData = asteroidsLevelsData;
            asteroidsHandler = new AsteroidsHandler(asteroidsLevelsData.AsteroidPref, asteroidsLevelsData.AsteroidsStagesData, LevelCompletedCallback);

            currentLevel = 0;
            StartCoroutine(DelayLevelLoad());
        }

        public void Unsetup()
        {
            asteroidsHandler.UnsubscribeEvents();
        }

        #region Levels load methods

        public void ResetLevels()
        {
            asteroidsHandler.ResetAsteroids();

            currentLevel = 0;
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
                Messenger<bool>.Broadcast("OnEndGame", true);
            }
        }

        private IEnumerator DelayLevelLoad()
        {
            Messenger<int>.Broadcast("OnStartLevel", currentLevel);
            yield return new WaitForSeconds(TIME_TO_START_LEVEL);
            asteroidsHandler.LoadLevel(asteroidsLevelsData.Levels[currentLevel]);
        }

        #endregion
    }
}