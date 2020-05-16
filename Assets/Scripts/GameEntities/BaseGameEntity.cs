using Asteroids.Effects;
using Asteroids.Utilities.Pools;

using UnityEngine;

namespace Asteroids.Entities
{
    /// <summary>
    /// Base entity logic
    /// </summary>
    public abstract class BaseGameEntity : PoolMember
    {
        protected static ScreenLismitsHandler screenLismitsHandler;

        [SerializeField]
        protected AudioSource audioSource;

        [SerializeField]
        protected new Collider collider;

        [SerializeField]
        protected new Rigidbody rigidbody;

        [SerializeField]
        protected GameObject model3D;

        [SerializeField]
        protected ParticlesSystemsPlayer particlesSystemsPlayer;

        protected bool isAlive;

        #region Setup/Unsetup methods

        public virtual void Setup()
        {
            if(screenLismitsHandler == null)
                screenLismitsHandler = new ScreenLismitsHandler();

            ActivateEntity(true);
        }

        public virtual void Unsetup()
        {
            ActivateEntity(false);
        }

        protected virtual void ActivateEntity(bool b)
        {
            if(model3D)
                model3D.SetActive(b);

            collider.enabled = b;
            rigidbody.isKinematic = !b;
            isAlive = b;
        }

        #endregion

        #region Manage entity destruction

        protected void PlayDestroyFXs()
        {
            if (particlesSystemsPlayer)
            {
                ParticlesSystemsPlayer particlesSystemsPlayerGO = (ParticlesSystemsPlayer)SimplePool.Spawn(particlesSystemsPlayer, rigidbody.position, Quaternion.identity);
                particlesSystemsPlayerGO.Play();
            }  
        }

        #endregion

        #region Resolve collisions

        protected abstract void ResolveEntitiesCollision(BaseGameEntity entity);

        private void OnCollision(Collider other)
        {
            if (isAlive)
            {
                BaseGameEntity entity = other.gameObject.GetComponent<BaseGameEntity>();

                if (entity)
                    ResolveEntitiesCollision(entity);
            }
        }
  
        private void OnCollisionEnter(Collision collision)
        {
            OnCollision(collision.collider);
        }

        private void OnTriggerEnter(Collider other)
        {
            OnCollision(other);
        }

        #endregion

        protected virtual void FixedUpdate()
        {
            screenLismitsHandler.FixLimitsPosition(transform);
        }
    }
}
