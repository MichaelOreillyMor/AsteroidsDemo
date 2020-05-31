using Asteroids.Entities;
using UnityEngine;

namespace Asteroids.Datas
{
    [CreateAssetMenu(fileName = "New SpaceshipData", menuName = "Data/SpaceshipData")]
    public class SpaceshipData : BaseEntityData
    {
        public SpaceshipState SpaceshipPref;
        public GameObject SpaceshipModel;

        public BaseGunData BaseGunData;
        public SpecialGunData SpecialGunData;

        public ThrusterData ThrusterData;

        public AudioClip EngineSound;
    }
}