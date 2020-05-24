using Asteroids.Datas;
using Asteroids.Utilities.Messages;
using Asteroids.Utilities.Pools;

using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Entities
{
    public class ShotgunState : SpecialGunState
    {
        private Quaternion[] rotationRockets;
        private int rocketsPerShot;
        private int shotArc;

        #region Setup/Unsetup methods

        public ShotgunState(ShotgunData specialGunData, Transform gunTr) : base(specialGunData, gunTr)
        {
            rocketsPerShot = specialGunData.RocketsPerShot;
            shotArc = specialGunData.ShotArc;
            SetRotationRockets();
        }
        private void SetRotationRockets()
        {
            rotationRockets = new Quaternion[rocketsPerShot];

            Vector3 halfRot = Vector3.up * (shotArc / 2);
            float step;

            for (int i = 0; i < rocketsPerShot; i++)
            {
                step = (float)i / (rocketsPerShot - 1);
                rotationRockets[i] = Quaternion.Euler(Vector3.Lerp(-halfRot, halfRot, step));
            }
        }

        #endregion

        #region Shot methods

        public override bool Shot(Vector3 currentVel)
        {
            time = Time.time;

            if (currentAmmunition > 0 && time > nextShotTime)
            {
                string shotID = GenerateShotID();

                nextShotTime = time + reloadTime;
                currentAmmunition--;

                foreach (Quaternion rotationRocket in rotationRockets)
                {
                    ShotRocket(currentVel, rotationRocket, shotID);
                }

                return true;
            }

            return false;
        }

        #endregion
    }
}
