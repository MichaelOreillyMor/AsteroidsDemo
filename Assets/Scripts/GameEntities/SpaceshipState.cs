using Asteroids.Datas;
using Asteroids.Systems;
using Asteroids.Input;
using Asteroids.Utilities.Messages;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Entities
{
    /// <summary>
    /// The Spaceship is a special entity, it has been created to be the state of a data. 
    /// I would like to create another screen where the player can choose between different spaceship models and
    /// different weapons to attach.
    /// </summary>
    public class SpaceshipState : BaseGameEntity, GameControls.ISpaceshipActions
    {
        private const float THRUSTER_VEL_ON_MOVE = 0.6f;

        [SerializeField]
        private GunState gunState;

        private SpaceshipData spaceshipData;

        private ParticleSystem thruster;
        private ParticleSystem.VelocityOverLifetimeModule thrusterVelMod;

        public float speed;
        public float maxVelocity;
        public float rotationSpeed;

        private bool isShoting;
        private bool isMovingForward;
        private float rotationDir;

        private bool wasMovingForwardLastFrame;

        private bool isAlive;

        #region Setup/Unsetup methods

        public void Setup(SpaceshipData spaceshipData)
        {           
            this.spaceshipData = spaceshipData;

            speed = spaceshipData.Speed;
            maxVelocity = spaceshipData.MaxVelocity;
            rotationSpeed = spaceshipData.RotationSpeed;

            InstantiateSpaceshipModel(spaceshipData.SpaceshipPref);
            gunState.Setup(spaceshipData.GunData);

            thruster = model3D.GetComponentInChildren<ParticleSystem>();
            thrusterVelMod = thruster.velocityOverLifetime;

            audioSource.clip = spaceshipData.EngineSound;
            audioSource.Play();

            isAlive = true;
            base.Setup();
        }

        public void ResetState()
        {
            rigidbody.isKinematic = true;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            rigidbody.isKinematic = false;

            ActiveModel(true);
            isAlive = true;
        }

        public void InstantiateSpaceshipModel(GameObject spaceshipPref)
        {
            model3D = Instantiate(spaceshipPref);
            Transform modelTr = model3D.transform;
            modelTr.SetParent(transform);
            modelTr.localPosition = Vector3.zero;
            modelTr.localRotation = Quaternion.identity;
        }

        public override void Unsetup()
        {
            gunState.Unsetup();
            base.Unsetup();
        }

        #endregion

        #region Actions
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
                if (isShoting)
                {
                    Shot();
                }

                if(wasMovingForwardLastFrame != isMovingForward)
                {
                    SetThrusterEmission();
                    wasMovingForwardLastFrame = isMovingForward;
                }
            }
        }

        private void SetThrusterEmission()
        {
            thrusterVelMod.z = (isMovingForward) ? THRUSTER_VEL_ON_MOVE : 0f;
        }

        public void Shot()
        {
            gunState.Shot(rigidbody.velocity);
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

        public void OnShot(InputAction.CallbackContext context)
        {
            isShoting = context.ReadValue<float>() == 1f;
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

        #region Collision

        protected override void ResolveEntitiesCollision(BaseGameEntity entity)
        {
            if (entity is Asteroid)
                Destroy();
        }

        private void Destroy()
        {
            isAlive = false;
            audioSource.Stop();
            PlayDestroyFXs();
            ActiveModel(false);

            Messenger<bool>.Broadcast("OnEndGame", false);
        }

        #endregion
    }
}
