using Asteroids.Datas;
using Asteroids.Utilities.Messages;

using UnityEngine;

namespace Asteroids.Entities.ShipModules
{
    public class AnimStateModule : IResetModule
    {
        private bool isMovingForward;
        private float rotationDir;

        #region Animator vars

        private const string ROT_ANIM = "Rotation";
        private const string FORW_ANIM = "Forward";
        private const string MAIN_SHOT_ANIM = "OnMainShot";
        private const string SPECIAL_SHOT_ANIM = "OnSpecialShot";

        private readonly float animMoveSpeed;

        private readonly float rotThreshold;

        private readonly float rotLeft;
        private readonly float rotRight;

        private readonly float forward;
        private readonly float idle;


        private float rotAmount;
        private float forwAmount;

        private readonly Animator animator;

        #endregion

        #region Setup/Unsetup methods

        public AnimStateModule(AnimModuleData animModuleData, Animator animator)
        {
            animMoveSpeed = animModuleData.AnimMoveSpeed;

            rotThreshold = animModuleData.RotThreshold;

            rotLeft = animModuleData.RotLeft;
            rotRight = animModuleData.RotRight;

            forward = animModuleData.Forward;
            idle = animModuleData.Idle;

            this.animator = animator;
        }

        public void Setup()
        {
            Messenger<bool>.AddListener(Messages.ON_MOVE_FORWARD_CHANGE, OnMovingForwardChange);
            Messenger<float>.AddListener(Messages.ON_ROTATE_DIR_CHANGE, OnRotationDirChange);

            Messenger.AddListener(Messages.ON_MAIN_GUN_FIRE, OnMainGunFire);
            Messenger.AddListener(Messages.ON_SPECIAL_GUN_FIRE, OnSpecialGunFire);
        }

        public void Unsetup()
        {
            Messenger<bool>.RemoveListener(Messages.ON_MOVE_FORWARD_CHANGE, OnMovingForwardChange);
            Messenger<float>.RemoveListener(Messages.ON_ROTATE_DIR_CHANGE, OnRotationDirChange);

            Messenger.RemoveListener(Messages.ON_MAIN_GUN_FIRE, OnMainGunFire);
            Messenger.RemoveListener(Messages.ON_SPECIAL_GUN_FIRE, OnSpecialGunFire);
        }

        public void ResetState()
        {
            rotAmount = 0;
            forwAmount = 0;
            animator.Rebind();
        }

        #endregion

        public void Update()
        {
            UpdateRotation();
            UpdateForward();
        }

        #region Update anim methods

        private void UpdateForward()
        {
            if (isMovingForward && forwAmount < forward)
            {
                forwAmount += animMoveSpeed * Time.deltaTime;
                if (forwAmount > forward)
                    forwAmount = forward;

                animator.SetFloat(FORW_ANIM, forwAmount);
            }
            else if (!isMovingForward && forwAmount > idle)
            {
                forwAmount -= animMoveSpeed * Time.deltaTime;
                if (forwAmount < idle)
                    forwAmount = idle;

                animator.SetFloat(FORW_ANIM, forwAmount);
            }
        }

        private void UpdateRotation()
        {
            if ((rotationDir > idle && rotAmount < rotRight) || (rotationDir == 0 && rotAmount < -rotThreshold))
            {
                rotAmount += animMoveSpeed * Time.deltaTime;
                if (rotAmount > rotRight)
                    rotAmount = rotRight;

                animator.SetFloat(ROT_ANIM, rotAmount);
            }
            else if ((rotationDir < idle && rotAmount > rotLeft) || (rotationDir == 0 && rotAmount > rotThreshold))
            {
                rotAmount -= animMoveSpeed * Time.deltaTime;
                if (rotAmount < rotLeft)
                    rotAmount = rotLeft;

                animator.SetFloat(ROT_ANIM, rotAmount);
            }
        }

        #endregion

        #region Callbacks methods

        private void OnMovingForwardChange(bool isMovingForward)
        {
            this.isMovingForward = isMovingForward;
        }

        private void OnRotationDirChange(float rotationDir)
        {
            this.rotationDir = rotationDir;
        }

        private void OnMainGunFire()
        {
            animator.SetTrigger(MAIN_SHOT_ANIM);
        }

        private void OnSpecialGunFire()
        {
            animator.SetTrigger(SPECIAL_SHOT_ANIM);
        }

        #endregion
    }
}