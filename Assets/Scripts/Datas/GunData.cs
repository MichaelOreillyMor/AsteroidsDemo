using Asteroids.Entities;
using UnityEngine;

namespace Asteroids.Datas
{
    [CreateAssetMenu(fileName = "New GunData", menuName = "Data/GunData")]
    public class GunData : ScriptableObject
    {
        public RocketState RocketPref;
        public int PreloadRocketPrefs;

        public float ReloadTime;
        public float RocketLifeTime;

        public float RocketAcceleration;
        public float RocketMaxSpeed;

        public int RocketsPerShot;

        [Range(0, 360)]
        public int ShotArc;

        public float DelayDespawn;
    }
}
