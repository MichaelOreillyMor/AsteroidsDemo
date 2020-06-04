using Asteroids.Datas;
using Asteroids.Entities.ShipModules;
using Asteroids.Utilities.Messages;
using Asteroids.Utilities.Pools;
using Asteroids.Input;

using UnityEngine;

namespace Asteroids.Entities
{
    /// <summary>
    /// SpaceshipState is a special Entity, I tried to follow a design by composition.
    /// The rockets or asteroids are performing a single behavior,
    /// and they can be build just with a ScriptableObject. 
    /// The spaceship is a composition of independent behaviors that we can call modules,
    /// The modules are independent and should be injected by a factory.
    /// The modules are Interfaces to follow the dependency inversion principle.
    /// The modulues injection is independent of the setup/unsetup of the spaceship that should be handler by another class.
    /// </summary>
    public class SpaceshipState : BaseEntityState
    {
        #region Guns vars

        [SerializeField]
        private Transform mainGunTr;
        public Transform MainGunTr => mainGunTr;

        [SerializeField]
        private Transform specialGunTr;
        public Transform SpecialGunTr => specialGunTr;

        #endregion

        #region Modules vars

        public GameControls.ISpaceshipActions IinputModule { get; private set; }

        private IBasicModule IthrusterModule;
        private IResetModule IgunsModule;

        #endregion

        #region Setup/Unsetup methods

        public void InjectDependencies(IBasicModule IthrusterModule, IResetModule IgunsModule, 
                                         GameControls.ISpaceshipActions IinputModule, GameObject spaceshipModel)
        {
            this.IthrusterModule = IthrusterModule;
            this.IgunsModule = IgunsModule;
            this.IinputModule = IinputModule;

            model3D = spaceshipModel;
        }

        public void Setup(SpaceshipData spaceshipData)
        {
            if (IthrusterModule != null && IgunsModule != null)
            {
                IthrusterModule.Setup();
                IgunsModule.Setup();
                ResetPosition();

                audioSource.clip = spaceshipData.EngineSound;
                audioSource.Play();

                SimplePool.Preload(spaceshipData.DestroyFXPlayerPref, 1);
                base.Setup(spaceshipData);
            }
            else 
            {
                Debug.LogError("Modules dependencies should be injected first");
            }
        }

        public void ResetPosition()
        {
            rigidbody.isKinematic = true;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            rigidbody.isKinematic = false;
        }

        public void ResetState()
        {
            ResetPosition();
            IgunsModule.ResetState();
            ActivateEntity(true);
        }

        public override void Unsetup()
        {
            IgunsModule.Unsetup();
            IthrusterModule.Unsetup();

            Destroy(model3D);

            ActivateEntity(false);
            audioSource.Stop();
        }

        #endregion

        #region Update modules methods

        protected override void FixedUpdate()
        {
            if (isAlive)
            {
                base.FixedUpdate();
                IthrusterModule.Update();
            }
        }

        private void Update()
        {
            if (isAlive)
            {
                IgunsModule.Update();
            }
        }

        #endregion

        #region Collisions methods

        protected override void ResolveEntitiesCollision(BaseEntityState entity)
        {
            if (entity is AsteroidState)
            {
                Destroy();
            }
        }

        private void Destroy()
        {
            audioSource.Stop();
            PlayDestroyFXs();
            ActivateEntity(false);

            Messenger<bool>.Broadcast("OnEndGame", false);
        }

        #endregion
    }
}
