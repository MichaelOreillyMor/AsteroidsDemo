using UnityEngine;

namespace Asteroids.Datas
{
    [CreateAssetMenu(fileName = "New SpaceshipData", menuName = "Data/SpaceshipData")]
    public class SpaceshipData : ScriptableObject
    {
        public GameObject SpaceshipPref;

        public GunData MainGunData;
        public GunData SecondaryGunData;

        public float Speed;
        public float MaxVelocity;
        public float RotationSpeed;

        public float ThrusterVelocity;
        public AudioClip EngineSound;
    }
}