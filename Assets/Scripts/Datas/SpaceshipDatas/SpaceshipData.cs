using UnityEngine;

namespace Asteroids.Datas
{
    [CreateAssetMenu(fileName = "New SpaceshipData", menuName = "Data/SpaceshipData")]
    public class SpaceshipData : BaseEntityData
    {
        public GameObject SpaceshipPref;

        public BaseGunData BaseGunData;
        public SpecialGunData SpecialGunData;

        public float Speed;
        public float MaxVelocity;
        public float RotationSpeed;

        public float ThrusterVelocity;
        public AudioClip EngineSound;
    }
}