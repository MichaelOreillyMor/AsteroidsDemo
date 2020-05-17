using Asteroids.Entities;
using Asteroids.Datas;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Entities
{
    public class GunStatesFactory
    {
        public static BaseGunState CreateGunState(BaseGunData baseGunData, Transform gunTr) 
        {
            BaseGunState gunState;

            if (baseGunData is ShotgunData)
            {
                ShotgunData shotgunData = (ShotgunData)baseGunData;
                ShotgunState shotgunState = new ShotgunState(shotgunData, gunTr);
                gunState = shotgunState;
            }
            else if (baseGunData is SpecialGunData)
            {
                SpecialGunData specialGunData = (SpecialGunData)baseGunData;
                SpecialGunState specialGunState = new SpecialGunState(specialGunData, gunTr);
                gunState = specialGunState;
            }
            else 
            {
                gunState = new BaseGunState(baseGunData, gunTr);
            }

            //pre load explosions?
            return gunState;
        }
    }
}