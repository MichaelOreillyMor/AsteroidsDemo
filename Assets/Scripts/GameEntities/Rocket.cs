using Asteroids.Systems;
using Asteroids.Utilities.Pools;

using System.Collections;
using UnityEngine;

namespace Asteroids.Entities
{
    /// <summary>
    /// Basic rocket entity
    /// </summary>
    public class Rocket : BaseGameEntity
    {
        private float acceleration;
        private float maxSpeed;

        public void Setup(Vector3 currentVel, float acceleration, float maxSpeed, float maxLifeTime)
        {
            base.Setup();

            this.acceleration = acceleration;
            this.maxSpeed = maxSpeed;
            rigidbody.velocity = currentVel;

            StartCoroutine(AutoDestroyDelay(maxLifeTime));
            audioSource.Play();
        }

        public override void Unsetup()
        {
            base.Unsetup();

            audioSource.Stop();
            SimplePool.Despawn(this);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            rigidbody.AddRelativeForce(Vector3.forward * acceleration);
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
        }

        protected IEnumerator AutoDestroyDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            PlayDestroyFXs();
            Unsetup();
        }

        protected override void ResolveEntitiesCollision(BaseGameEntity entity)
        {
            Destroy();
        }

        private void Destroy()
        {
            StopAllCoroutines();
            PlayDestroyFXs();
            Unsetup();
        }
    }
}