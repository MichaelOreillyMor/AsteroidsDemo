using UnityEngine;

namespace Asteroids.Datas
{
    [CreateAssetMenu(fileName = "New ThrusterData", menuName = "Data/Modules/ThrusterData")]
    public class ThrusterModuleData : ScriptableObject
    {
        public float Speed;
        public float MaxVelocity;
        public float RotationSpeed;

        public float ThrusterVelocity;
    }
}