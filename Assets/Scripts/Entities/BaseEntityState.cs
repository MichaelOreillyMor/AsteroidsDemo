using Asteroids.Datas;
using Asteroids.Effects;
using Asteroids.Utilities.Pools;

using UnityEngine;

namespace Asteroids.Entities
{
    /// <summary>
    /// Base entity logic
    /// </summary>
    public abstract class BaseEntityState : PoolMember
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

        protected bool isAlive;

        private bool fixPosOnLimits;
        protected ParticlesFxPlayer destroyFXPlayerPref;

        #region Setup/Unsetup methods

        public virtual void Setup(BaseEntityData baseEntityData)
        {
            fixPosOnLimits = baseEntityData.FixPosOnLimits;
            destroyFXPlayerPref = baseEntityData.DestroyFXPlayerPref;

            if (screenLismitsHandler == null)
                screenLismitsHandler = new ScreenLismitsHandler();

            ActivateEntity(true);
        }

        public virtual void Unsetup()
        {
            ActivateEntity(false);
            audioSource.Stop();
            SimplePool.Despawn(this);
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
            if (destroyFXPlayerPref)
            {
                ParticlesFxPlayer particlesSystemsPlayerGO = (ParticlesFxPlayer)SimplePool.Spawn(destroyFXPlayerPref, rigidbody.position, Quaternion.identity);
                particlesSystemsPlayerGO.Play();
            }  
        }

        #endregion

        #region Resolve collisions

        protected abstract void ResolveEntitiesCollision(BaseEntityState entity);

        private void OnCollision(Collider other)
        {
            if (isAlive)
            {
                BaseEntityState entity = other.gameObject.GetComponent<BaseEntityState>();

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
            if(fixPosOnLimits)
                screenLismitsHandler.FixLimitsPosition(transform);
        }
    }
}
