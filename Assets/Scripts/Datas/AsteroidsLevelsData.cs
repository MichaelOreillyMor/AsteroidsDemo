using Asteroids.Entities;
using UnityEngine;

namespace Asteroids.Datas
{
    [CreateAssetMenu(fileName = "New AsteroidsLevelsData", menuName = "Data/AsteroidsLevelsData")]
    public class AsteroidsLevelsData : ScriptableObject
    {
        public AsteroidState AsteroidPref;
        public AsteroidsStagesData AsteroidsStagesData;

        public AsteroidsLevelData[] Levels;
    }
}
