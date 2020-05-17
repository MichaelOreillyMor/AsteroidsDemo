using Asteroids.Entities;
using Asteroids.Datas;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Entities
{
    public class GunStatesFactory
    {
        public static BaseGunState GetGunState(BaseGunData baseGunData, Transform gunTr) 
        {
            BaseGunState gunState;

            if (baseGunData is ShotgunData)
            {

            }
            else 
            {

            }

            //pre load explosions?
            return gunState;
        }
    }
}