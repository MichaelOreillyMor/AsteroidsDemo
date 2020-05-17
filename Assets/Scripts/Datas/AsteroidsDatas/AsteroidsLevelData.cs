using UnityEngine;

namespace Asteroids.Datas
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New AsteroidsLevelData", menuName = "Data/AsteroidsLevelData")]
    public class AsteroidsLevelData : ScriptableObject
    {
        public AsteroidSpawnInfo[] AsteroidsSpawnInfo;
    }
}
