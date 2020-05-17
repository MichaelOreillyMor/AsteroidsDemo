using Asteroids.Datas;
using Asteroids.Utilities.Messages;
using Asteroids.Utilities.Pools;

using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Entities
{
    /// <summary>
    /// I would like to create another screen where the player can choose between different different weapons to attach.
    /// </summary>
    public class BaseGunState
    {
        protected BaseGunData gunData;
        protected Transform gunTr;

        protected RocketState rocketPref;

        protected float time;
        protected float reloadTime;
        protected float nextShotTime;

        protected int shotsCount;
        protected string gunName;

        #region Setup/Unsetup methods

        public BaseGunState(BaseGunData gunData, Transform gunTr)
        {
            this.gunData = gunData;
            this.gunTr = gunTr;

            rocketPref = gunData.RocketData.RocketPref;
            reloadTime = gunData.ReloadTime;
            gunName = gunData.name;

            SimplePool.Preload(rocketPref, gunData.PreloadRocketPrefs);
            SimplePool.Preload(gunData.RocketData.DestroyFXPlayerPref, gunData.PreloadRocketPrefs);
        }

        public virtual void Unsetup()
        {
            ResetRockets();
        }

        private void ResetRockets()
        {
            List<PoolMember> rocketsActive = SimplePool.GetActiveInstances(rocketPref);

            if (rocketsActive.Count > 0)
            {
                for (int i = rocketsActive.Count - 1; i >= 0; i--)
                {
                    RocketState rocket = (RocketState)rocketsActive[i];
                    rocket.Unsetup();
                }
            }
        }

        #endregion

        #region Shot methods

        public virtual void Shot(Vector3 currentVel)
        {
            time = Time.time;

            if (time > nextShotTime)
            {
                string shotID = GenerateShotID();

                nextShotTime = time + reloadTime;

                ShotRocket(currentVel, Quaternion.identity, shotID);
            }
        }

        /// <summary>
        /// Generates an simple ID for all the shot´s rockets 
        /// </summary>
        /// <returns>shot ID</returns>
        protected string GenerateShotID() 
        {
            if (shotsCount == int.MaxValue)
                shotsCount = 0;

            shotsCount++;
            return gunName + shotsCount;
        }

        protected void ShotRocket(Vector3 currentVel, Quaternion rotationRocket, string shotID)
        {
            Quaternion relativeRotation = gunTr.rotation * rotationRocket;
            RocketState rocket = (RocketState)SimplePool.Spawn(rocketPref, gunTr.position, relativeRotation);
            rocket.Setup(gunData.RocketData, currentVel, shotID);
        }

        #endregion
    }
}