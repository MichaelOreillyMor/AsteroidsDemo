using Asteroids.Datas;
using Asteroids.Utilities.Messages;
using Asteroids.Utilities.Pools;

using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Entities
{
    /// <summary>
    /// The GunState is a special entity, it has been created to be the state of a data. 
    /// I would like to create another screen where the player can choose between different different weapons to attach.
    /// </summary>
    public class BaseGunState
    {
        private BaseGunData gunData;
        private Transform gunTr;

        private RocketState rocketPref;

        private float time;
        private float reloadTime;
        private float nextShotTime;

        private Quaternion[] rotationRockets;
        private int rocketsPerShot;
        private int shotArc;

        private int shotsCount;
        private string gunName;

        public BaseGunState(BaseGunData gunData, Transform gunTr)
        {
            this.gunData = gunData;
            this.gunTr = gunTr;

            rocketPref = gunData.RocketPref;

            reloadTime = gunData.ReloadTime;
            rocketsPerShot = gunData.RocketsPerShot;
            shotArc = gunData.ShotArc;

            gunName = gunData.name;

            SetRotationRockets();
            SimplePool.Preload(rocketPref, gunData.PreloadRocketPrefs);
        }

        private void SetRotationRockets()
        {
            rotationRockets = new Quaternion[rocketsPerShot];
            rotationRockets[0] = Quaternion.Euler(Vector3.up);

            if (rocketsPerShot > 1)
            {
                Vector3 halfRot = Vector3.up * (shotArc / 2);
                float step;

                for (int i = 0; i < rocketsPerShot; i++)
                {
                    step = (float)i / (rocketsPerShot - 1);
                    rotationRockets[i] = Quaternion.Euler(Vector3.Lerp(-halfRot, halfRot, step));
                }
            }
        }

        public void Shot(Vector3 currentVel)
        {
            time = Time.time;

            if (time > nextShotTime)
            {
                int shotID = GenerateShotID();
                nextShotTime = time + reloadTime;

                foreach (Quaternion rotationRocket in rotationRockets)
                {
                    ShotRocket(currentVel, rotationRocket, shotID);
                }

                Messenger.Broadcast("OnShot");
            }
        }

        /// <summary>
        /// Generates an simple ID for all the shot´s rockets 
        /// </summary>
        /// <returns>shot ID</returns>
        private string GenerateShotID() 
        {
            if (shotsCount == int.MaxValue)
                shotsCount = 0;

            shotsCount++;

            return gunName + shotsCount;
        }

        private void ShotRocket(Vector3 currentVel, Quaternion rotationRocket, int shotID)
        {
            Quaternion relativeRotation = gunTr.rotation * rotationRocket;
            RocketState rocket = (RocketState)SimplePool.Spawn(rocketPref, gunTr.position, relativeRotation);
            rocket.Setup(gunData, currentVel, shotID);
        }

        public void ResetRockets()
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
    }
}