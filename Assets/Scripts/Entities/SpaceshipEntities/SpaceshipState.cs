using Asteroids.Datas;
using Asteroids.Entities.ShipModules;
using Asteroids.Utilities.Messages;
using Asteroids.Utilities.Pools;

using UnityEngine;

namespace Asteroids.Entities
{
    public class SpaceshipState : BaseEntityState
    {
        #region Guns vars

        [SerializeField]
        private Transform mainGunTr;

        [SerializeField]
        private Transform specialGunTr;

        #endregion

        #region Modules vars

        public SpaceshipInputModule InputModule { get; private set; }
        public SpaceshipThrusterModule ThrusterModule { get; private set; }
        public SpaceshipGunsModule GunsModule { get; private set; }

        #endregion

        #region Setup/Unsetup methods

        public void Setup(SpaceshipData spaceshipData)
        {
            InstantiateModel(spaceshipData.SpaceshipModel);

            SetupInput();
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
            GunsModule = new SpaceshipGunsModule(spaceshipData.BaseGunData, mainGunTr,
                                                    spaceshipData.SpecialGunData, specialGunTr,
                                                    animator, rigidbody);

            GunsModule.Setup();
        }

        private void SetupThruster(SpaceshipData spaceshipData)
        {
            ParticleSystem thruster = model3D.GetComponentInChildren<ParticleSystem>();
            ThrusterModule = new SpaceshipThrusterModule(spaceshipData.ThrusterData, rigidbody, thruster);
            ThrusterModule.Setup();
        }

        private void SetupInput()
        {
            InputModule = new SpaceshipInputModule();
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
            GunsModule.ResetState();
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
            GunsModule.Unsetup();
            ThrusterModule.Unsetup();

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
                ThrusterModule.Update();
            }
        }

        private void Update()
        {
            if (isAlive)
            {
                GunsModule.Update();
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
