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
        public int ShotID { get; private set; }

        private float acceleration;
        private float maxSpeed;

        private float lifeTime;
        private float delayDespawn;

        public void Setup(GunData gunData, Vector3 currentVel, int shotID)
        {
            base.Setup();
            this.ShotID = shotID;

            acceleration = gunData.RocketAcceleration;
            maxSpeed = gunData.RocketMaxSpeed;

            delayDespawn = gunData.DelayDespawn;
            lifeTime = gunData.RocketLifeTime;

            rigidbody.velocity = currentVel;

            StartCoroutine(DestroyDelay(lifeTime));
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
            if(delay > 0)
                yield return new WaitForSeconds(delay);

            ActivateEntity(false);
            PlayDestroyFXs();

            yield return new WaitForSeconds(delayDespawn);

            Unsetup();
        }

        protected override void ResolveEntitiesCollision(BaseGameEntity entity)
        {
            if (isAlive)            
                Destroy();          
        }

        private void Destroy()
        {
            StopAllCoroutines();
            StartCoroutine(DestroyDelay(0f));
        }
    }
}