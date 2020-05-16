using Asteroids.Entities;
using UnityEngine;

namespace Asteroids.Datas
{
    [CreateAssetMenu(fileName = "New AsteroidsLevelsData", menuName = "Data/AsteroidsLevelsData")]
    public class AsteroidsLevelsData : ScriptableObject
    {
        public AsteroidState AsteroidPref;
        public int PreloadAsteroidPrefs;

        public int DelayToStartLevels;

        public AsteroidsStagesData AsteroidsStagesData;
        public AsteroidsLevelData[] Levels;
    }
}
