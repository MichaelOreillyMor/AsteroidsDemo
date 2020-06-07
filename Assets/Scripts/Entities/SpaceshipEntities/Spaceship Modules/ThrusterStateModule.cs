using Asteroids.Datas;
using Asteroids.Utilities.Messages;

using UnityEngine;

namespace Asteroids.Entities.ShipModules
{
    public class ThrusterStateModule : IBasicModule
    {
        private readonly Rigidbody rigidbody;
        private readonly Transform transform;

        #region Movement vars

        private readonly float speed;
        private readonly float maxVelocity;
        private readonly float rotationSpeed;

        private bool isMovingForward;
        private float rotationDir;

        #endregion

        #region Thruster vars

        private readonly float thrusterVelocity;
        private ParticleSystem.VelocityOverLifetimeModule thrusterVelMod;

        #endregion

        #region Setup/Unsetup methods

        public ThrusterStateModule(ThrusterModuleData thrusterData, Rigidbody rigidbody, ParticleSystem thruster) 
        {
            this.rigidbody = rigidbody;
            this.transform = rigidbody.transform;

            thrusterVelMod = thruster.velocityOverLifetime;
            thrusterVelocity = thrusterData.ThrusterVelocity;

            speed = thrusterData.Speed;
            maxVelocity = thrusterData.MaxVelocity;
            rotationSpeed = thrusterData.RotationSpeed;
        }

        public void Setup() 
        {
            Messenger<bool>.AddListener(Messages.ON_MOVE_FORWARD_CHANGE, OnMovingForwardChange);
            Messenger<float>.AddListener(Messages.ON_ROTATE_DIR_CHANGE, OnRotationDirChange);
        }

        public void Unsetup()
        {
            Messenger<bool>.RemoveListener(Messages.ON_MOVE_FORWARD_CHANGE, OnMovingForwardChange);
            Messenger<float>.RemoveListener(Messages.ON_ROTATE_DIR_CHANGE, OnRotationDirChange);
        }

        #endregion

        public void Update()
        {
            if (isMovingForward)
            {
                MoveForward();
            }

            if (rotationDir != 0f)
            {
                Rotate();
            }
        }

        #region Actions methods

        private void Rotate()
        {
            Quaternion deltaRotation = Quaternion.Euler(transform.up * rotationDir * rotationSpeed);
            rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
        }

        private void MoveForward()
        {
            rigidbody.AddRelativeForce(Vector3.forward * speed);
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxVelocity);
        }

        #endregion

        #region Callbacks methods

        private void OnMovingForwardChange(bool isMovingForward)
        {
            this.isMovingForward = isMovingForward;
            thrusterVelMod.z = (isMovingForward) ? thrusterVelocity : 0f;
        }

        private void OnRotationDirChange(float rotationDir)
        {
            this.rotationDir = rotationDir;
        }

        #endregion
    }
}