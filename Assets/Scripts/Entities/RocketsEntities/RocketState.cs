using Asteroids.Datas;
using Asteroids.Utilities.Pools;

using System.Collections;
using UnityEngine;

namespace Asteroids.Entities
{
    /// <summary>
    /// Basic rocket entity
    /// </summary>
    public class RocketState : BaseEntityState
    {
        public string ShotID { get; private set; }

        private float acceleration;
        private float maxSpeed;

        private float lifeTime;
        private float delayDespawn;

        #region Setup/Unsetup methods

        public void Setup(RocketData rocketData, Vector3 currentVel, string shotID)
        {
            base.Setup(rocketData);
            ShotID = shotID;

            acceleration = rocketData.Acceleration;
            maxSpeed = rocketData.MaxSpeed;

            delayDespawn = rocketData.DelayDespawn;
            lifeTime = rocketData.LifeTime;

            rigidbody.velocity = currentVel;

            StartCoroutine(DestroyDelay(lifeTime));
            audioSource.Play();
        }

        #endregion

        #region Update methods

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            rigidbody.AddRelativeForce(Vector3.forward * acceleration);

            if(rigidbody.velocity.magnitude > maxSpeed)
                rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
        }

        #endregion

        #region Delay methods

        private IEnumerator DestroyDelay(float delay)
        {
            if (delay > 0)
                yield return new WaitForSeconds(delay);

            ActivateEntity(false);
            PlayDestroyFXs();

            yield return new WaitForSeconds(delayDespawn);

            Unsetup();
        }

        #endregion

        #region Collisions methods

        protected override void ResolveEntitiesCollision(BaseEntityState entity)
        {
            if (isAlive)            
                Destroy();          
        }

        private void Destroy()
        {
            StopAllCoroutines();
            StartCoroutine(DestroyDelay(0f));
        }

        #endregion
    }
}