using Asteroids.Entities;
using UnityEngine;

namespace Asteroids.Datas
{
    [CreateAssetMenu(fileName = "New BaseGunData", menuName = "Data/GunDatas/BaseGunData")]
    public class BaseGunData : ScriptableObject
    {
        public RocketState RocketPref;
        public int PreloadRocketPrefs;

        public float ReloadTime;
        public float RocketLifeTime;

        public float RocketAcceleration;
        public float RocketMaxSpeed;

        public float DelayDespawnRockets;
    }
}