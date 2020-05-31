using Asteroids.Datas;
using Asteroids.Utilities.Messages;

using UnityEngine;

namespace Asteroids.Entities
{
    public class SpecialGunState : BaseGunState
    {
        protected int maxAmmunition;
        protected int currentAmmunition;

        #region Setup/Unsetup methods

        public SpecialGunState(SpecialGunData specialGunData, Transform gunTr) : base(specialGunData, gunTr) 
        {
            maxAmmunition = specialGunData.MaxAmmunition;
            currentAmmunition = 0;
        }
        public override void Setup()
        {
            base.Setup();
            StartListeningAddAmmunitionEvent();
        }

        public override void Unsetup()
        {
            base.Unsetup();
            StopListeningAddAmmunitionEvent();
        }
        public override void ResetState()
        {
            currentAmmunition = 0;
            base.ResetState();
        }

        #endregion

        #region Shot methods

        public override bool CanShot()
        {
            return (currentAmmunition > 0 && base.CanShot());
        }

        protected override void ConsumeShot()
        {
            base.ConsumeShot();
            currentAmmunition--;
        }

        #endregion

        #region Ammunition methods

        protected void StartListeningAddAmmunitionEvent()
        {
            Messenger<AsteroidDestroyedMessage>.AddListener("OnAsteroidDestroyed", AddAmunition);
        }

        protected void StopListeningAddAmmunitionEvent()
        {
            Messenger<AsteroidDestroyedMessage>.RemoveListener("OnAsteroidDestroyed", AddAmunition);
        }

        protected void AddAmunition(AsteroidDestroyedMessage  asteroidDestroyedMessage)
        {
            bool isMyShot = asteroidDestroyedMessage.ShotID.Contains(gunName);

            if (currentAmmunition < maxAmmunition && !isMyShot)            
                currentAmmunition++;
            
        }

        #endregion
    }
}