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
    public class GunState : MonoBehaviour
    {
        private const int PRELOAD_BULLETS = 5;

        private GunData gunData;

        private float time;
        private float reloadTime;
        private float nextShotTime;

        public void Setup(GunData gunData)
        {
            this.gunData = gunData;
            reloadTime = gunData.ReloadTime;

            SimplePool.Preload(gunData.BulletPref, PRELOAD_BULLETS);
        }

        public void Shot(Vector3 currentVel)
        {
            time = Time.time;

            if (time > nextShotTime)
            {
                nextShotTime = time + reloadTime;
                Rocket rocket = (Rocket)SimplePool.Spawn(gunData.BulletPref, transform.position, transform.rotation);
                rocket.Setup(currentVel, gunData.RocketAcceleration, gunData.RocketMaxSpeed, gunData.RocketLifeTime);

                Messenger.Broadcast("OnShot");
            }
        }

        public void Unsetup()
        {
            List<PoolMember> rocketsActive = SimplePool.GetActiveInstances(gunData.BulletPref);
            Rocket rocket;

            for (int i = rocketsActive.Count - 1; i >= 0; i--)
            {
                rocket = (Rocket)rocketsActive[i];
                rocket.Unsetup();
            }
        }
    }
}