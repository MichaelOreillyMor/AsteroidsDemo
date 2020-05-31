using Asteroids.Datas;
using Asteroids.Entities.ShipModules;
using Asteroids.Input;
using Asteroids.Utilities.Messages;
using Asteroids.Utilities.Pools;

using UnityEngine;

namespace Asteroids.Entities
{
    /// <summary>
    /// I would like to create another screen where the player can choose 
    /// between different spaceship models and different weapons to attach.
    /// </summary>
    public class SpaceshipState : BaseEntityState
    {
        #region Guns vars

        [SerializeField]
        private Transform mainGunTr;

        [SerializeField]
        private Transform specialGunTr;

        #endregion

        #region Modules vars

        private SpaceshipInputModule spaceshipInputModule;
        private SpaceshipThrusterModule spaceshipThrusterModule;
        private SpaceshipGunsModule spaceshipGunsModule;

        #endregion


        #region Setup/Unsetup methods

        public void Setup(SpaceshipData spaceshipData, InputHandler inputHandler)
        {
            InstantiateModel(spaceshipData.SpaceshipModel);

            SetupInput(inputHandler);
            SetupThruster(spaceshipData);
            SetupGuns(spaceshipData);

            ResetPosition();

            audioSource.clip = spaceshipData.EngineSound;
            audioSource.Play();

            SimplePool.Preload(spaceshipData.DestroyFXPlayerPref, 1);
            base.Setup(spaceshipData);
        }

        private void SetupGuns(SpaceshipData spaceshipData)
        {
            Animator animator = model3D.GetComponent<Animator>();
            spaceshipGunsModule = new SpaceshipGunsModule(spaceshipData.BaseGunData, mainGunTr,
                                                            spaceshipData.SpecialGunData, specialGunTr,
                                                            animator, rigidbody);

            spaceshipGunsModule.Setup();
        }

        private void SetupThruster(SpaceshipData spaceshipData)
        {
            ParticleSystem thruster = model3D.GetComponentInChildren<ParticleSystem>();
            spaceshipThrusterModule = new SpaceshipThrusterModule(spaceshipData.ThrusterData, rigidbody, thruster);
            spaceshipThrusterModule.Setup();
        }

        private void SetupInput(InputHandler inputHandler)
        {
            spaceshipInputModule = new SpaceshipInputModule();
            inputHandler.SetSpaceshipActionsCallbacks(spaceshipInputModule);
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
            spaceshipGunsModule.ResetState();
            ActivateEntity(true);
        }

        public void InstantiateModel(GameObject spaceshipModel)
        {
            model3D = Instantiate(spaceshipModel);
            Transform modelTr = model3D.transform;

            modelTr.SetParent(transform);
            modelTr.localPosition = Vector3.zero;
            modelTr.localRotation = Quaternion.identity;
        }

        public override void Unsetup()
        {
            spaceshipGunsModule.Unsetup();
            spaceshipThrusterModule.Unsetup();

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
                spaceshipThrusterModule.Update();
            }
        }

        private void Update()
        {
            if (isAlive)
            {
                spaceshipGunsModule.Update();
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
