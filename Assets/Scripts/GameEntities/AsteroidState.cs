using Asteroids.Datas;
using Asteroids.Utilities.Messages;
using Asteroids.Utilities.Pools;

using UnityEngine;

namespace Asteroids.Entities
{    
     /// <summary>
     /// Basic asteroid entity
     /// </summary>
    public class AsteroidState : BaseGameEntity
    {
        private AsteroidStageData asteroidStageData;

        private float initSpeed;
        private float initMaxRot;

        public void Setup(AsteroidStageData asteroidStageData, Vector3 direction)
        {
            base.Setup();

            this.asteroidStageData = asteroidStageData;

            initSpeed = asteroidStageData.InitSpeed;
            initMaxRot = asteroidStageData.InitMaxRot;

            SetPhysicalProperties(asteroidStageData);
            SetRandInitForces(direction);
        }

        private void SetPhysicalProperties(AsteroidStageData asteroidStageData)
        {
            transform.localScale = Vector3.one * asteroidStageData.Scale;
            transform.rotation = Quaternion.Euler(Random.insideUnitSphere * 180f);
            rigidbody.mass = asteroidStageData.Mass;
        }

        private void SetRandInitForces(Vector3 direction)
        {
            Vector3 randRot = Random.insideUnitSphere * initMaxRot;

            rigidbody.AddForce(direction * initSpeed);
            rigidbody.AddTorque(randRot, ForceMode.Impulse);
        }

        public override void Unsetup()
        {
            base.Unsetup();

            audioSource.Stop();
            SimplePool.Despawn(this);
        }

        protected override void ResolveEntitiesCollision(BaseGameEntity entity)
        {
            if (isAlive)
            {
                if (entity is RocketState)
                {
                    Destroy(entity.transform.position);
                }
                else
                {
                    audioSource.Play();
                }
            }
        }

        private void Destroy(Vector3 rocketPos)
        {
            PlayDestroyFXs(); 
            Unsetup();

            AsteroidDestroyedMessage asteroidDestroyedMessage = new AsteroidDestroyedMessage(asteroidStageData, transform.position, rocketPos);
            Messenger<AsteroidDestroyedMessage>.Broadcast("OnAsteroidDestroyed", asteroidDestroyedMessage);
        }
    }
}