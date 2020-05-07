using Asteroids.Entities;
using UnityEngine;

namespace Asteroids.Datas
{
    [CreateAssetMenu(fileName = "New GunData", menuName = "Data/GunData")]
    public class GunData : ScriptableObject
    {
        public Rocket BulletPref;
        public float ReloadTime;
        public float RocketLifeTime;
        public float RocketAcceleration;
        public float RocketMaxSpeed;
    }
}
