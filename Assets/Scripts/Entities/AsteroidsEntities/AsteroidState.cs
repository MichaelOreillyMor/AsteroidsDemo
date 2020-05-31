using Asteroids.Datas;
using Asteroids.Utilities.Messages;
using Asteroids.Utilities.Pools;

using UnityEngine;

namespace Asteroids.Entities
{    
     /// <summary>
     /// Basic asteroid entity
     /// </summary>
    public class AsteroidState : BaseEntityState
    {
        private AsteroidStageData asteroidStageData;

        private float initSpeed;
        private float maxSpeed;

        private float initMaxRot;

        private string shotCreatedAsteroidID;

        #region Setup/Unsetup methods

        /// <summary>
        /// Initialize an asteroid state
        /// </summary>
        /// <param name="asteroidStageData">Stage data of the asteroid (big, medium, small)</param>
        /// <param name="direction">Direction of the asteroid</param>
        /// <param name="shotCreatedAsteroidID">ID Shot that split the previous asteroid and created this piece</param>
        public void Setup(AsteroidStageData asteroidStageData, Vector3 direction, string shotCreatedAsteroidID)
        {
            base.Setup(asteroidStageData);

            this.asteroidStageData = asteroidStageData;
            this.shotCreatedAsteroidID = shotCreatedAsteroidID;

            initSpeed = asteroidStageData.InitSpeed;
            maxSpeed = asteroidStageData.MaxSpeed;

            initMaxRot = asteroidStageData.InitMaxRot;

            SetPhysicalProperties(asteroidStageData);
            SetRandInitForces(direction);
        }

        #region Physics methods

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

        #endregion

        #endregion

        #region Update methods

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (rigidbody.velocity.magnitude > maxSpeed)
                rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
        }

        #endregion

        #region Collisions methods

        protected override void ResolveEntitiesCollision(BaseEntityState entity)
        {
            if (isAlive)
            {
                if (entity is RocketState)
                {
                    string shotID = entity.GetComponent<RocketState>().ShotID;

                    if (shotID != shotCreatedAsteroidID)
                        Destroy(entity.transform.position, shotID);
                }
                else
                {
                    audioSource.Play();
                }
            }
        }

        private void Destroy(Vector3 rocketPos, string shotID)
        {
            PlayDestroyFXs(); 
            Unsetup();

            AsteroidDestroyedMessage asteroidDestroyedMessage = new AsteroidDestroyedMessage(asteroidStageData, transform.position, rocketPos, shotID);
            Messenger<AsteroidDestroyedMessage>.Broadcast("OnAsteroidDestroyed", asteroidDestroyedMessage);
        }

        #endregion
    }
}