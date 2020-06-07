using Asteroids.Datas;
using Asteroids.Utilities.Messages;

using UnityEngine;

namespace Asteroids.Entities.ShipModules
{
    public class GunsStateModule : IBasicModule, IResetModule
    {
        private readonly BaseGunState mainGunState;
        private readonly BaseGunState specialGunState;

        private bool isShotingMainGun;
        private bool isShotingSpecialGun;

        private readonly Rigidbody rigidbody;

        #region Setup/Unsetup methods

        public GunsStateModule(BaseGunData mainGunData, Transform mainGunTr, 
                                SpecialGunData specialGunData, Transform specialGunTr, 
                                Rigidbody rigidbody)
        {
            mainGunState = GunStatesFactory.CreateGunState(mainGunData, mainGunTr);
            specialGunState = GunStatesFactory.CreateGunState(specialGunData, specialGunTr);


            this.rigidbody = rigidbody;
        }
        public void Setup()
        {
            mainGunState.Setup();
            specialGunState.Setup();

            Messenger<bool>.AddListener(Messages.ON_SHOTING_MAIN_GUN_CHANGE, OnShotingMainGunChange);
            Messenger<bool>.AddListener(Messages.ON_SHOTING_SPECIAL_GUN_CHANGE, OnShotingSpecialGunChange);
        }

        public void Unsetup()
        {
            mainGunState.Unsetup();
            specialGunState.Unsetup();

            Messenger<bool>.RemoveListener(Messages.ON_SHOTING_MAIN_GUN_CHANGE, OnShotingMainGunChange);
            Messenger<bool>.RemoveListener(Messages.ON_SHOTING_SPECIAL_GUN_CHANGE, OnShotingSpecialGunChange);
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
                Messenger.Broadcast(Messages.ON_MAIN_GUN_FIRE);
            }
        }

        private void SpecialShot()
        {
            if (specialGunState.CanShot())
            {
                specialGunState.Shot(rigidbody.velocity);
                Messenger.Broadcast(Messages.ON_SPECIAL_GUN_FIRE);
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