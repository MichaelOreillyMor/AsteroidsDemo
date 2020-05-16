using Asteroids.Datas;
using UnityEngine;

namespace Asteroids.Utilities.Messages
{
    public struct AsteroidDestroyedMessage
    {
        public AsteroidStageData stageData;
        public Vector3 AsteroidPos;
        public Vector3 RocketPos;

        public AsteroidDestroyedMessage(AsteroidStageData stageData, Vector3 asteroidPos, Vector3 rocketPos)
        {
            this.stageData = stageData;
            AsteroidPos = asteroidPos;
            RocketPos = rocketPos;
        }
    }
}