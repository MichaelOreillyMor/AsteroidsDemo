using Asteroids.Datas;
using Asteroids.Utilities.Messages;

using UnityEngine;

namespace Asteroids.Entities.ShipModules
{
    public class SpaceshipThrusterModule
    {
        private Rigidbody rigidbody;
        private Transform transform;

        #region Movement vars

        private bool isMovingForward;
        private float rotationDir;

        private float speed;
        private float maxVelocity;
        private float rotationSpeed;

        #endregion

        #region Thruster vars

        private ParticleSystem.VelocityOverLifetimeModule thrusterVelMod;
        private float thrusterVelocity;

        #endregion

        #region Setup/Unsetup methods

        public SpaceshipThrusterModule(ThrusterData thrusterData,
                                        Rigidbody rigidbody, ParticleSystem thruster) 
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
            Messenger<bool>.AddListener("OnMoveForwardChange", OnMovingForwardChange);
            Messenger<float>.AddListener("OnRotateDirChange", OnRotationDirChange);
        }

        public void Unsetup()
        {
            Messenger<bool>.RemoveListener("OnMoveForwardChange", OnMovingForwardChange);
            Messenger<float>.RemoveListener("OnRotateDirChange", OnRotationDirChange);
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