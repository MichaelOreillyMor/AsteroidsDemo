using Asteroids.Entities;
using UnityEngine;

namespace Asteroids.Datas
{
    [CreateAssetMenu(fileName = "New SpaceshipData", menuName = "Data/SpaceshipData")]
    public class SpaceshipData : BaseEntityData
    {
        public SpaceshipState SpaceshipPref;
        public GameObject SpaceshipModel;

        public GunsModuleData GunsData;
        public ThrusterModuleData ThrusterData;
        public AnimModuleData AnimData;

        public AudioClip EngineSound;
    }
}