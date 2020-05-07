using Asteroids.Systems;
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
        [SerializeField]
        protected new AudioSource audioSource;

        [SerializeField]
        protected new Collider collider;

        [SerializeField]
        protected new Rigidbody rigidbody;

        [SerializeField]
        protected GameObject model3D;

        [SerializeField]
        protected ParticlesSystemsPlayer particlesSystemsPlayer;

        protected static ScreenLismitsHandler screenLismitsHandler;

        #region Setup/Unsetup methods

        public virtual void Setup()
        {
            ActiveModel(true);

            if(screenLismitsHandler == null)
                screenLismitsHandler = new ScreenLismitsHandler();
        }

        public virtual void Unsetup()
        {
            ActiveModel(false);
        }

        public virtual void ActiveModel(bool b)
        {
            model3D.SetActive(b);
            collider.enabled = b;
            rigidbody.isKinematic = !b;
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

        private void OnCollisionEnter(Collision other)
        {
            BaseGameEntity entity = other.gameObject.GetComponent<BaseGameEntity>();

            if (entity)
                ResolveEntitiesCollision(entity);
        }

        private void OnTriggerEnter(Collider other)
        {
            BaseGameEntity entity = other.gameObject.GetComponent<BaseGameEntity>();

            if (entity)
                ResolveEntitiesCollision(entity);
        }

        #endregion

        protected virtual void FixedUpdate()
        {
            screenLismitsHandler.FixLimitsPosition(transform);
        }
    }
}
