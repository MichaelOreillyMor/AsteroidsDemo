using Asteroids.Datas;
using Asteroids.Utilities.Messages;

using UnityEngine;

namespace Asteroids.Entities.ShipModules
{
    public class ThrusterStateModule : IBasicModule
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

        #region Animator vars

        private const string DIRECTION = "Direction";

        private const float ANIM_THRESHOLD = 0.1f;

        private const float ANIM_LEFT = -1f;
        private const float ANIM_RIGHT = 1f;
        private const float ANIM_FORWARD = 0f;

        private Animator animator;
        private float animSpeed;
        private float animDir;

        #endregion 

        #region Setup/Unsetup methods

        public ThrusterStateModule(ThrusterModuleData thrusterData, Rigidbody rigidbody, 
                                    ParticleSystem thruster, Animator animator) 
        {
            this.rigidbody = rigidbody;
            this.transform = rigidbody.transform;

            thrusterVelMod = thruster.velocityOverLifetime;
            thrusterVelocity = thrusterData.ThrusterVelocity;

            this.animator = animator;
            animSpeed = thrusterData.AnimSpeed;
            animDir = 0;

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

            UpdateDirAnim();
        }

        private void UpdateDirAnim()
        {
            if ((rotationDir > ANIM_FORWARD && animDir < ANIM_RIGHT) || (rotationDir == 0 && animDir < -ANIM_THRESHOLD))
            {
                animDir += animSpeed * Time.deltaTime;
                if (animDir > ANIM_RIGHT)
                    animDir = ANIM_RIGHT;

                animator.SetFloat("Direction", animDir);
            }
            else if ((rotationDir < ANIM_FORWARD && animDir > ANIM_LEFT) || (rotationDir == 0 && animDir > ANIM_THRESHOLD))
            {
                animDir -= animSpeed * Time.deltaTime;
                if (animDir < ANIM_LEFT)
                    animDir = ANIM_LEFT;

                animator.SetFloat("Direction", animDir);
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