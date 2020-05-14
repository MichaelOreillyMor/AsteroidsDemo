using Asteroids.Datas;
using Asteroids.Utilities.Pools;

using System.Collections;
using UnityEngine;

namespace Asteroids.Entities
{
    /// <summary>
    /// Basic rocket entity
    /// </summary>
    public class RocketState : BaseGameEntity
    {
        private float acceleration;
        private float maxSpeed;
        private float delayFX;
        public void Setup(GunData gunData, Vector3 currentVel)
        {
            base.Setup();

            acceleration = gunData.RocketAcceleration;
            maxSpeed = gunData.RocketMaxSpeed;
            delayFX = gunData.DelayToFX;

            rigidbody.velocity = currentVel;

            StartCoroutine(DestroyDelay(gunData.RocketLifeTime));
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

        private IEnumerator DestroyDelay(float delay)
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