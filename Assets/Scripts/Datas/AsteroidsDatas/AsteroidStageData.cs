using UnityEngine;

namespace Asteroids.Datas
{
    /// <summary>
    /// This is an "Enum" element with additional info
    /// </summary>
    [CreateAssetMenu(fileName = "New AsteroidStageData", menuName = "Data/AsteroidStageData")]
    public class AsteroidStageData : BaseEntityData
    {
        public float InitSpeed;
        public float MaxSpeed;

        public float InitMaxRot;

        public float Scale;
        public float Mass;
    }
}
