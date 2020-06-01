using UnityEngine;

namespace Asteroids.Datas
{
    [CreateAssetMenu(fileName = "New ThrusterData", menuName = "Data/ThrusterData")]
    public class ThrusterModuleData : ScriptableObject
    {
        public float Speed;
        public float MaxVelocity;
        public float RotationSpeed;

        public float ThrusterVelocity;
    }
}