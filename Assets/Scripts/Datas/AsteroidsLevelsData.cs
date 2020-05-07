using Asteroids.Entities;
using UnityEngine;

namespace Asteroids.Datas
{
    [CreateAssetMenu(fileName = "New AsteroidsLevelsData", menuName = "Data/AsteroidsLevelsData")]
    public class AsteroidsLevelsData : ScriptableObject
    {
        public Asteroid AsteroidPref;
        public AsteroidsStagesData AsteroidsStagesData;

        public AsteroidsLevelData[] Levels;
    }
}
