using Asteroids.Datas;
using Asteroids.Utilities.Messages;

using UnityEngine;

namespace Asteroids.Entities.ShipModules
{
    public class GunsStateModule : IBasicModule, IResetModule
    {
        private BaseGunState mainGunState;
        private BaseGunState specialGunState;

        private bool isShotingMainGun;
        private bool isShotingSpecialGun;

        private Rigidbody rigidbody;

        #region Animator vars

        private const string MAIN_SHOT_ANIM = "OnMainShot";
        private const string SECONDARY_SHOT_ANIM = "OnSecondaryShot";

        private Animator animator;

        #endregion

        #region Setup/Unsetup methods

        public GunsStateModule(BaseGunData mainGunData, Transform mainGunTr, 
                            SpecialGunData specialGunData, Transform specialGunTr, 
                            Animator animator, Rigidbody rigidbody)
        {
            mainGunState = GunStatesFactory.CreateGunState(mainGunData, mainGunTr);
            specialGunState = GunStatesFactory.CreateGunState(specialGunData, specialGunTr);

            this.animator = animator;
            this.rigidbody = rigidbody;
        }
        public void Setup()
        {
            mainGunState.Setup();
            specialGunState.Setup();

            Messenger<bool>.AddListener("OnShotingMainGunChange", OnShotingMainGunChange);
            Messenger<bool>.AddListener("OnShotingSpecialGunChange", OnShotingSpecialGunChange);
        }

        public void Unsetup()
        {
            mainGunState.Unsetup();
            specialGunState.Unsetup();

            Messenger<bool>.RemoveListener("OnShotingMainGunChange", OnShotingMainGunChange);
            Messenger<bool>.RemoveListener("OnShotingSpecialGunChange", OnShotingSpecialGunChange);
        }

        public void ResetState()
        {
            mainGunState.ResetState();
            specialGunState.ResetState();
        }

        #endregion

        public void Update()
        {
            if (isShotingMainGun)
            {
                MainShot();
            }
            else if (isShotingSpecialGun)
            {
                SpecialShot();
            }
        }

        #region Actions methods

        private void MainShot()
        {
            if (mainGunState.CanShot())
            {
                mainGunState.Shot(rigidbody.velocity);
                animator.SetTrigger(MAIN_SHOT_ANIM);
            }
        }

        private void SpecialShot()
        {
            if (specialGunState.CanShot())
            {
                specialGunState.Shot(rigidbody.velocity);
                animator.SetTrigger(SECONDARY_SHOT_ANIM);
            }
        }

        #endregion

        #region Callbacks methods

        private void OnShotingMainGunChange(bool isShotingMainGun)
        {
            this.isShotingMainGun = isShotingMainGun;
        }

        private void OnShotingSpecialGunChange(bool isShotingSpecialGun)
        {
            this.isShotingSpecialGun = isShotingSpecialGun;
        }

        #endregion
    }
}