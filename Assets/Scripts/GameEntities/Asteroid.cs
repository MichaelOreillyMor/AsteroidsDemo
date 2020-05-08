using Asteroids.Datas;
using Asteroids.Utilities.Messages;
using Asteroids.Utilities.Pools;

using UnityEngine;

namespace Asteroids.Entities
{    
     /// <summary>
     /// Basic asteroid entity
     /// </summary>
    public class Asteroid : BaseGameEntity
    {
        private static float INIT_ROT_MULT = 0.0035f;
        private AsteroidStageData asteroidStageData;

        public void Setup(AsteroidStageData asteroidStageData, Vector3 direction)
        {
            base.Setup();
            this.asteroidStageData = asteroidStageData;

            SetRandInitForces();

            transform.localScale = Vector3.one * asteroidStageData.Scale;

            rigidbody.mass = asteroidStageData.mass;
            rigidbody.AddForce(direction * asteroidStageData.Speed);
        }

        private void SetRandInitForces()
        {
            transform.rotation = Quaternion.Euler(UnityEngine.Random.Range(0, 360),
                UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));

            float initMaxRot = asteroidStageData.Speed * INIT_ROT_MULT;
            Vector3 randRot = new Vector3(UnityEngine.Random.Range(-initMaxRot, initMaxRot),
                UnityEngine.Random.Range(-initMaxRot, initMaxRot), UnityEngine.Random.Range(-initMaxRot, initMaxRot));

            rigidbody.AddTorque(randRot, ForceMode.Impulse);
        }

        public override void Unsetup()
        {
            base.Unsetup();

            asteroidStageData = null;
            audioSource.Stop();
            SimplePool.Despawn(this);
        }

        protected override void ResolveEntitiesCollision(BaseGameEntity entity)
        {
            if (entity is Rocket)
            {
                Destroy(entity.transform.position);
            }
            else
            {
                audioSource.Play();
            }
        }

        private void Destroy(Vector3 rocketPos)
        {
            PlayDestroyFXs();
            Messenger<AsteroidDestroyedMessage>.Broadcast("OnAsteroidDestroyed", new AsteroidDestroyedMessage(asteroidStageData, transform.position, rocketPos));
            Unsetup();
        }
    }
}