using Asteroids.Datas;
using Asteroids.Utilities.Messages;

using UnityEngine;

namespace Asteroids.Entities.ShipModules
{
    public class AnimModule : IResetModule
    {
        private bool isMovingForward;
        private float rotationDir;

        #region Animator vars

        private const string DIR_ANIM = "Direction";
        private const string MAIN_SHOT_ANIM = "OnMainShot";
        private const string SPECIAL_SHOT_ANIM = "OnSpecialShot";

        private readonly float animMoveSpeed;

        private readonly float animThreshold;

        private readonly float animLeft;
        private readonly float animRight;

        private readonly float animIdle;
        private readonly float animForward;

        private float animDir;

        private readonly Animator animator;

        #endregion

        #region Setup/Unsetup methods

        public AnimModule(AnimModuleData animModuleData, Animator animator)
        {
            animMoveSpeed = animModuleData.AnimMoveSpeed;

            animThreshold = animModuleData.AnimThreshold;

            animLeft = animModuleData.AnimLeft;
            animRight = animModuleData.AnimRight;

            animIdle = animModuleData.AnimIdle;
            animForward = animModuleData.AnimForward;

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
            animDir = 0;
            animator.Rebind();
        }

        #endregion

        public void Update()
        {
            if ((rotationDir > animIdle && animDir < animRight) || (rotationDir == 0 && animDir < -animThreshold))
            {
                animDir += animMoveSpeed * Time.deltaTime;
                if (animDir > animRight)
                    animDir = animRight;

                animator.SetFloat(DIR_ANIM, animDir);
            }
            else if ((rotationDir < animIdle && animDir > animLeft) || (rotationDir == 0 && animDir > animThreshold))
            {
                animDir -= animMoveSpeed * Time.deltaTime;
                if (animDir < animLeft)
                    animDir = animLeft;

                animator.SetFloat(DIR_ANIM, animDir);
            }
        }

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