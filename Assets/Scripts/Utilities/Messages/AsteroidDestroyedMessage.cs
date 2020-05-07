using Asteroids.Datas;
using UnityEngine;

namespace Asteroids.Utilities.Messages
{
    public struct AsteroidDestroyedMessage
    {
        public AsteroidStageData Stage;
        public Vector3 AsteroidPos;
        public Vector3 RocketPos;

        public AsteroidDestroyedMessage(AsteroidStageData stage, Vector3 asteroidPos, Vector3 rocketPos)
        {
            Stage = stage;
            AsteroidPos = asteroidPos;
            RocketPos = rocketPos;
        }
    }
}