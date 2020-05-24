using Asteroids.Datas;
using Asteroids.Input;
using Asteroids.Utilities.Messages;
using Asteroids.Utilities.Pools;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Entities
{
    /// <summary>
    /// I would like to create another screen where the player can choose 
    /// between different spaceship models and different weapons to attach.
    /// </summary>
    public class SpaceshipState : BaseEntityState, GameControls.ISpaceshipActions
    {
        #region Guns vars

        [SerializeField]
        private Transform mainGunTr;

        [SerializeField]
        private Transform secondaryGunTr;

        private BaseGunState mainGunState;
        private BaseGunState secondaryGunState;

        private bool isShotingMainGun;
        private bool isShotingSecondaryGun;

        #endregion

        #region Movement vars

        private float speed;
        private float maxVelocity;
        private float rotationSpeed;
        private float rotationDir;

        #endregion

        #region Input vars

        private bool isMovingForward;
        private bool wasMovingForwardLastFrame;

        #endregion

        #region Thruster vars

        private ParticleSystem thruster;
        private ParticleSystem.VelocityOverLifetimeModule thrusterVelMod;
        private float thrusterVelocity;

        #endregion

        #region Animator vars

        private const string MAIN_SHOT_ANIM = "OnMainShot";
        private const string SECONDARY_SHOT_ANIM = "OnSecondaryShot";

        private Animator animator;

        #endregion

        #region Setup/Unsetup methods

        public void Setup(SpaceshipData spaceshipData)
        {           
            speed = spaceshipData.Speed;
            maxVelocity = spaceshipData.MaxVelocity;
            rotationSpeed = spaceshipData.RotationSpeed;

            InstantiateSpaceshipModel(spaceshipData.SpaceshipPref);

            mainGunState = GunStatesFactory.CreateGunState(spaceshipData.BaseGunData, mainGunTr);
            secondaryGunState = GunStatesFactory.CreateGunState(spaceshipData.SpecialGunData, secondaryGunTr);

            thrusterVelocity = spaceshipData.ThrusterVelocity;
            thruster = model3D.GetComponentInChildren<ParticleSystem>();
            thrusterVelMod = thruster.velocityOverLifetime;

            audioSource.clip = spaceshipData.EngineSound;
            audioSource.Play();

            SimplePool.Preload(spaceshipData.DestroyFXPlayerPref, 1);
            base.Setup(spaceshipData);
        }

        public void ResetState()
        {
            rigidbody.isKinematic = true;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            rigidbody.isKinematic = false;

            mainGunState.ResetState();
            secondaryGunState.ResetState();

            ActivateEntity(true);
        }

        public void InstantiateSpaceshipModel(GameObject spaceshipPref)
        {
            model3D = Instantiate(spaceshipPref);

            Transform modelTr = model3D.transform;
            modelTr.SetParent(transform);
            modelTr.localPosition = Vector3.zero;
            modelTr.localRotation = Quaternion.identity;

            animator = modelTr.GetComponent<Animator>();
        }

        public override void Unsetup()
        {
            mainGunState.Unsetup();
            secondaryGunState.Unsetup();

            base.Unsetup();
        }

        #endregion

        #region Actions methods

        protected override void FixedUpdate()
        {
            if (isAlive)
            {
                base.FixedUpdate();

                if (isMovingForward)
                {
                    MoveForward();
                }

                if (rotationDir != 0f)
                {
                    Rotate();
                }
            }
        }

        private void Update()
        {
            if (isAlive)
            {
                if (isShotingMainGun)
                {
                    MainShot();
                }
                else if (isShotingSecondaryGun)
                {
                    SecondaryShot();
                }

                if (wasMovingForwardLastFrame != isMovingForward)
                {
                    SetThrusterEmission();
                    wasMovingForwardLastFrame = isMovingForward;
                }
            }
        }

        private void SetThrusterEmission()
        {
            thrusterVelMod.z = (isMovingForward) ? thrusterVelocity : 0f;
        }

        public void MainShot()
        {
            bool canShot = mainGunState.Shot(rigidbody.velocity);

            if(canShot)
                animator.SetTrigger(MAIN_SHOT_ANIM);
        }
        public void SecondaryShot()
        {
            bool canShot = secondaryGunState.Shot(rigidbody.velocity);

            if (canShot)
                animator.SetTrigger(SECONDARY_SHOT_ANIM);
        }

        public void Rotate()
        {
            Quaternion deltaRotation = Quaternion.Euler(transform.up * rotationDir * rotationSpeed);
            rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
        }

        public void MoveForward()
        {
            rigidbody.AddRelativeForce(Vector3.forward * speed);
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxVelocity);
        }

        public void OnMainShot(InputAction.CallbackContext context)
        {
            isShotingMainGun = context.ReadValue<float>() == 1f;
        }

        public void OnSecondaryShot(InputAction.CallbackContext context)
        {
            isShotingSecondaryGun = context.ReadValue<float>() == 1f;
        }

        public void OnMoveForward(InputAction.CallbackContext context)
        {
            isMovingForward = context.ReadValue<float>() == 1f;
        }

        public void OnRotate(InputAction.CallbackContext context)
        {
            rotationDir = context.ReadValue<float>();
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
