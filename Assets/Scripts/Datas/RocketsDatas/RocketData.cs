using Asteroids.Entities;

using UnityEngine;

namespace Asteroids.Datas
{
    [CreateAssetMenu(fileName = "New RocketData", menuName = "Data/RocketData")]
    public class RocketData : BaseEntityData
    {
        public RocketState RocketPref;

        public float LifeTime;

        public float Acceleration;
        public float MaxSpeed;

        public float DelayDespawn;
    }
}